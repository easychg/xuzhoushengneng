<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taizhang_genzong_list.aspx.cs" Inherits="taizhang_genzong_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
<meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="Cache-Control" content="no-siteapp" />
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
<title>用户管理</title>
</head>
<body>
<form id="Form1" runat="server">
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 物资采购数据管理 <span class="c-gray en">&gt;</span> 物资采购跟踪列表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">
	<div class="text-c"> 应到货日期范围：
		<input type="text" onfocus="WdatePicker({ maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}' })" id="datemin" class="input-text Wdate" style="width:120px;" runat="server" />
		-
		<input type="text" onfocus="WdatePicker({ minDate:'#F{$dp.$D(\'datemin\')}' })" id="datemax" class="input-text Wdate" style="width:120px;" runat="server" />
        订单状态：<asp:DropDownList ID="ddl_state" runat="server" CssClass="select" Width="200" Height="30">
        <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
        <asp:ListItem Value="2" Text="未到货"></asp:ListItem>
        <asp:ListItem Value="3" Text="超期"></asp:ListItem>
        </asp:DropDownList>
        订单编号：<input type="text" class="input-text" style="width:250px" placeholder="输入订单编号" id="txtdingdanbianhao" runat="server" />
        计划编号：<input type="text" class="input-text" style="width:250px" placeholder="输入计划编号" id="txtjihuabianhao" runat="server" />
        厂商名称：<input type="text" class="input-text" style="width:250px" placeholder="输入厂商名称" id="txtchangshangmingcheng" runat="server" />
        <asp:LinkButton ID="lbtn_search" runat="server" 
            CssClass="btn btn-success radius" Text="搜索" onclick="lbtn_search_Click"></asp:LinkButton>
		<%--
		<button type="submit" class="btn btn-success radius" id="" name=""> 搜索</button>--%>
	</div>
	<div class="cl pd-5 bg-1 bk-gray mt-20"> 
    <span class="l">
    
        延期交货明细表：
        <asp:LinkButton ID="lbtn_export" runat="server" CssClass="btn btn-primary radius" OnClientClick="return check()" Text="部分导出" onclick="lbtn_export_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_export_all" runat="server" CssClass="btn btn-primary radius" Text="全部导出" onclick="lbtn_export_all_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_daohuo" runat="server" CssClass="btn btn-danger radius" OnClientClick="return check()" Text="标记到货" onclick="lbtn_daohuo_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_delete" runat="server" CssClass="btn btn-danger radius" OnClientClick="return check()" Text="删除" onclick="lbtn_delete_Click"></asp:LinkButton>
    </span> 
    
    </div>
	<div class="mt-20">
	<table class="table table-border table-bordered table-hover table-bg table-sort">
		<thead>
			<tr class="text-c">
				<th width="25"><input type="checkbox" name="" value=""></th>
				<th width="30">序号</th>
				<th width="100">订单编号</th>
                <th width="100">计划编号</th>
				<th width="200">供货厂商</th>
				<th width="90">订货依据</th>
				<th width="150">采购方式</th>
				<th width="50">订单日期</th>
                <th width="50">是否全部到货</th>
                <th width="100">查看</th>
			</tr>
		</thead>
		<tbody>
        <asp:Repeater ID="rpt_taizhang" runat="server" 
                onitemcommand="rpt_taizhang_ItemCommand">
        <ItemTemplate>
        <tr class="text-c">
				<td><asp:CheckBox runat="server" ID="ckb" ToolTip='<%#Eval("dingdanbianhao") %>' /></td>
				<td width="30"><%#Container.ItemIndex+1 %></td>
				<td width="100"><%#Eval("dingdanbianhao") %></td>
                <td width="100"><%#Eval("jihuabianhao") %></td>
				<td width="200"><%#Eval("gonghuochangshang") %></td>
				<td width="90"><%#Eval("dinghuoyiju") %></td>
				<td width="150"><%#Eval("caigoufangshi") %></td>
				<td width="50"><%#Eval("dingdanriqi") %></td>
				<td width="50" style="color:<%#Eval("isfinished").ToString()=="0"?"green":"red" %>"><%#Eval("isfinished").ToString()=="0"?"是":"否" %></td>
                <td width="100">
                <a onclick="admin_role_edit('查看','taizhang_genzong_info.aspx?dingdanbianhao=<%#Eval("dingdanbianhao") %>','1')" class="btn btn-primary radius">查看</a>
                </td>
                </tr>
        </ItemTemplate>
        </asp:Repeater>
			
		</tbody>
	</table>
	</div>
