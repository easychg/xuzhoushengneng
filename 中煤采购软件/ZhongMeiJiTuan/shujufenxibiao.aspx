<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shujufenxibiao.aspx.cs" Inherits="shujufenxibiao" %>

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
<title>台账分析表</title>
<style type="text/css">
    .table-border
    {
       margin:0 auto;
        width:60%;
        }
        .table-border th
        {
            height:32px;
            font-size:16px;
            }
.table-border th,.table-border td
{
    border:1px solid #ddd;
    }
</style>
</head>
<body>
<form id="Form1" runat="server">
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 数据分析管理 <span class="c-gray en">&gt;</span> 数据分析表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">
	<div class="text-c"> 订单日期范围：
		<input type="text" onfocus="WdatePicker({ maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}' })" id="datemin" class="input-text Wdate" style="width:120px;" runat="server" />
		-
		<input type="text" onfocus="WdatePicker({ minDate:'#F{$dp.$D(\'datemin\')}' })" id="datemax" class="input-text Wdate" style="width:120px;" runat="server" />
        <asp:LinkButton ID="lbtn_search" runat="server" 
            CssClass="btn btn-success radius" Text="搜索" OnClientClick="return checkDT()" onclick="lbtn_search_Click"></asp:LinkButton>
		<asp:LinkButton ID="lbtn_daochu" runat="server" CssClass="btn btn-primary radius" onclick="lbtn_daochu_Click" Text="导出"></asp:LinkButton>
        <input id="btnPrint" type="button" class="btn btn-primary radius" value="打印" onclick="a('1')" style="display:none;" />  
	</div>
	<div class="cl pd-5 bg-1 bk-gray mt-20"> 
    <span class="l">
    
    </span> 
    
    </div>
    <div class="text-c"> <asp:Label ID="lbl_dingdanbianhao" runat="server" Font-Size="XX-Large"></asp:Label></div>
	<div class="mt-20" id="divdayin">
    <!--startprint1-->
	<table class="table-border">
     <%--class="table table-border table-bordered table-hover table-bg table-sort"--%>
		<thead>
        <tr>
        <td colspan="11" style="border:0px;text-align:center;font-size:20px;height:20px;"><div id="divheader" runat="server"></div></td>
        </tr>
			<tr class="text-c" style="background-color:#94B74D;">
				<th width="100" style="text-align:center">采购方式/单位名称</th>
				<th width="100" style="text-align:center">五家沟</th>
				<th width="100" style="text-align:center">南阳坡</th>
				<th width="100" style="text-align:center">元宝湾</th>
                <th width="100" style="text-align:center">动力中心</th>
                <th width="100" style="text-align:center">洗运中心</th>
                <th width="100" style="text-align:center">炫昂建材</th>
                <th width="100" style="text-align:center">永皓电厂</th>
                <th width="100" style="text-align:center">赤钰冶金</th>
                <th width="100" style="text-align:center">其它</th>
                <th width="100" style="text-align:center">合计</th>
			</tr>
		</thead>
		<tbody>
        <div id="divcaigoufangshi" runat="server"></div>
        <asp:Repeater ID="rpt_caigoufangshi" runat="server">
        <ItemTemplate>
            <tr class="text-c" style="background-color:<%#Eval("caigoufangshi").ToString()=="总计"?"#E5BBB9":""%>">
				<td width="200" style="font-size:16px;"><%#Eval("caigoufangshi") %></td>
				<td width="100"><%#Eval("五家沟")%></td>
                <td width="100"><%#Eval("南阳坡")%></td>
				<td width="100"><%#Eval("元宝湾")%></td>
                <td width="100"><%#Eval("动力中心")%></td>
                <td width="100"><%#Eval("洗运中心")%></td>
				<td width="100"><%#Eval("炫昂建材")%></td>
                <td width="100"><%#Eval("永皓电厂")%></td>
				<td width="100"><%#Eval("赤钰冶金")%></td>
                <td width="100"><%#Eval("其它")%></td>
                <td width="100"><%#Eval("合计")%></td>
           </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr style="height:20px;"></tr>
        <tr class="text-c" style="background-color:#94B74D;">
				<th width="100" style="text-align:center">采购性质/单位名称</th>
				<th width="100" style="text-align:center">五家沟</th>
				<th width="100" style="text-align:center">南阳坡</th>
				<th width="100" style="text-align:center">元宝湾</th>
                <th width="100" style="text-align:center">动力中心</th>
                <th width="100" style="text-align:center">洗运中心</th>
                <th width="100" style="text-align:center">炫昂建材</th>
                <th width="100" style="text-align:center">永皓电厂</th>
                <th width="100" style="text-align:center">赤钰冶金</th>
                <th width="100" style="text-align:center">其它</th>
                <th width="100" style="text-align:center">合计</th>
			</tr>
            <div id="divjihualeixing" runat="server"></div>
			<asp:Repeater ID="rpt_jihualeixing" runat="server">
        <ItemTemplate>
            <tr class="text-c" style="background-color:<%#Eval("jihualeixing").ToString()=="总计"?"#E5BBB9":""%>">
				<td width="100" style="font-size:16px;"><%#Eval("jihualeixing")%></td>
				<td width="100"><%#Eval("五家沟")%></td>
                <td width="100"><%#Eval("南阳坡")%></td>
				<td width="100"><%#Eval("元宝湾")%></td>
                <td width="100"><%#Eval("动力中心")%></td>
                <td width="100"><%#Eval("洗运中心")%></td>
				<td width="100"><%#Eval("炫昂建材")%></td>
                <td width="100"><%#Eval("永皓电厂")%></td>
				<td width="100"><%#Eval("赤钰冶金")%></td>
                <td width="100"><%#Eval("其它")%></td>
                <td width="100"><%#Eval("合计")%></td>
           </tr>
        </ItemTemplate>
        </asp:Repeater>
        
        <tr style="height:20px;"></tr>
        <tr class="text-c" style="background-color:#94B74D;">
				<th width="100" style="text-align:center">物资属性/单位名称</th>
				<th width="100" style="text-align:center">五家沟</th>
				<th width="100" style="text-align:center">南阳坡</th>
				<th width="100" style="text-align:center">元宝湾</th>
                <th width="100" style="text-align:center">动力中心</th>
                <th width="100" style="text-align:center">洗运中心</th>
                <th width="100" style="text-align:center">炫昂建材</th>
                <th width="100" style="text-align:center">永皓电厂</th>
                <th width="100" style="text-align:center">赤钰冶金</th>
                <th width="100" style="text-align:center">其它</th>
                <th width="100" style="text-align:center">合计</th>
			</tr>
            <div id="divwuzishuxing" runat="server"></div>
        <asp:Repeater ID="rpt_wuzishuxing" runat="server">
        <ItemTemplate>
            <tr class="text-c" style="background-color:<%#Eval("wuzishuxing").ToString()=="总计"?"#E5BBB9":""%>">
				<td width="100" style="font-size:16px;"><%#Eval("wuzishuxing")%></td>
				<td width="100"><%#Eval("五家沟")%></td>
                <td width="100"><%#Eval("南阳坡")%></td>
				<td width="100"><%#Eval("元宝湾")%></td>
                <td width="100"><%#Eval("动力中心")%></td>

                <td width="100"><%#Eval("洗运中心")%></td>
				<td width="100"><%#Eval("炫昂建材")%></td>
                <td width="100"><%#Eval("永皓电厂")%></td>
				<td width="100"><%#Eval("赤钰冶金")%></td>
                <td width="100"><%#Eval("其它")%></td>

                <td width="100"><%#Eval("合计")%></td>
           </tr>
        </ItemTemplate>
        </asp:Repeater>
		</tbody>
	</table>
    <!--endprint1-->
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
$('.').dataTable({
						"bStateSave": false,//状态保存
                        "aLengthMenu" : [10, 20, 50,100,1000,10000], //更改显示记录数选项  
						"aoColumnDefs": [
						  { "orderable": false, "aTargets": [0] }// 制定列不参与排序
						]
					});
	
});
function checkDT(){
    if($("#datemin").val()==""){
        alert("开始时间不能为空");
        return false;
    }
    if($("#datemax").val()==""){
        alert("结束时间不能为空");
        return false;
    }
}
function check() {
					if ($(":checkbox:checked").length == 0) {
						alert("请选择需要操作的数据");
						return false;
					}
					return confirm('确定操作吗？');
				}
                function admin_role_edit(title, url, id, w, h) {
                layer_show(title, url, w, h);
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

<script src="lib/jquery.jqprint-0.3.js" type="text/javascript"></script>
    <script src="lib/jquery-migrate-1.1.0.js" type="text/javascript"></script>
<script type="text/javascript">


function a(oper) {

    $("#divdayin").jqprint();
//if (oper < 10)  
//{  
//bdhtml=window.document.body.innerHTML;//获取当前页的html代码  
//sprnstr="<!--startprint"+oper+"-->";//设置打印开始区域  
//eprnstr="<!--endprint"+oper+"-->";//设置打印结束区域  
//prnhtml=bdhtml.substring(bdhtml.indexOf(sprnstr)+18); //从开始代码向后取html  
//prnhtmlprnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));//从结束代码向前取html  
//window.document.body.innerHTML=prnhtml;  
//window.print();  
//window.document.body.innerHTML=bdhtml;  
//} else {  
//window.print();  
//}  
}  

</script>
</body>
</html>