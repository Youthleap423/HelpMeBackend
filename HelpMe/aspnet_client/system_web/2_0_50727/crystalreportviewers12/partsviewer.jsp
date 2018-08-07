<%--
	    (c) Business Objects 2004. All rights reserved.
--%>

<%@ page session="true" %>

<%@ page import="com.crystaldecisions.report.web.viewer.CrystalReportPartsViewer" %>
<%@ page import="com.crystaldecisions.sdk.occa.report.partsdefinition.*" %>
<%@ page language="java" contentType="text/html; charset=utf-8" %>

<%@ include file="viewrpt_utils.jsp" %>

<%!
// To parse string like "Field3;Field15" and return array of the elements
public java.util.List parseReportPartsString (	String sDTString,
						javax.servlet.jsp.JspWriter out)
throws Exception
{
	//out.println ("parseReportPartsString: " + sDTString + "<BR>");
	java.util.List mcArray = new java.util.ArrayList ();
	int index = -1;
	String sString = sDTString;
	do
	{
		index = sString.indexOf (";");
		String parts = sString;	// if index is -1, by default is the last value
		if (index != -1)
			parts = sString.substring (0, index);
		mcArray.add (parts.substring(0, parts.length()));
		sString = sString.substring (index+1);
		//alertMessage (out, response, "The parseReportPartsString added: " + parts);
	}
	while (index != -1);

	return mcArray;
}
%>

<%
	SetPageExpiry(response);
	jspInit();

	// TOP LEVEL TRY
	try
	{
		String rptId = getReportId(request);
		IReportSource rptSrc = getReportSource(request, rptId);
		String queryStr = request.getQueryString();

		// (ADAPT00887325) For webshpere, request.getQueryString() may return null. For detail
		// please refer to http://www-1.ibm.com/support/docview.wss?uid=swg21215961 or google 
		// on 'request.getAttribute("javax.servlet.forward.query_string")'
		if (queryStr == null)
			queryStr = (String)request.getAttribute("javax.servlet.forward.query_string");

		if (rptSrc == null)
		{  			
			clientRedirect(response, "../viewrpt.cwr?" + queryStr + "&init=part");	
		} else {		
			CrystalReportPartsViewer partsViewer = new CrystalReportPartsViewer();
			
			partsViewer.setReportSource(rptSrc);
			partsViewer.setURI(PARTSVIEWER + "?" + queryStr);
			
			initializeViewer(partsViewer, request, response, rptId);
		
			String sParam_partContext = request.getParameter(PARAM_PARTCONTEXT);
			String sParam_partName = request.getParameter(PARAM_PARTNAME);
		
			// To specify which parts of a report are displayed
			ReportPartsDefinition definition = new ReportPartsDefinition();
			if (sParam_partContext != null &&
			    sParam_partName != null && 
			    sParam_partContext.compareToIgnoreCase("default") != 0 && 
			    sParam_partName.compareToIgnoreCase("default") != 0)
			{
				ReportPartNodes nodes = new ReportPartNodes();
				// Check multiple parts speecified
				java.util.List sArray = parseReportPartsString (sParam_partName, out);
				for (int x=0; x < sArray.size(); x++)
				{
					//alertMessage (out, response, (String) sArray.get(x));
					ReportPartNode node = new ReportPartNode();
					node.setName ((String) sArray.get(x));
					nodes.add (node);
				}
				definition.setDataContext (sParam_partContext);
				definition.setReportPartNodes (nodes);
			}
		
			// VIEW WITH REPORT PARTS VIEWER
			partsViewer.setReportParts (definition);
			partsViewer.setHasBorder (false);
			partsViewer.setPreserveLayout (false);			 
			
			String sParam_Zoom = request.getParameter("sZoom");
			if( sParam_Zoom != null )
			{
				partsViewer.setZoomFactor(Integer.valueOf(sParam_Zoom).intValue());
			} 		 
		
			ServletContext svlContext = getServletConfig().getServletContext();
			JspWriter jspWriter;
			if ( ViewerOptions.isUsejspwriter())
			    jspWriter = out;
			else
			    jspWriter = null;
			partsViewer.processHttpRequest(request, response, svlContext, jspWriter);
		}        
	}
	// TOP LEVEL CATCH
	catch(Exception e)
	{
		WriteErrorCommit (response, session, e.getMessage());
	}
	
	
	jspDestroy();
%>
