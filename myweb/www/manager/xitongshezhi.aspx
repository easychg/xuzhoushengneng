<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="xitongshezhi.aspx.cs" Inherits="manager_xitongshezhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<script type="text/javascript" src="lib/PIE_IE678.js"></script>
<![endif]-->
    <link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="lib/icheck/icheck.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.1/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="css/post-ui7_v20150309143458.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="kindeditor/plugins/code/prettify.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>添加文章</title>
</head>
<body>
    <div class="pd-20">
        <form runat="server" id="forma">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">title：</label>
                <div class="formControls col-8">
                    <asp:TextBox ID="txttitle" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2" ></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">keywords：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtkeywords"  runat="server" multiple="multiple" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">description：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtdescription" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"><span id="sppwd" style="color:red;"></span></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">版权和备案号代码：</label>
                <div class="formControls col-8">
                   <textarea name="" style="width:100%;height:200px" id="txtbeian" runat="server" class="textarea" placeholder=""></textarea>
                </div>
                <div class="col-2"><span id="Span1" style="color:red;"></span></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <div class="col-9 col-offset-2">
             <asp:Button ID="btnok" runat="server" CssClass="btn btn-primary radius" Text="&nbsp;&nbsp;提交&nbsp;&nbsp;" OnClick="btnok_Click" OnClientClick="return yanzheng()" />
                </div>
            </div>
        </form>
    </div>
    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="lib/icheck/jquery.icheck.min.js"></script>
    <script type="text/javascript" src="lib/Validform/5.3.2/Validform.min.js"></script>
    <script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
    <script type="text/javascript" src="js/H-ui.js"></script>
    <script type="text/javascript" src="js/H-ui.admin.js"></script>

    <script type="text/javascript">
        function yanzheng() {
//            if ($("#ddl_nav0").val() == "0") {
//                alert("导航栏不能为空");
//                return false;
//            }
//            if ($("#txttitle").val() == "") {
//                alert("标题不能为空");
//                return false;
//            }
//            if ($("#txtauthor").val() == "") {
//                alert("作者不能为空");
//                return false;
//            }
//            if ($("#txtdescr").val() == "") {
//                alert("简介不能为空");
//                return false;
//            }
        }
        
</script>
</body>
</html>
