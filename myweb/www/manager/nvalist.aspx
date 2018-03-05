<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nvalist.aspx.cs" Inherits="manager_nvalist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.7/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="lib/icheck/icheck.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>模块管理</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 导航栏管理 <span class="c-gray en">&gt;</span> 导航栏列表 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c pd-5 bg-1 bk-gray mt-20">
            上级栏目：<asp:DropDownList ID="ddl_nav" runat="server" CssClass="select" Height="30" Width="150">
                <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
            </asp:DropDownList>
            模板名称：<asp:DropDownList ID="ddl_muban" runat="server" CssClass="select" Height="30" Width="150">
                <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
            </asp:DropDownList>
            导航栏名称：
            <input type="text" class="input-text" style="width: 250px" placeholder="输入导航栏名称" id="daohanglanmingcheng" runat="server" name=""/>
            <input type="text" class="input-text" style="width:100px" placeholder="输入序号" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" id="xuhao" runat="server" />
            是否显示：<asp:DropDownList ID="ddl_show" runat="server" CssClass="select" Height="30" Width="40">
                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                <asp:ListItem Text="否" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:LinkButton ID="add_module" runat="server" CssClass="btn btn-primary radius" OnClick="add_modules" OnClientClick="return yanzheng();"><i class="Hui-iconfont">&#xe600;</i> 保存</asp:LinkButton>
        </div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th>一级导航栏</th>
                        <th>导航栏名称</th>
                        <th>模板名称</th>
                        <th>排序</th>
                        <th>是否显示</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><asp:DropDownList ID="ddl_nav0" runat="server" CssClass="select" Height="30" Width="150">
                                        <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbl_nav0" runat="server" Text='<%#Eval("parent_id") %>' Visible="false"></asp:Label>
                                </td>
                                <td><asp:TextBox ID="txt_name" runat="server" Text='<%#Eval("nav_name")%>' CssClass="input-text"></asp:TextBox></td>
                                <td><asp:DropDownList ID="ddl_muban" runat="server" CssClass="select" Height="30" Width="150">
                                        <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbl_muban" runat="server" Text='<%#Eval("muban_id") %>' Visible="false"></asp:Label>
                                </td>
                                <td><asp:TextBox ID="txt_paixu" runat="server" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%#Eval("paixu")%>' Width="100" CssClass="input-text"></asp:TextBox></td>
                                <td><asp:DropDownList ID="ddl_show" runat="server" CssClass="select" Height="30" Width="40">
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbl_isshow" runat="server" Text='<%#Eval("isshow") %>' Visible="false"></asp:Label>
                                </td>
                                <td class="td-manage">
                                    <asp:LinkButton ID="lbtshan"  runat="server" Text="删除"  CommandName="lbtshan" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('确实要删除吗？')" CssClass="btn btn-danger radius" ></asp:LinkButton> 
                                    <asp:LinkButton ID="lbtn_edit"  runat="server" Text="修改"  CommandName="lbtn_edit" CommandArgument='<%#Eval("id") %>' CssClass="btn btn-warning radius" ></asp:LinkButton> 
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
                "bStateSave": false, //状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0]}// 制定列不参与排序
                ]
            });

        });
        function admin_role_edit(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        function yanzheng() {
//            if ($("#ddl_muban").val() == "0") {
//                alert("请选择模板");
//                return false;
//            }
            if ($("#daohanglanmingcheng").val()=="") {
                alert("请输入导航栏名称");
                return false;
            }
            var paixu = $("#xuhao").val();
            if (paixu == "") {
                alert("请输入序号");
                return false;
            }
        }
</script>
        </form>
</body>
</html>
