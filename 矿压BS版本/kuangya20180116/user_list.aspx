<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_list.aspx.cs" Inherits="manager_user_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
	<meta http-equiv="Cache-Control" content="no-siteapp" />
	<!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<script type="text/javascript" src="lib/PIE_IE678.js"></script>
<![endif]-->
	<link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
	<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
	<link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.1/iconfont.css" />
	<link rel="stylesheet" type="text/css" href="lib/icheck/icheck.css" />
	<link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
	<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
	<!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
	<title>用户管理</title>
</head>
<body>
	<form id="forma" runat="server">
		<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 系统管理<span class="c-gray en">&gt;</span> 会员列表 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
		<div class="page-container">
			<div class="text-c">
				日期范围：
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;" readonly="readonly"/>
				-
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;" readonly="readonly"/>
				用户名：
           <input type="text" name="" id="userweixin" runat="server" placeholder="用户名" style="width: 150px" class="input-text" />
				手机号：
           <input type="text" name="" id="userphone" runat="server" placeholder="手机号" style="width: 150px" class="input-text" />
				<%--<button name="" id="Button1" class="btn btn-success" type="submit"><i class="Hui-iconfont">&#xe665;</i> 搜产品</button>--%>
				<asp:LinkButton runat="server" ID="search" CssClass="btn btn-success radius" OnClick="search_OnClick"><i class="Hui-iconfont">&#xe665;</i> 查询</asp:LinkButton>
			</div>
			<div class="cl pd-5 bg-1 bk-gray mt-20">
				<span class="l">
					<asp:LinkButton ID="lbtDelAll" runat="server" OnClientClick="return check();" class="btn btn-danger radius" OnClick="lbtDelAll_OnClick"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</asp:LinkButton>
					</span></div>
					<div class="mt-20">
						<table class="table table-border table-bordered table-hover table-bg table-sort">
							<thead>
								<tr class="text-c">
									<th width="40">
										<input name="" type="checkbox" value="" /></th>
									<th>序号</th>
									<th>用户名</th>
                                    
									<th>加入时间</th>
									<th style="display:none">用户状态</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>
								<asp:Repeater runat="server" ID="rptlist" OnItemCommand="rptlist_OnItemCommand">
									<%--OnItemDataBound="rptlist_OnItemDataBound"--%>
									<ItemTemplate>
										<tr class="text-c">
											<td>
												<asp:CheckBox runat="server" ID="ckb" ToolTip='<%#Eval("man_id") %>' />
											</td>
											<td><%#Container.ItemIndex+1 %></td>
                                            
											<td><%#Eval("man_name") %></td>
											
											<td><%#Eval("createtime") %></td>
											<td class="td-status" style="display:none"><span class="<%#Eval("state").ToString()=="启用"?"label label-success radius":"label label-danger radius" %>"><%#Eval("state") %></span></td>
											<td class="td-manage">
												<asp:Label runat="server" ID="userstate" Text='<%#Eval("state") %>' Style="display: none;"></asp:Label>
												<asp:LinkButton ID="lbtnOpen" runat="server" Text="禁用"  CommandName="lbtnOpen" CommandArgument='<%#Eval("man_id") %>' Style="color: #fff" CssClass="btn btn-primary radius"></asp:LinkButton>
												<asp:LinkButton ID="lbtnClose" runat="server" Text="启用"  CommandName="lbtnClose" CommandArgument='<%#Eval("man_id") %>' Style="color: #fff" CssClass="btn btn-success radius"></asp:LinkButton>
												<asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandName="lbtnDel" CommandArgument='<%#Eval("man_id") %>' OnClientClick="return confirm('确实要删除吗？');" CssClass="btn btn-danger radius" Style="color: #fff"></asp:LinkButton>
												<a onclick="admin_role_edit('详细','user_detail.aspx?userid=<%#Eval("man_id") %>','1')" class="btn badge-warning radius" style="color: #fff">详细</a>
											</td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</tbody>
						</table>
					</div>
					
			</div>
			<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
			<script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
			<script type="text/javascript" src="lib/My97DatePicker/WdatePicker.js"></script>
			<script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script>
			<script type="text/javascript" src="js/H-ui.js"></script>
			<script type="text/javascript" src="js/H-ui.admin.js"></script>
			<script type="text/javascript">
				$(function () {
					$('.table-sort').dataTable({
						"bStateSave": false,//状态保存
						"aoColumnDefs": [
						  { "orderable": false, "aTargets": [0] }// 制定列不参与排序
						]
					});

				});
				function admin_role_edit(title, url, id, w, h) {
					layer_show(title, url, '900', h);
				}
				function check() {
					if ($(":checkbox:checked").length == 0) {
						alert("请选择需要删除的用户");
						return false;
					}
					return confirm('确定删除吗？');
				}

</script>
	</form>
</body>
</html>
