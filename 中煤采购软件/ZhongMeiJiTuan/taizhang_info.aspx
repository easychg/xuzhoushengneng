<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taizhang_info.aspx.cs" Inherits="taizhang_info" %>

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
<style type="text/css">
.page-container
{
    overflow-x:scroll;
    }
    .table tr td
    {
        width:300px;
        text-align:center;
        }
        .table tr
        {
            height:30px;
            }
        .input-text
        {
            width:120px;
            }
            .textarea
            {
                width:300px;
                }
</style>
</head>
<body>
<div class="page-container">
<form id="Form1" runat="server" class="form form-horizontal">
<table class="table table-border table-bordered">
<tr>
<td>月份</td><td>使用单位</td><td>计划编号</td><td>资金来源</td><td>计划类型</td><td>询价员</td><td>采购编号</td><td>物资属性</td><td>物资编码</td><td>定价物资名称</td><td>定价规格型号</td><td>定价单位</td><td>定价数量</td><td>已到货</td><td>含税单价（元）</td><td>含税金额（元）</td><td>税率（%）</td><td>采购方式</td><td>所属协议号</td><td>订单编号</td><td>订货依据</td><td>交货地点</td><td>制造商</td><td>物资品牌</td><td>供货厂商</td><td>电商平台</td><td>网店名称</td><td>付款方式</td><td>厂商电话</td><td>订单日期</td><td>交货期</td><td>应到时间</td><td>使用寿命</td><td>质保期</td><td>其他</td><td>备注</td><td>操作人员</td><td>订单状态</td><td>实到时间</td><td>操作</td>
</tr>
<tr  style="height:30px;">
<td><asp:TextBox ID="yuefen" runat="server" class="input-text" ></asp:TextBox></td>
<td><asp:TextBox ID="shiyongdanwei" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="jihuabianhao" runat="server" class="input-text"></asp:TextBox></td>
<td> <asp:DropDownList ID="ddl_zijinlaiyuan" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:DropDownList ID="ddl_jihualeixing" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:TextBox ID="xunjiayuan" runat="server" class="input-text"></asp:TextBox></td>

<td><asp:TextBox ID="caigoubianhao" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:DropDownList ID="ddl_wuzishuxing" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:TextBox ID="wuzibianma" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="dingjiawuzimingcheng" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="dingjiaguigexinghao" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="dingjiadanwei" runat="server" class="input-text"></asp:TextBox></td>

<td><asp:TextBox ID="dingjiashuliang" runat="server" onkeyup="if(isNaN(value))execCommand('undo');cal()" Text="0" onafterpaste="if(isNaN(value))execCommand('undo')" class="input-text"></asp:TextBox></td>
<td><asp:Label ID="lblyidaohuo" Width="100" runat="server" Text="0.00"></asp:Label></td>
<td><asp:TextBox ID="hanshuidanjia" runat="server" onkeyup="if(isNaN(value))execCommand('undo');cal()" Text="0" onafterpaste="if(isNaN(value))execCommand('undo')" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="hanshuijine" runat="server" onkeyup="if(isNaN(value))execCommand('undo');cal()" Text="0" onafterpaste="if(isNaN(value))execCommand('undo')" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="shuilv" runat="server" onkeyup="if(isNaN(value))execCommand('undo');cal()" Text="0" onafterpaste="if(isNaN(value))execCommand('undo')" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="caigoufangshi" runat="server" class="input-text" ></asp:TextBox></td>
<td><asp:TextBox ID="suoshuxieyihao" runat="server" class="input-text"></asp:TextBox></td>

<td><asp:TextBox ID="dingdanbianhao" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="dinghuoyiju" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:DropDownList ID="ddl_jiaohuodidian" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:TextBox ID="zhizaoshang" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="wuzipinpai" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="gonghuochangshang" runat="server" class="input-text"></asp:TextBox></td>

<td><asp:DropDownList ID="ddl_dianshangpingtai" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:TextBox ID="wangdianmingcheng" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:DropDownList ID="ddl_fukuanfangshi" runat="server" CssClass="select" Width="200" Height="30"></asp:DropDownList></td>
<td><asp:TextBox ID="changshangdianhua" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="dingdanriqi" runat="server" class="input-text Wdate" onfocus="WdatePicker({ dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
<td><asp:TextBox ID="jiaohuoqi" runat="server" class="input-text"></asp:TextBox></td>

<td><asp:TextBox ID="yingdaoshijian" runat="server" class="input-text Wdate" onfocus="WdatePicker({ dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>

<td><asp:TextBox ID="shiyongshouming" runat="server" class="input-text" ></asp:TextBox></td>
<td><asp:TextBox ID="zhibaoqi" runat="server" class="input-text"></asp:TextBox></td>
<td><asp:TextBox ID="qita" runat="server" class="input-text"></asp:TextBox></td>
<td><textarea id="beizhu" runat="server"   class="textarea"  placeholder="说点什么...最少输入10个字符" datatype="*10-100" style="height:30px;" dragonfly="true" nullmsg="备注不能为空！" onKeyUp="$.Huitextarealength(this,200)"></textarea></td>

<td><asp:Label ID="lbl_user" runat="server" Width="200" Text=""></asp:Label></td>
<td><asp:DropDownList ID="ddl_state" runat="server" CssClass="select" Width="200" Height="30">
<asp:ListItem Value="0" Text="未到货"></asp:ListItem>
<asp:ListItem Value="1" Text="全部到货"></asp:ListItem>
</asp:DropDownList></td>
<td><asp:TextBox ID="shidaoshijian" runat="server" class="input-text Wdate" onfocus="WdatePicker({ dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
<td><asp:LinkButton ID="lbtn_save" runat="server" CssClass="btn btn-primary radius" Text="保存" onclick="lbtn_save_Click"></asp:LinkButton></td>
</tr>
</table>
	
		<%--<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">月份：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">使用单位：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">计划编号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">资金来源：</label>
			<div class="formControls col-xs-8 col-sm-9">
           
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">计划类型：</label>
			<div class="formControls col-xs-8 col-sm-9">
            
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">询价员：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">采购编号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">物资属性：</label>
			<div class="formControls col-xs-8 col-sm-9">
            
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">物资编码：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">定价物资名称：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">定价规格型号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">定价单位：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">定价数量：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">含税单价（元）：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">含税金额（元）：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">税率（%）：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">采购方式：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>

        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">所属协议号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
            </div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">订单编号：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
            </div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">订货依据：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
        </div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">交货地点：</label>
			<div class="formControls col-xs-8 col-sm-9">
            
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">制造商：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">物资品牌：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">供货厂商：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">电商平台：</label>
			<div class="formControls col-xs-8 col-sm-9">
            
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">网店名称：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">付款方式：</label>
			<div class="formControls col-xs-8 col-sm-9">
            
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">厂商电话：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">订单日期：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
        </div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">交货期：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>


        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">交货时间：</label>
			<div class="formControls col-xs-8 col-sm-9">
                <asp:TextBox ID="jiaohuoshijian" runat="server" class="input-text Wdate" onfocus="WdatePicker({ dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
			</div>
        </div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">应到时间：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">实到时间：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">使用寿命：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">质保期：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        <div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">其他：</label>
			<div class="formControls col-xs-8 col-sm-9">
                
			</div>
		</div>
        
		<div class="row cl">
			<label class="form-label col-xs-4 col-sm-2">备注：</label>
			<div class="formControls col-xs-8 col-sm-9">
				
			</div>
		</div>
		
		<div class="row cl">
			<div class="col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-2">
            
			</div>
		</div>--%>
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
