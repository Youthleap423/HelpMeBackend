<%@ page import="com.businessobjects.crystalreports.reportsourcebridge.ReportSourceBridge,
                 com.businessobjects.crystalreports.reportsourcebridge.RSBridgeResourceManager,
                 java.util.ResourceBundle,
                 java.util.HashMap,
                 java.util.Enumeration,
                 com.crystaldecisions.sdk.framework.ISessionMgr,
                 com.crystaldecisions.sdk.framework.CrystalEnterprise,
                 com.crystaldecisions.sdk.plugin.CeProgID"%>

<%@ page language="java" contentType="text/html; charset=utf-8" %>
<HTML>
<HEAD>

<%
  //PREVENT BROWSER FROM CACHING THE PAGE
  response.setHeader("Cache-Control","no-cache"); //HTTP 1.1
  response.setHeader("Pragma","no-cache"); //HTTP 1.0
  response.setDateHeader ("Expires", 0); //prevents caching at the proxy server
  response.setHeader("Expires", "0");

  String lastusr = ReportSourceBridge.getLastUser(request);
  String lastcms = ReportSourceBridge.getLastCMS(request);
  String lastaut = ReportSourceBridge.getLastAuth(request);
 
  ResourceBundle resource = RSBridgeResourceManager.getResource(request.getLocale());
  
  String viewerPath = "." + pageContext.getServletContext().getInitParameter ("path.dhtmlViewer");
  
  String queryString = request.getQueryString();    

  // (ADAPT00887325) For webshpere, request.getQueryString() may return null. For detail
  // please refer to http://www-1.ibm.com/support/docview.wss?uid=swg21215961 or google 
  // on 'request.getAttribute("javax.servlet.forward.query_string")'
  if (queryString == null)
    queryString = (String)request.getAttribute("javax.servlet.forward.query_string");

  String newQueryString = "?" + ReportSourceBridge.encodeQueryString(queryString);  
%>
  <link rel="stylesheet" type="text/css" href="<%= viewerPath %>/css/default.css">
  <script language="javascript">
 var gCounter = 0;

encodeUTF8 = function(string) {
    var arr = [];
    var strLen = string.length;
    for(var i = 0; i < strLen; i++) {
        var c = string.charCodeAt(i);
        if(c < 0x80) {
            arr.push(c);
        }
        else if(c < 0x0800) {
            arr.push((c >> 6) | 0xc0);
            arr.push(c & 0x3f | 0x80);
        }
        else if(c < 0xd800 || c >= 0xe000) {
            arr.push((c >> 12) | 0xe0);
            arr.push((c >> 6) & 0x3f | 0x80);
            arr.push(c & 0x3f | 0x80);
        }
        else if(c < 0xdc00) {
            var c2 = string.charCodeAt(i + 1);
            if(isNaN(c2) || c2 < 0xdc00 || c2 >= 0xe000) {
                arr.push(0xef, 0xbf, 0xbd);
                continue;
            }
            i++;
            val = ((c & 0x3ff) << 10) | (c2 & 0x3ff);
            val += 0x10000;
            arr.push((val >> 18) | 0xf0);
            arr.push((val >> 12) & 0x3f | 0x80);
            arr.push((val >> 6) & 0x3f | 0x80);
            arr.push(val & 0x3f | 0x80);
        }
        else {
            arr.push(0xef, 0xbf, 0xbd);
        }
    }
    return arr;
};

encodeBASE64 = function(byteArray) {
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var arr = [];
    var c1, c2, c3, e1, e2, e3, e4;
    var i = 0, arrLen = byteArray.length;
    
    while(i < arrLen) {
        c1 = byteArray[i++];
        c2 = byteArray[i++];
        c3 = byteArray[i++];
        
        e1 = c1 >> 2;
        e2 = ((c1 & 3) << 4) | (c2 >> 4);
        e3 = ((c2 & 15) << 2) | (c3 >> 6);
        e4 = c3 & 63;
        
        if (isNaN(c2)) {
            e3 = e4 = 64;
        } else if(isNaN(c3)) {
            e4 = 64;
        }
        arr.push(keyStr.charAt(e1));
        arr.push(keyStr.charAt(e2));
        arr.push(keyStr.charAt(e3));
        arr.push(keyStr.charAt(e4));
    }
    return arr.join('');
};


  //SUBMIT LOGON FORM
  function logon() { 	
   	var url = location.href;
  	var iq = url.indexOf('?');
  	if (iq != -1)
		url = url.substring(0, iq);
	url = url + "<%=newQueryString%>"; 

  if(gCounter == 0) {
   	    document.getElementById("apspassword").value = encodeBASE64(encodeUTF8(document.getElementById("apspassword").value));
   	    gCounter++;
   	}
   	document.getElementById("logonform").action = url;
   	document.forms["logonform"].submit();
  }
 </script>

