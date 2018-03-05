<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="manager_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KJ453煤矿压力监测系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="js/jquery.js"></script>
    <script src="js/cloud.js" type="text/javascript"></script>

<script language="javascript">
    $(function () {
        $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
        $(window).resize(function () {
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
        })
    });

    $(document).ready(function () {
        $(".Button1").click
  (function () {
      $(".tip").fadeIn(200);
  });

        $(".tiptop a").click(function () {
            $(".tip").fadeOut(200);
        });

        $(".sure").click(function () {
            $(".tip").fadeOut(100);
        });

        $(".cancel").click(function () {
            $(".tip").fadeOut(100);
        });

    });

</script> 
</head>
<body autocomplete="off"   style="background-color:#1c77ac; background-image:url(images/light.png); background-repeat:no-repeat; background-position:center top; overflow:hidden;">
    <form id="form1" runat="server">
      <div id="mainBody">
      <div id="cloud1" class="cloud"></div>
      <div id="cloud2" class="cloud"></div>
    </div>  


<div class="logintop">    
<%--    <span style="width:400px;">
    <a href="http://www.cumtsn.com" target="_blank">徐州圣能科技有限公司</a>制作 
    </span>    --%>
  <%--  <ul>
    <li><a href="#">回首页</a></li>
    <li><a href="#">帮助</a></li>
    <li><a href="#">关于</a></li>
    </ul>    --%>
    </div>
    
    <div class="loginbody">
    
    <span class="systemlogo"></span> 
       
    <div class="loginbox">
    
    <ul>
    <li>
 <%--   <div>
    <table>
    
    </table>
    </div>--%>
    <input id="txt_name" autocomplete="off"   name="" runat="server" type="text" class="loginpwd" value="用户名称" onclick="JavaScript:this.value=''"/></li>
    <li>
        <asp:TextBox ID="txt_psw" runat="server" CssClass="loginpwd" 
            TextMode="Password"></asp:TextBox>
   <%-- <input id="userpwd" name=""   type="password"  runat="server"  class="loginpwd" value="用户密码"  onclick="JavaScript:this.value=''"/>--%></li>
    <li>
        
        <asp:Button ID="Button1" runat="server" Text="登录"  CssClass="loginbtn" 
            onclick="btn_submit_Click"/>
   <%-- <input name="" type="button" class="loginbtn" value="登录"  />--%>
   <%-- <label><input name="" type="checkbox" value="" checked="checked" />记住密码</label><label><a href="#">忘记密码？</a></label>--%>
    </li>
    </ul>
    
    
    </div>
    
    </div>
    
    
    
    <div class="loginbm">版权所有    <a href="http://www.cumtsn.com" target="_blank">徐州圣能科技有限公司</a> </div>
	<div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
      <div class="tip">
    	<div class="tiptop"><span>提示信息</span><a></a></div>
        
      <div class="tipinfo">
        <span><img src="images/ticon.png" /></span>
        <div class="tipright">
        <p>是否确认对信息的修改 ？</p>
        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
        </div>
        </div>
        
        <div class="tipbtn">
        <input name="" type="button"  class="sure" value="确定" />&nbsp;
        <input name="" type="button"  class="cancel" value="取消" />
        </div>
    
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $(".logintop").hide();
        })
    </script>
</body>
</html>
