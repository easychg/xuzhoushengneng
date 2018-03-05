<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="manager_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<script type="text/javascript" src="lib/PIE_IE678.js"></script>
<![endif]-->
    <link href="static/h-ui/css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="static/h-ui.admin/css/H-ui.login.css" rel="stylesheet" type="text/css" />
    <link href="static/h-ui.admin/css/style.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.8/iconfont.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>中煤集团</title>
    <meta name="keywords" content="H-ui.admin v2.3,H-ui网站后台模版,后台模版下载,后台管理系统模版,HTML后台模版下载"/>
    <meta name="description" content="H-ui.admin v2.3，是一款由国人开发的轻量级扁平化网站后台模板，完全免费开源的网站后台管理系统模版，适合中小型CMS后台系统。"/>
    <style type="text/css">
        .header {
            top: 0;
            height: 60px;
            background: #426374 no-repeat 0 center;
            text-align: center;
            font-size: 40px;
            color: #fff;
        }
    </style>
</head>
<body>
 <form id="forma" runat="server">
    <div class="header">中煤集团管理系统</div>
    <div class="loginWraper">
        <div id="loginform" class="loginBox">
            
                <div class="row cl">
                    <label class="form-label col-xs-3" style="text-align:right"><i class="Hui-iconfont">&#xe60d;</i></label>
                    <div class="formControls col-xs-8">
                       <asp:TextBox ID="txt_name" runat="server" placeholder="账户" CssClass="input-text size-L"></asp:TextBox>
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-xs-3" style="text-align:right"><i class="Hui-iconfont">&#xe60e;</i></label>
                    <div class="formControls col-xs-8">
                       <asp:TextBox ID="txt_psw" runat="server" placeholder="密码" CssClass="input-text size-L" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="row cl">
                    <div class="formControls col-xs-8 col-xs-offset-3">
                        <input class="input-text size-L" runat="server" id="vCode" type="text" placeholder="验证码" onblur="if(this.value==''){this.value='验证码:'}" onclick="if (this.value == '验证码:') { this.value = ''; }" value="验证码:" style="width: 150px;"/>
                        <img src="handlerHelper.ashx?action=getVcode" id="verifyCode"/>
                        <a id="kanbuq" href="javascript:;" onclick="f_refreshtype()">看不清，换一张</a>
                    </div>
                </div>
                <div class="row cl">
                    <div class="formControls col-xs-8 col-xs-offset-3">
                       <asp:Button ID="btn_submit" runat="server" CssClass="btn btn-success radius size-L"
                        Text="&nbsp;登&nbsp;&nbsp;&nbsp;&nbsp;录&nbsp;" onclick="btn_submit_Click" />
                        <input name="" type="reset" class="btn btn-default radius size-L" value="&nbsp;取&nbsp;&nbsp;&nbsp;&nbsp;消&nbsp;"/>
                    </div>
                </div>
           
        </div>
    </div>
    <div class="footer">中煤集团</div>
    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="static/h-ui/js/H-ui.js"></script>
     <script type="text/javascript">
         //点击切换验证码
         function f_refreshtype() {
             var Image1 = document.getElementById("verifyCode");
             if (Image1 != null) {
                 Image1.src = Image1.src + "&t=" + new Date().getMilliseconds();
             }
         }
     </script>
     </form>
</body>
</html>
