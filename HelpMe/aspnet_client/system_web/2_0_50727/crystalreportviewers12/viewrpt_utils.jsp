
<%@ page import="com.crystaldecisions.sdk.occa.report.reportsource.RequestContext,
				 com.crystaldecisions.sdk.occa.report.data.ConnectionInfos,
                 com.crystaldecisions.sdk.occa.report.data.Fields,
                 com.crystaldecisions.sdk.occa.report.data.GroupPath,
                 com.crystaldecisions.xml.serialization.XMLObjectSerializer,
                 java.io.StringReader,
                 java.io.IOException,
                 java.io.PrintWriter,
                 org.xml.sax.SAXException" %>
                 
<%@ page import="com.crystaldecisions.report.web.viewer.CrystalReportViewer"%>   
<%@ page import="com.crystaldecisions.report.web.viewer.CrystalReportViewerBase"%>              
<%@ page import="com.crystaldecisions.report.web.viewer.CrPrintMode,
                 com.crystaldecisions.sdk.occa.report.reportsource.IReportSource,
                 com.businessobjects.crystalreports.reportsourcebridge.ViewerOptions,
                 com.businessobjects.crystalreports.reportsourcebridge.RSBridgeResourceManager,
		 com.businessobjects.crystalreports.reportsourcebridge.SessionObject"%>
                 
<%@ page import="com.crystaldecisions.report.web.viewer.CrystalImageCleaner" %>
<%@ page import="com.crystaldecisions.sdk.occa.report.definition.ReportPartIDs"%>
<%@ page import="com.crystaldecisions.sdk.occa.report.definition.ReportPartID"%>                 
               
<%!
             
String RPT_ID = "id";
String RPT_SRC = "rptsrc";
String INIT = "init";
String PARAM_SELECTIONFORMULA = "sf";
String VALUE_YES = "y";
String PARAM_SHOW_INITIALPART = "bShowInitialReportPart";
String PARAM_REFRESH = "sRefresh";
String PARAM_PARTCONTEXT = "sPartContext";
String PARAM_PARTNAME = "sReportPart";
String PARAM_GROUPPATHID = "iGroup";
String HTMLVIEWER	= "htmlviewerbridge.jsp";
String ADVHTMLVIEWER = "advhtmlviewer.jsp";
String PARTSVIEWER = "partsviewer.jsp";
String RENDERING_DPI = "dpi";
String TOOLBAR = "toolbar";
String DRILLDOWN_TABS = "drilldowntabs";

String L_COMMIT_ERROR="There was an error in launching the viewer: ";
String L_GENERAL_ERROR="An error has occurred: ";

String RSB_SESS_ERRORPROMPT = "RSBErrorPrompt";
String RSB_SESS_ERRORMSG = "RSBErrorMsg";

String errorPagePath = "./errorpage.jsp";



public void SetPageExpiry(HttpServletResponse response)
{
	response.setHeader("Cache-Control","no-cache"); //HTTP 1.1
	response.setHeader("Pragma","no-cache");
	response.setDateHeader ("Expires", -1);
}

public void jspInit()
{
	CrystalImageCleaner.start(getServletConfig().getServletContext(), 60000, 12000);
	super.jspInit();
}
public void jspDestroy()
{
	CrystalImageCleaner.stop(getServletConfig().getServletContext());
	super.jspDestroy();
}

void setViewerOptions(CrystalReportViewer viewer, HttpServletRequest request)
{
	viewer.setHasRefreshButton(ViewerOptions.isShowRefresh());
    viewer.setHasExportButton(ViewerOptions.isShowExport());
    viewer.setHasPrintButton(ViewerOptions.isShowPrint());
    
    String sToolbar = request.getParameter(TOOLBAR);
    // web.xml parameter "viewrpt.toolbar" takes precedence if its value is false
    if (!ViewerOptions.isShowToolbar() || "hide".equalsIgnoreCase(sToolbar))
    {
        viewer.setDisplayToolbar(false);
    }
    else
    {
        viewer.setDisplayToolbar(true);
    }
    
    String sDrilldownTabs = request.getParameter(DRILLDOWN_TABS);
    if ("hide".equalsIgnoreCase(sDrilldownTabs))
    {
        viewer.setHasDrilldownTabs(false);
    }
    else
    {
        viewer.setHasDrilldownTabs(true);
    }
    
    
    viewer.setDisplayGroupTree(ViewerOptions.isShowGroupTree());
    viewer.setHasLogo(ViewerOptions.isShowLogo());
    
    String printControl = request.getParameter("advprint");
	CrPrintMode printMode = CrPrintMode.ACTIVEX;
	if (printControl != null && printControl.length() > 0)
	{
		if (printControl.equalsIgnoreCase("acro"))
			printMode = CrPrintMode.PDF;		 		
	} 	 
	viewer.setPrintMode(printMode);   
}
	
