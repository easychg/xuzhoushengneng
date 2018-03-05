<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guanliyuanzulist.aspx.cs" Inherits="manager_guanliyuanzulist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <title>管理员组</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 权限管理 <span class="c-gray en">&gt;</span> 管理员组管理 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
            日期范围：
	
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
	
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
            <input type="text" class="input-text" style="width: 250px" placeholder="输入管理员组名称" id="qitatiaojian" runat="server" name=""/>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"><i class="Hui-iconfont">&#xe665;</i> 搜管理员组</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l"><a href="javascript:;" onclick="admin_role_edit('添加用户','guanliyuanzu.aspx','510')" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加管理员组</a></span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th >管理员组名称</th>
                        <th>排序</th>
                        <th>状态</th>
                        <th>创建时间</th>
                        <th>备注</th>
                        <th>组内管理员</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand" OnItemDataBound="rptlist_ItemDataBound" >
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("roleName") %></td>
                                <td><%#Eval("paixu") %></td>
                                <td class="td-status"><span class='<%#Eval("state").ToString()=="禁止"?"label label-danger radius":"label label-success radius" %>'><%#Eval("state") %></span></td>
                                <td><%#Eval("createtime") %></td>
                                <td><%#Eval("remark") %></td>
                                <td>
                                    <asp:Label ID="lb_id" runat="server" ToolTip='<%#Eval("id") %>' Visible="false"></asp:Label>
                                    <asp:Repeater ID="rpta" runat="server">
                                        <ItemTemplate><%#Container.ItemIndex==0?"":"，" %><%#Eval("man_name") %></ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                
                                <td class="td-manage">
                                    <asp:Label ID="lbl_zhuangtai" runat="server" Text='<%#Eval("state") %>' style="display:none;"></asp:Label>
                                    <asp:LinkButton ID="lbtn_open" runat="server" Text="启用" CommandName="lbtn_open" CommandArgument='<%#Eval("id") %>' Style="color: #fff" CssClass="btn btn-success radius"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_close" runat="server" Text="禁止" CommandName="lbtn_close" CommandArgument='<%#Eval("id") %>' Style="color: #fff" CssClass="btn btn-primary radius"></asp:LinkButton>
                                    <a   onclick="admin_role_edit('详细','guanliyuanzu.aspx?manId=<%#Eval("id") %>','1')" class="btn btn-warning radius">详细</a>
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
            layer_show(title, url, w, h);
        }
</script>
        </form>
</body>
</html>
