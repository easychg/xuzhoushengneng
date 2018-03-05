<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taizhang_list.aspx.cs" Inherits="taizhang_list" %>

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
<form runat="server">
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 物资采购数据管理 <span class="c-gray en">&gt;</span> 物资采购数据列表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">
	<div class="text-c"> 导入日期范围：
		<input type="text" onfocus="WdatePicker({ maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}' })" id="datemin" class="input-text Wdate" style="width:120px;" runat="server" />
		-
		<input type="text" onfocus="WdatePicker({ minDate:'#F{$dp.$D(\'datemin\')}' })" id="datemax" class="input-text Wdate" style="width:120px;" runat="server" />
        
        计划类型：<asp:DropDownList ID="ddl_jihualeixing" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        物资属性：<asp:DropDownList ID="ddl_wuzishuxing" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        付款方式：<asp:DropDownList ID="ddl_fukuanfangshi" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        电商平台：<asp:DropDownList ID="ddl_dianshangpingtai" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        资金来源：<asp:DropDownList ID="ddl_zijinlaiyuan" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        交货地点：<asp:DropDownList ID="ddl_jiaohuodidian" runat="server" CssClass="select" Height="30" Width="120">
        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
        </asp:DropDownList>
        计划编号：<input type="text" class="input-text" style="width:250px" placeholder="输入计划编号" id="txtjihuabianhao" runat="server" />
        厂商名称：<input type="text" class="input-text" style="width:250px" placeholder="输入厂商名称" id="txtchangshangmingcheng" runat="server" />
        <asp:LinkButton ID="lbtn_search" runat="server" 
            CssClass="btn btn-success radius" Text="搜索" onclick="lbtn_search_Click"></asp:LinkButton>
		<%--<input type="text" class="input-text" style="width:250px" placeholder="输入会员名称、电话、邮箱" id="" name="">
		<button type="submit" class="btn btn-success radius" id="" name=""> 搜索</button>--%>
	</div>
	<div class="cl pd-5 bg-1 bk-gray mt-20"> 
    <span class="l">
        <asp:FileUpload ID="FileUpload1" runat="server" />  
        <asp:LinkButton ID="lbtn_daoru" runat="server"  CssClass="btn btn-warning radius" onclick="lbtn_daoru_Click" Text="导入"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_delete" runat="server" CssClass="btn btn-danger radius" OnClientClick="return check()" Text="删除" onclick="lbtn_delete_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_wanggou" runat="server" 
            CssClass="btn btn-primary radius" OnClientClick="return check()" 
            Text="导出网购订货通知单" onclick="lbtn_wanggou_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_changxie" runat="server" 
            CssClass="btn btn-primary radius" OnClientClick="return check()" 
            Text="导出长协订货通知单" onclick="lbtn_changxie_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtn_putong" runat="server" 
            CssClass="btn btn-primary radius" OnClientClick="return check()" 
            Text="导出普通订货通知单" onclick="lbtn_putong_Click"></asp:LinkButton>
            <a onclick="admin_role_edit('查看','taizhang_info.aspx','1')" class="btn btn-primary radius">添加</a>
    </span> 
    
    </div>
	<div class="mt-20">
	<table class="table table-border table-bordered table-hover table-bg table-sort">
		<thead>
			<tr class="text-c">
				<th width="25"><input type="checkbox" name="" value=""></th>
				<th width="80">序号</th>
				<th width="100">月份</th>
				<th width="40">使用单位</th>
				<th width="90">计划编号</th>
                <th width="100">订单日期</th>
                <th width="100">应到货时间</th>
                <th width="100">备注</th>
                <th width="100">订单状态</th>
                <th width="100">操作员</th>
                <th width="100">查看</th>
			</tr>
		</thead>
		<tbody>
        <asp:Repeater ID="rpt_taizhang" runat="server" 
                onitemcommand="rpt_taizhang_ItemCommand">
        <ItemTemplate>
        <tr class="text-c">
				<td><asp:CheckBox runat="server" ID="ckb" ToolTip='<%#Eval("taizhang_id") %>' /></td>
				<td width="80"><%#Container.ItemIndex+1 %></td>
				<td width="100"><%#Eval("yuefen") %></td>
				<td width="40"><%#Eval("shiyongdanwei") %></td>
				<td width="90"><%#Eval("jihuabianhao") %></td>
                <td width="100"><%#Eval("dingdanriqi")%></td>
                <td width="100"><%#Eval("yingdaoshijian")%></td>
                <td width="100"><%#Eval("beizhu") %></td>
                <td width="100" style='color:<%#Eval("isfinished").ToString()=="1"?"green":"red" %>'><%#Eval("isfinished").ToString()=="1"?"全部到货":"未到货" %></td>
                <td width="100"><asp:Label ID="lbl_user" runat="server" Text='<%#Eval("manager_id") %>'></asp:Label></td>
                <td width="100" align="left" style="text-align:left">
                <a onclick="admin_role_edit('查看','taizhang_info.aspx?id=<%#Eval("taizhang_id") %>','1')" class="btn btn-primary radius">查看</a>
                <a onclick="admin_role_edit2('添加记录','jilu_add.aspx?id=<%#Eval("taizhang_id") %>','1')" class="btn btn-primary radius" style='display:<%#Eval("isfinished").ToString()=="1"?"none":"" %>'>记录</a>
                <asp:LinkButton ID="lbtn_genzong" runat="server" CommandName="lbtn_genzong"  CommandArgument='<%#Eval("taizhang_id") %>' CssClass="btn btn-warning radius" Text="跟踪" Visible="false"></asp:LinkButton>
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
                layer_show(title, url, '1000', '200');
            }
               function admin_role_edit2(title, url, id, w, h) {
                layer_show(title, url, '500', '300');
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