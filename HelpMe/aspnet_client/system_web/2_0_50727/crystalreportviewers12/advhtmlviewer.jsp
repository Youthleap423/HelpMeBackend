<%--
	    (c) Business Objects 2006. All rights reserved.
--%>

<%@ page import="com.crystaldecisions.report.web.viewer.CrystalReportInteractiveViewer" %>
<%@ page import="com.crystaldecisions.sdk.occa.report.lib.ReportSDKExceptionBase" %> 
<%@ page language="java" contentType="text/html; charset=utf-8" %>
             
<%@ include file="viewrpt_utils.jsp" %>

<%!

void setAdvancedViewerOptions(CrystalReportInteractiveViewer viewer, HttpServletRequest request)
{
	viewer.setHasBooleanSearchButton(true);
	viewer.setEnableBooleanSearch(true);
	viewer.setShowAdvSearchFieldsTab(true);
	viewer.setShowAdvSearchConditionsTab(true);
	viewer.setHasHelpButton(true);
	
	String sParam_Zoom = request.getParameter("sZoom");
	if( sParam_Zoom != null )
	{
		viewer.setZoomFactor(Integer.valueOf(sParam_Zoom).intValue());
	}
}

boolean viewReport(HttpServletRequest request, 
				HttpServletResponse response,
				ServletContext application,
				JspWriter out,
				IReportSource rptSrc,
				String rptId,
				boolean checkReportPart) throws Exception
{
	boolean bIsReportPart = isReportPart(request);
	try {		
		CrystalReportInteractiveViewer viewer = new CrystalReportInteractiveViewer();	
		viewer.setProductLocale(rptSrc.getProductLocale());
		viewer.setReportSource(rptSrc);
		String queryStr = request.getQueryString();	

		// (ADAPT00887325) For webshpere, request.getQueryString() may return null. For detail
		// please refer to http://www-1.ibm.com/support/docview.wss?uid=swg21215961 or google 
		// on 'request.getAttribute("javax.servlet.forward.query_string")'
		if (queryStr == null)
			queryStr = (String)request.getAttribute("javax.servlet.forward.query_string");
	 		
		viewer.setURI(ADVHTMLVIEWER + "?" + queryStr);
	     
		initializeViewer(viewer, request, response, rptId);
		setViewerOptions(viewer, request);
		setAdvancedViewerOptions(viewer, request);		
		if (checkReportPart)
			checkReportPart(viewer, request, rptSrc);
		drillDownGroup(viewer, request);
	
		JspWriter jspWriter;
		if (ViewerOptions.isUsejspwriter() )
		    jspWriter = out;
		else
		    jspWriter = null;
		viewer.processHttpRequest(request, response, application, jspWriter);
		viewer = null;
	}
	catch(ReportSDKExceptionBase e)
	{
		response.reset();
		if (bIsReportPart)
			return false;
		else
			throw new Exception (e.toString());
	}
	catch(Exception e)
	{
		response.reset();		
		if (bIsReportPart)
			return false;
		else
			throw (e);
	}

	return true;
}
%>
 
<%
	SetPageExpiry(response);
	jspInit();		
		
	try
	{
		String rptId = getReportId(request);
		IReportSource rptSrc = getReportSource(request, rptId);
		if (rptSrc == null)
		{  
			String queryStr = request.getQueryString();
			clientRedirect(response, "../viewrpt.cwr?" + queryStr + "&init=advhtml");	
		} else {
		
			boolean succeeded = viewReport(request, response, application, out, rptSrc, rptId, true);
			if (!succeeded )
			{
				alertMessage (out, response, "Report part navigation failed! Default to the first page of the report.");
				viewReport(request, response, application, out, rptSrc, rptId, false);
			}
		}
	} 
	catch(Exception e)
	{
		WriteErrorCommit (response, session, e.getMessage());
	}
	 
	jspDestroy();
%>