</HEAD>
<BODY>
<form name="logonform" id="logonform" method="post">

  <table class="list" width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:white">
    <tr>
      <td class="list">
        <span class="listSelected"><%= resource.getString("logon.title") %></span><br>
        <hr size=0>
      </td>
      <td class="list">		
      <%  //add formdata 
      	if (request.getMethod().equalsIgnoreCase("POST"))
      	{
      		HashMap queryMap = ReportSourceBridge.parseQueryString(queryString, true);
			for (Enumeration paramNames = request.getParameterNames(); paramNames.hasMoreElements(); ) 
			{
				String key = paramNames.nextElement().toString();
			    String keyName = ReportSourceBridge.UTF8Decode(key);			
			    if (keyName.compareTo("apsuser") == 0 ||
			    	keyName.compareTo("apspassword") == 0 ||
					keyName.compareTo("cmsname") == 0 ||
					keyName.compareTo("apsauthtype") == 0 ||  
					keyName.compareTo("apstoken") == 0 || 					
					queryMap.containsKey(keyName))  //do not submit querystring argument
			    	continue;
			   	String value = ReportSourceBridge.UTF8Decode(request.getParameter(key));
				%>
				<input type="hidden" name="<%=ReportSourceBridge.HtmlEncode(keyName)%>" value="<%=ReportSourceBridge.HtmlEncode(value)%>">
				<%
		     }
        }
        %> 
        <input type="hidden" name="logonencrypted" value="true">
      </td>   
    </tr>
  </table>


  <br>
  <table class="list" cellpadding="3" cellspacing="0" border="0" width="100%">
    <tr>
      <td class="list" valign="top">
        <table class="list" cellpadding="3" cellsapcing="0" border="0">
          <tr>
            <td class="list"><%= resource.getString("logon.cms.name") %></td>
            <td class="list"><input type=text size=30 name="cmsname" value="<%= ReportSourceBridge.HtmlEncode(lastcms) %>"></td>
          </tr>
          <tr>
            <td class="list"><%= resource.getString("logon.username") %></td>
            <td class="list"><input type=text size=30 name="apsuser" id="apsuser" value="<%= ReportSourceBridge.HtmlEncode(lastusr) %>"></td>
          </tr>
          <tr>
            <td class="list"><%= resource.getString("logon.password") %></td>
            <td class="list"><input type=password size=30 name="apspassword" id="apspassword" value=""></td>
          </tr>
          <tr>
            <td class="list"><%= resource.getString("logon.authentication") %></td>
            <td class="list">
              <select size=1 name="apsauthtype" style="width:200px">

<%
  ISessionMgr sm = CrystalEnterprise.getSessionMgr();
  String[] authProgIds = sm.getInstalledAuthIDs();
  for(int i = 0; i < authProgIds.length; ++i)
  {
    String ptypename = authProgIds[i];
    String pname = sm.nameFromProgID(ptypename);
    out.write("<option value='" + ptypename + "'");
    if(lastaut.equals(ptypename))
      out.write(" selected");
    else if ((lastaut.length() == 0) && ptypename.equals(CeProgID.SEC_ENTERPRISE))
      out.write(" selected");
    out.write(">" + pname );
  }
%>

              </select>
            </td>
          </tr>
          <tr>
            <td class="list">&nbsp;</td>
            <td class="list">
            <table cellpadding=0 cellspacing=0 border=0>
<tr>
	
   <td><img src="<%= viewerPath %>/images/buttonl.gif"></td>
   <td class="clsButton" align=middle nowrap background="<%= viewerPath %>/images/buttonm.gif">
     <div class="clsButton">
     <a href="javascript:logon();"><%= resource.getString("logon.button.logon") %></a>
     </div>
   </td>
   <td><img src="<%= viewerPath %>/images/buttonr.gif"></td> 
</tr>            
             
            </table>
            </td>
          </tr>
        </table>
      </td>
      <td valign=top>
      <!-- Error Message Here -->

<%
  //GET ANY ERROR MESSAGE THAT MAY BE STORED IN OUR SESSION VARIABLE
  Object errObj = session.getAttribute("ErrMessage");
  if (errObj != null)
  { 	
  	  String strErrMessage = (String) errObj;
  	  session.removeAttribute("ErrMessage");   	
%>	
        <span class='results' style='color:red'><b><%= resource.getString("logon.error.not.recognized") %></b>
          <ul style='margin-top:0;margin-bottom:0;'>
<%
    if(strErrMessage != "")
    {
%>
            <li><b><%= strErrMessage %></b>
<%
    }
%>
            <li><%= resource.getString("logon.error.please.check") %>
            <li><%= resource.getString("logon.error.reenter") %>
            <li><%= resource.getString("logon.error.unsure") %>
          </ul>
        </span>
<%
  }
  else
  {
%>
        &nbsp;
<%
  }
%>

      </td>
    </tr>
  </table>
</form>

</BODY>
</HTML>
