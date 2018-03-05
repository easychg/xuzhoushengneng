<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fenzhanjiduankou_info.aspx.cs" Inherits="fenzhanjiduankou_info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
<meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="Cache-Control" content="no-siteapp" />
<link rel="Bookmark" href="/favicon.ico" >
<link rel="Shortcut Icon" href="/favicon.ico" />
<!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5shiv.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>

<![endif]-->
<link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
<link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.8/iconfont.css" />

<link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
<!--[if IE 6]>
<script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
<!--/meta 作为公共模版分离出去-->

<link href="lib/webuploader/0.1.5/webuploader.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="page-container">
	<form id="Form1" runat="server" class="form form-horizontal">
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">所属测区：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_AreaInfo" runat="server" CssClass="select" Height="30" Width="200">
            </asp:DropDownList>
			</div>
		</div>
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">面巷信息：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_WorkfaceInfo" runat="server" CssClass="select" Height="30" Width="200">
            </asp:DropDownList>
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">分站号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_fenzhanhao" runat="server" CssClass="select" Height="30" Width="200">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                <asp:ListItem Text="6" Value="6"></asp:ListItem>
            </asp:DropDownList>
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">端口号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_duankouhao" runat="server" CssClass="select" Height="30" Width="200">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="COM1" Value="COM1"></asp:ListItem>
                <asp:ListItem Text="COM2" Value="COM2"></asp:ListItem>
                <asp:ListItem Text="COM3" Value="COM3"></asp:ListItem>
                <asp:ListItem Text="COM4" Value="COM4"></asp:ListItem>
                <asp:ListItem Text="COM5" Value="COM5"></asp:ListItem>
                <asp:ListItem Text="COM6" Value="COM6"></asp:ListItem>
                <asp:ListItem Text="COM7" Value="COM7"></asp:ListItem>
                <asp:ListItem Text="COM8" Value="COM8"></asp:ListItem>
                <asp:ListItem Text="COM9" Value="COM9"></asp:ListItem>
                <asp:ListItem Text="COM10" Value="COM10"></asp:ListItem>
                <asp:ListItem Text="COM11" Value="COM11"></asp:ListItem>
                <asp:ListItem Text="COM12" Value="COM12"></asp:ListItem>
                <asp:ListItem Text="COM13" Value="COM13"></asp:ListItem>
                <asp:ListItem Text="COM14" Value="COM14"></asp:ListItem>
                <asp:ListItem Text="COM15" Value="COM15"></asp:ListItem>
                <asp:ListItem Text="COM16" Value="COM16"></asp:ListItem>
            </asp:DropDownList>
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">波特率：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_botelv" runat="server" CssClass="select" Height="30" Width="200">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="300" Value="300"></asp:ListItem>
                <asp:ListItem Text="600" Value="600"></asp:ListItem>
                <asp:ListItem Text="1200" Value="1200"></asp:ListItem>
                <asp:ListItem Text="2400" Value="2400"></asp:ListItem>
                <asp:ListItem Text="4800" Value="4800"></asp:ListItem>
                <asp:ListItem Text="9600" Value="9600"></asp:ListItem>
                <asp:ListItem Text="19200" Value="19200"></asp:ListItem>
                <asp:ListItem Text="115200" Value="115200"></asp:ListItem>
            </asp:DropDownList>
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">使用状态：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:DropDownList ID="ddl_shiyongzhuangtai" runat="server" CssClass="select" Height="30" Width="200">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
            </asp:DropDownList>
			</div>
		</div>
		<div class="row cl">
			<div class="col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-2">
            <asp:LinkButton ID="lbtn_save" runat="server" CssClass="btn btn-success radius" 
                    Text="保存" onclick="lbtn_save_Click"></asp:LinkButton>
			</div>
		</div>
	</form>
</div>

<!--_footer 作为公共模版分离出去-->
<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script> 
<script type="text/javascript" src="lib/layer/2.4/layer.js"></script>
<script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script> 
<script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script> <!--/_footer 作为公共模版分离出去-->

<!--请在下方写此页面业务相关的脚本-->
<script type="text/javascript" src="lib/My97DatePicker/4.8/WdatePicker.js"></script>
<script type="text/javascript" src="lib/jquery.validation/1.14.0/jquery.validate.js"></script> 
<script type="text/javascript" src="lib/jquery.validation/1.14.0/validate-methods.js"></script> 
<script type="text/javascript" src="lib/jquery.validation/1.14.0/messages_zh.js"></script>
<script type="text/javascript" src="lib/webuploader/0.1.5/webuploader.min.js"></script> 

<script type="text/javascript">
    $(function () {
        $('.skin-minimal input').iCheck({
            checkboxClass: 'icheckbox-blue',
            radioClass: 'iradio-blue',
            increaseArea: '20%'
        });
</script>
</body>
</html>