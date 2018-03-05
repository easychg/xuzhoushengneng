<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guanliyuanlist.aspx.cs" Inherits="manager_guanliyuanlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
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
    <title>管理员</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 权限管理 <span class="c-gray en">&gt;</span> 管理员管理 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
            日期范围：
	
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
	
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
            <input type="text" class="input-text" style="width: 250px" placeholder="输入管理员名称" id="qitatiaojian" runat="server" name=""/>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"> 搜管理员</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l"><a href="javascript:;" onclick="admin_role_edit('添加用户','guanliyuan.aspx','510')" class="btn btn-primary radius">添加管理员</a></span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th>管理员名称</th>
                        <th>管理员组名称</th>
                        <th>状态</th>
                        <th>创建时间</th>
                        <th>备注</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("man_name") %></td>
                                <td><%#Eval("roleName") %></td>
                                <td class="td-status"><span class='<%#Eval("state").ToString()=="禁止"?"label label-danger radius":"label label-success radius" %>'><%#Eval("state") %></span></td>
                                <td><%#Eval("createTime") %></td>
                                <td><%#Eval("remark") %></td>
                                <td class="td-manage">
                                    <asp:Label ID="lbl_zhuangtai" runat="server" Text='<%#Eval("state") %>' style="display:none;"></asp:Label>
                                    <asp:LinkButton ID="lbtn_open" runat="server" Text="启用" CommandName="lbtn_open" CommandArgument='<%#Eval("man_id") %>' Style="color: #fff" CssClass="btn btn-success radius"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_close" runat="server" Text="禁止" CommandName="lbtn_close" CommandArgument='<%#Eval("man_id") %>' Style="color: #fff" CssClass="btn btn-primary radius"></asp:LinkButton>
                                    <a   onclick="admin_role_edit('详细','guanliyuan.aspx?manId=<%#Eval("man_id") %>','1')" class="btn btn-warning radius">详细</a>
                               </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

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
        $(function () {
            $('.table-sort').dataTable({
                "bStateSave": false,//状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0] }// 制定列不参与排序
                ]
            });

        });
        function admin_role_edit(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
</script>
        </form>
</body>
</html>
