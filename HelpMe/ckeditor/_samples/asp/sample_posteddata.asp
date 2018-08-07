<%@  codepage="65001" language="VBScript" %>
<% Option Explicit %>
<!DOCTYPE html>
<!--
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
-->
<html lang="en">
<head>
	<title>Sample - CKEditor</title>
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<link type="text/css" rel="stylesheet" href="../sample.css" />
</head>
<body>
	<h1 class="samples">
		CKEditor - Posted Data
	</h1>
	<table border="1" cellspacing="0" id="outputSample">
		<colgroup><col width="100" /></colgroup>
		<thead>
			<tr>
				<th>Field&nbsp;Name</th>
				<th>Value</th>
			</tr>
		</thead>
			<%
			Dim sForm
			For Each sForm in Request.Form
			%>
			<tr>
				<th><%=Server.HTMLEncode( sForm )%></th>
				<td><pre class="samples"><%=Server.HTMLEncode( Request.Form(sForm) )%></pre></td>
			</tr>
			<% Next %>
	</table>
	<div id="footer">
		<hr />
		<p>
			CKEditor - The text editor for Internet - <a class="samples" href="http://ckeditor.com/">http://ckeditor.com</a>
		</p>
		<p id="copy">
			Copyright &copy; 2003-2012, <a class="samples" href="http://cksource.com/">CKSource</a> - Frederico Knabben. All rights reserved.
		</p>
	</div>
</body>
</html>
