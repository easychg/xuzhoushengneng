<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_center.aspx.cs" Inherits="manager_user_center" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="Cache-Control" content="no-siteapp" />
<LINK rel="Bookmark" href="/favicon.ico" >
<LINK rel="Shortcut Icon" href="/favicon.ico" />
<!--[if lt IE 9]>
<script type="text/javascript" src="http://lib.h-ui.net/html5.js"></script>
<script type="text/javascript" src="http://lib.h-ui.net/respond.min.js"></script>
<script type="text/javascript" src="http://lib.h-ui.net/PIE_IE678.js"></script>
<![endif]-->
<link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="lib/icheck/icheck.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.1/iconfont.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.7/iconfont.css" />
<!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
<!--/meta 作为公共模版分离出去-->

<title>基本信息 - 基本信息 </title>

</head>
<body>
<div class="pd-20">
	<form runat="server" class="form form-horizontal" id="form1">
	<div class="row cl">
		<label class="form-label col-xs-4 col-sm-3" style="width:200px"><span class="c-red"></span>用户名：</label>
		<div class="formControls col-xs-8 col-sm-3">
			<asp:Label runat="server"   id="lblacc" name="txtcomname"></asp:Label>
		</div>
	</div>
	<div class="row cl">
		<label class="form-label col-xs-4 col-sm-3" style="width:200px"><span class="c-red">*</span>密码：</label>
		<div class="formControls col-xs-8 col-sm-3">
			<asp:TextBox runat="server" class="input-text" TextMode="Password" AutoComplete="off" placeholder="不输入则不修改密码" id="txtpass1" name="password"></asp:TextBox>
		</div>
	</div>
	<div class="row cl">
		<label class="form-label col-xs-4 col-sm-3" style="width:200px"><span class="c-red">*</span>确认密码：</label>
		<div class="formControls col-xs-8 col-sm-3">
			<asp:TextBox runat="server" class="input-text" TextMode="Password"  placeholder="不输入则不修改密码"  id="txtpass2" ></asp:TextBox><label id="lblemail" style="color:red"></label>
		</div>
	</div>
        <div class="row cl">
		<label class="form-label col-xs-4 col-sm-3" style="width:200px"><span class="c-red"></span>角色名：</label>
		<div class="formControls col-xs-8 col-sm-3">
            <label runat="server" id="lbljuese" ></label>
		</div>
	</div>
	<div class="row cl">
        <label class="form-label col-xs-4 col-sm-3"></label>
		<div class="col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-3">
			<asp:Button runat="server" ID="btnsubmit" class="btn btn-primary radius" OnClick="btnsubmit_Click" OnClientClick="return yanzheng();" Text="&nbsp;&nbsp;提交&nbsp;&nbsp;"/>
		</div>
	</div>
	</form>
</div>

<!--_footer 作为公共模版分离出去--> 
<script src="lib/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="lib/layer/2.1/layer.js"></script> 
<script type="text/javascript" src="lib/icheck/jquery.icheck.min.js"></script> 
<script type="text/javascript" src="lib/jquery.validation/1.14.0/jquery.validate.min.js"></script> 
<script type="text/javascript" src="lib/jquery.validation/1.14.0/validate-methods.js"></script> 
<script type="text/javascript" src="lib/jquery.validation/1.14.0/messages_zh.min.js"></script> 
<script type="text/javascript" src="static/h-ui/js/H-ui.js"></script> 
<script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script> 
<!--/_footer /作为公共模版分离出去--> 

<!--请在下方写此页面业务相关的脚本--> 
<script type="text/javascript">

    function yanzheng() {
        var pass1 = $("#txtpass1").val();
        var pass2 = $("#txtpass2").val();
        if (pass1 == "" && pass2 == "") {
            alert("密码不能为空!");
            return false;
        }
        if (pass1 != "" || pass2 != "") {
            if (pass1 != pass2) {
                alert("两次密码不一致!");
                return false;
            }
        }
    }


</script> 
<!--/请在上方写此页面业务相关的脚本-->
</body>
</html>