String getReportId(HttpServletRequest request) throws Exception
{	
	String rptid = request.getParameter(RPT_ID);
	if (rptid == null)
	{
		String rptsrckey = request.getParameter(RPT_SRC);
		if (rptsrckey == null)
		{
			String msg = RSBridgeResourceManager.getString("Error_MissingReportID", request.getLocale());
			throw new Exception (msg);			
		}		 
		rptid = java.net.URLDecoder.decode(rptsrckey);	
	}
	return rptid;
}
	
IReportSource getReportSource(HttpServletRequest request, String rptId)throws Exception
{
	HttpSession session = request.getSession();
	String rptid = request.getParameter(RPT_ID);
	Object rptsrcobj = null;
	if (rptid != null)
	{
		String wid = request.getParameter("wid");
		SessionObject sessionObject = SessionObject.getSessionObject(session);
		if (sessionObject != null)
			rptsrcobj = sessionObject.getReportSource(rptid, wid);

		if (rptsrcobj == null || !(rptsrcobj instanceof IReportSource))
			return null;
	} else {
		rptsrcobj = session.getAttribute(rptId);
		if (rptsrcobj == null || !(rptsrcobj instanceof IReportSource))
		{
			Object[] param = { rptId };
			String msg = RSBridgeResourceManager.getStringWithParams("Error_RptSrcInvalid", request.getLocale(), param);
			throw new Exception (msg);
		}
	}	
	return (IReportSource)rptsrcobj;	
}
		
void initializeViewer(CrystalReportViewerBase viewer, 				
				HttpServletRequest request, 
				HttpServletResponse response, 
				String rptKey) throws Exception
{
	viewer.setOwnForm(true);
	viewer.setOwnPage(true); 
	viewer.setTop(0);
	viewer.setLeft(0);

	String wid = request.getParameter("wid");
	String rptid = rptKey + "_" + wid;
	    
	String sf = request.getParameter(PARAM_SELECTIONFORMULA);
	if (sf != null && sf.length()>0)
	{
		String decodedSF = java.net.URLDecoder.decode(sf, "UTF-8");    	
		viewer.setViewTimeSelectionFormula(decodedSF);
	}
    
	String promptOnRefresh = request.getParameter("promptonrefresh");
	if (promptOnRefresh != null && promptOnRefresh.equalsIgnoreCase("0")) 
	{
		viewer.setReuseParameterValuesOnRefresh(true);
	}	

	String promptKey = "rsb_prompts_" + rptid;
	HttpSession session = request.getSession();
	Object parametersStr = session.getAttribute(promptKey);
	if (parametersStr != null && (parametersStr instanceof String))
	{
		XMLObjectSerializer xmlSerializer = new XMLObjectSerializer();

		StringReader sReader = new StringReader((String)parametersStr);
		Fields paramFields = null;
		try {			 
			Object xmlObj = xmlSerializer.load(sReader);
				if (xmlObj instanceof Fields)
			paramFields = (Fields)xmlObj;
		} catch (SAXException e)
		{}

		if (paramFields != null)			
			viewer.setParameterFields(paramFields);
		session.removeAttribute(promptKey);
	}

	String logonKey = "rsb_connInfos_" + rptid;
	Object connInfosStr = session.getAttribute(logonKey);
	if (connInfosStr != null && (connInfosStr instanceof String))
	{
		XMLObjectSerializer xmlSerializer = new XMLObjectSerializer();  

		StringReader sReader = new StringReader((String)connInfosStr);
		ConnectionInfos connInfos = null;
		try {			 
			Object xmlObj = xmlSerializer.load(sReader);
			if (xmlObj instanceof ConnectionInfos)
				connInfos = (ConnectionInfos)xmlObj;
		} catch (SAXException e)
		{}

		if (connInfos != null)			
			viewer.setDatabaseLogonInfos(connInfos);
		session.removeAttribute(logonKey);
	}

	String refreshCommand = request.getParameter(PARAM_REFRESH);
	if (refreshCommand != null && refreshCommand.compareToIgnoreCase(VALUE_YES)==0)
		viewer.refresh();	// Hitting the Database

	String dpi = request.getParameter(RENDERING_DPI);
	if (dpi != null && dpi.length() > 0) 
	{
	   	try
		{
			viewer.setRenderingDPI(Integer.valueOf(dpi).intValue());
		}
		catch(Exception e)
		{}
	}		
	//viewer.setProductLocale(userLocale);   	
}

