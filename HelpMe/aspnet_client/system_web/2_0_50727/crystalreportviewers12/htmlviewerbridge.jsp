<%@ page language="java" contentType="text/html; charset=utf-8" %>
<%@ include file="viewrpt_utils.jsp" %>
 
<%
	SetPageExpiry(response);
	jspInit();
	
	String queryStr = request.getQueryString();

	// (ADAPT00887325) For webshpere, request.getQueryString() may return null. For detail
	// please refer to http://www-1.ibm.com/support/docview.wss?uid=swg21215961 or google 
	// on 'request.getAttribute("javax.servlet.forward.query_string")'
	if (queryStr == null)
		queryStr = (String)request.getAttribute("javax.servlet.forward.query_string");

	String initStr = request.getParameter(INIT);  
	if (initStr != null)
	{
		String lInitStr = initStr.toLowerCase();
		int index = lInitStr.indexOf("java");
		if (index < 0)
			index = lInitStr.indexOf("actx");
		if (index >= 0)		
			clientRedirect(response,"../viewrpt.cwr?" + queryStr);
	}
	    
 	try
	{	
		String rptId = getReportId(request);
		IReportSource rptSrc = getReportSource(request, rptId);
		if (rptSrc == null)
		{  			
			clientRedirect(response, "../viewrpt.cwr?" + queryStr + "&init=html");
		} else {		
			CrystalReportViewer viewer = new CrystalReportViewer();  
			viewer.setProductLocale(rptSrc.getProductLocale());
			viewer.setReportSource(rptSrc);
			viewer.setURI(HTMLVIEWER + "?" + queryStr);
					     	   	
			initializeViewer(viewer, request, response, rptId);
			setViewerOptions(viewer, request);
			checkReportPart(viewer, request, rptSrc);
			drillDownGroup(viewer, request);
						 
			String sParam_Zoom = request.getParameter("sZoom");
			if( sParam_Zoom != null )
			{
				viewer.setZoomFactor(Integer.valueOf(sParam_Zoom).intValue());
			}
						
			JspWriter jspWriter;
			if (ViewerOptions.isUsejspwriter() )
				jspWriter = out;
			else
				jspWriter = null;
			viewer.processHttpRequest(request, response, application, jspWriter);	
			viewer = null; 	
		}
	}
	catch(Exception e)
	{
		WriteError(response, session, L_COMMIT_ERROR, e.getMessage());
	}
	
	jspDestroy();
%>