</div>
</form>
<!--_footer 作为公共模版分离出去-->
<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script> 
<script type="text/javascript" src="lib/layer/2.4/layer.js"></script>
<script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script> 
<script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script> <!--/_footer 作为公共模版分离出去-->

<!--请在下方写此页面业务相关的脚本-->
<script type="text/javascript" src="lib/My97DatePicker/4.8/WdatePicker.js"></script> 
<script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script> 
<script type="text/javascript" src="lib/laypage/1.2/laypage.js"></script>
<script type="text/javascript">
$(function(){
//	$('.table-sort').dataTable({
//		"aaSorting": [[ 1, "asc" ]],//默认第几个排序
//		"bStateSave": true,//状态保存
//		"aoColumnDefs": [
//		  //{"bVisible": false, "aTargets": [ 3 ]} //控制列的隐藏显示
//		  {"orderable":false,"aTargets":[0,8,9]}// 制定列不参与排序
//		]
//	});
$('.table-sort').dataTable({
						"bStateSave": false,//状态保存
                        "aLengthMenu" : [10, 20, 50,100,1000,10000], //更改显示记录数选项  
						"aoColumnDefs": [
						  { "orderable": false, "aTargets": [0] }// 制定列不参与排序
						]
					});
	
});
function check() {
					if ($(":checkbox:checked").length == 0) {
						alert("请选择需要操作的数据");
						return false;
					}
					return confirm('确定操作吗？');
				}
                function admin_role_edit(title, url, id, w, h) {
                layer_show(title, url, '1000', h);
            }
/*用户-添加*/
function member_add(title,url,w,h){
	layer_show(title,url,w,h);
}
/*用户-查看*/
function member_show(title,url,id,w,h){
	layer_show(title,url,w,h);
}
/*用户-停用*/
function member_stop(obj,id){
	layer.confirm('确认要停用吗？',function(index){
		$.ajax({
			type: 'POST',
			url: '',
			dataType: 'json',
			success: function(data){
				$(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="member_start(this,id)" href="javascript:;" title="启用"><i class="Hui-iconfont">&#xe6e1;</i></a>');
				$(obj).parents("tr").find(".td-status").html('<span class="label label-defaunt radius">已停用</span>');
				$(obj).remove();
				layer.msg('已停用!',{icon: 5,time:1000});
			},
			error:function(data) {
				console.log(data.msg);
			},
		});		
	});
}

/*用户-启用*/
function member_start(obj,id){
	layer.confirm('确认要启用吗？',function(index){
		$.ajax({
			type: 'POST',
			url: '',
			dataType: 'json',
			success: function(data){
				$(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="member_stop(this,id)" href="javascript:;" title="停用"><i class="Hui-iconfont">&#xe631;</i></a>');
				$(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">已启用</span>');
				$(obj).remove();
				layer.msg('已启用!',{icon: 6,time:1000});
			},
			error:function(data) {
				console.log(data.msg);
			},
		});
	});
}
/*用户-编辑*/
function member_edit(title,url,id,w,h){
	layer_show(title,url,w,h);
}
/*密码-修改*/
function change_password(title,url,id,w,h){
	layer_show(title,url,w,h);	
}
/*用户-删除*/
function member_del(obj,id){
	layer.confirm('确认要删除吗？',function(index){
		$.ajax({
			type: 'POST',
			url: '',
			dataType: 'json',
			success: function(data){
				$(obj).parents("tr").remove();
				layer.msg('已删除!',{icon:1,time:1000});
			},
			error:function(data) {
				console.log(data.msg);
			},
		});		
	});
}
</script> 
</body>
</html>