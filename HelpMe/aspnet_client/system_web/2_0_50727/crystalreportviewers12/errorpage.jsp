<%--
	    (c) Business Objects 2004. All rights reserved.
--%>

<!--
	This is the JSP page to support CB0 Report Linking
-->
<%@ page session="true" %>
<%@ page language="java" contentType="text/html; charset=utf-8" %>
<HTML>
<HEAD>

<%@ include file="viewrpt_utils.jsp" %>

<%
// PREVENT BROWSER FROM CACHING THE PAGE
	response.setHeader("Cache-Control","no-cache");
	response.setHeader("Pragma","no-cache");
	response.setDateHeader ("Expires", -1);
%>

</HEAD>
<BODY>

<table width='100%' border='0' cellpadding='0' cellspacing='0'>
<tr><td><span><h2>&nbsp;Report Linking Error</h2></span></td>
<td align='right'><a href='javascript:window.history.back();'>Back&nbsp;</a></td></tr>
<tr><td colspan=2>&nbsp;</td></tr>
</table>

<p>

<table class='list' width='100%' border='0' cellpadding='3' cellspacing='0'>
<tr>
	<td class='list'>
<%
	// DISPLAY THE CURRENT ERROR MESSAGE IN WINDOW
	out.println("<span><h3>" + session.getAttribute(RSB_SESS_ERRORPROMPT) + "</h3></span>");
	out.println("<span>" + session.getAttribute(RSB_SESS_ERRORMSG) + "</span>");
%>
	</td>
</tr>
</table>

</BODY>
</HTML>