void checkReportPart(CrystalReportViewerBase viewer, HttpServletRequest request, IReportSource rptSrc)
{
	String sParam_partContext = request.getParameter(PARAM_PARTCONTEXT);
	String sParam_partName = request.getParameter(PARAM_PARTNAME);

	// check if there is a parameter indicating to show the initial report part
	boolean bShowInitialReportPart = false;
	String sShowInitRptPart = request.getParameter(PARAM_SHOW_INITIALPART);
	if( sShowInitRptPart != null && sShowInitRptPart.equalsIgnoreCase("true"))
		bShowInitialReportPart = true;

 	// Check show particular part or not?  This takes precedence over show Initial Report part
	// check isReportPart?
	boolean bIsReportPart = isReportPart(request);

	if (bIsReportPart)
	{
		viewer.navigateTo (sParam_partContext, sParam_partName);
	}
	else if( bShowInitialReportPart )
	{
		// get the initial part from the report source, if it fails, don't do anything
		try
		{
			ReportPartIDs rptPartIds = rptSrc.getInitialReportPartEx(new RequestContext());

			if( rptPartIds != null && rptPartIds.size() > 0)
			{
				ReportPartID rptPartId = (ReportPartID) rptPartIds.get(0);
				viewer.navigateTo (rptPartId.getDataContext(), rptPartId.getName());
			}
		}
		catch( Exception e)
		{}
	}
}

void drillDownGroup(CrystalReportViewer viewer, HttpServletRequest request)
{
	String sParam_groupPathID = request.getParameter(PARAM_GROUPPATHID );

	if( sParam_groupPathID != null && sParam_groupPathID.length() > 0)
	{
		// get the initial group path from the request, if it fails, don't do anything
		try
		{
			GroupPath gPath = new GroupPath();
			gPath.fromString( sParam_groupPathID);
			if (gPath != null )
				viewer.drillDown( gPath, gPath.toString() );
		}
		catch( Exception e)
		{}
	}
}

public boolean isReportPart(HttpServletRequest request)
{	
	// check isReportPart?
	String sParam_partName = request.getParameter(PARAM_PARTNAME);
	boolean bIsReportPart = false;
	if (sParam_partName != null && sParam_partName.length() > 0)
	{
	    // if this is the first time loading this page, then navigate to the default report part
	    // we can figure that out by checking if there is a CrystalViewState parameter
	    // if there is, it means it was clicked
	    String checkCrystalViewState = request.getParameter("CrystalViewState");
	    String checkCrystalEventArgument = request.getParameter("CrystalEventArgument");
	    if( checkCrystalViewState != null &&
	        checkCrystalEventArgument != null )
	    {
	        bIsReportPart = false;
	    }
	    else
	    {
	        bIsReportPart = true;
	    }
	}
	return bIsReportPart;
}

public void WriteError( HttpServletResponse response, 
						HttpSession session, 
						String errorPrompt, 
						String errorMsg)
					throws Exception
{
	// this function may be called multiple times after an error occurs, so we need to check that
	// we don't commit the response more than once'
	if (!response.isCommitted())
	{
		session.setAttribute(RSB_SESS_ERRORPROMPT, errorPrompt);
		session.setAttribute(RSB_SESS_ERRORMSG, errorMsg);
		try
		{
			response.reset();
			// response.flushBuffer();	// for display the out.println() immediately.  This is causing the IllegalStateException...
			// DISPLAY THE CURRENT ERROR MESSAGE IN WINDOW
			clientRedirect(response, errorPagePath);
		}
		catch (Exception e)
		{
			// sendRedirect may fail and return java.lang.IllegalStateException, it is bceause
			// we could be hitting both sendRedirect more than once. If the first has been committed,
			// the second one will fail.
			throw new Exception ("Error in showing Error Page: " + e.toString());
		}
	}
}

public void WriteErrorCommit(HttpServletResponse response, HttpSession session, String msg)
throws Exception
{
	WriteError(response, session, L_COMMIT_ERROR, msg);
}

public void alertMessage (	javax.servlet.jsp.JspWriter out,
				HttpServletResponse response,
				String text)
throws Exception
{
	// note: Window Object --- a web browser window or frame
	//response.reset();	// erase any already-buffered HTML
	out.println ("<SCRIPT>" + "\n");
	out.println ("window.alert(\"" + text + "\");"  + "\n");
	out.println ("</SCRIPT>" + "\n");
	// Caused ~ASP 0156~Header Error~The HTTP headers are already written to the client browser. Any HTTP header modifications must be made before writing page content.
	//response.flushBuffer ();	// send buffered output to the client immediately
}

public void clientRedirect (HttpServletResponse response, String url) throws Exception {
	PrintWriter out = response.getWriter();
	out.println("<HEAD>");
	out.println("<SCRIPT language=\"JavaScript\">");
	out.println("<!--");
	out.println("window.location=\"" + url + "\";");
	out.println("//-->");
	out.println("</SCRIPT>");
	out.println("</HEAD>");
}

%>