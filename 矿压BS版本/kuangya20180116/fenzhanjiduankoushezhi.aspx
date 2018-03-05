<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fenzhanjiduankoushezhi.aspx.cs" Inherits="fenzhanjiduankoushezhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <title>分站端口设置</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 系统管理 <span class="c-gray en">&gt;</span> 分站端口设置 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
            所属测区：<asp:DropDownList ID="ddl_AreaInfo" runat="server" CssClass="select" Height="30" Width="120">
            </asp:DropDownList>
            面巷信息：<asp:DropDownList ID="ddl_WorkfaceInfo" runat="server" CssClass="select" Height="30" Width="120">
            </asp:DropDownList>
            分站号：
            <asp:DropDownList ID="ddl_fenzhanhao" runat="server" CssClass="select" Height="30" Width="120">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                <asp:ListItem Text="6" Value="6"></asp:ListItem>
            </asp:DropDownList>
            端口号：
            <asp:DropDownList ID="ddl_duankouhao" runat="server" CssClass="select" Height="30" Width="120">
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
            波特率：<asp:DropDownList ID="ddl_botelv" runat="server" CssClass="select" Height="30" Width="120">
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
            使用状态：<asp:DropDownList ID="ddl_shiyongzhuangtai" runat="server" CssClass="select" Height="30" Width="120">
                <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
            </asp:DropDownList>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-primary radius" OnClick="search_Click"> <i class="Hui-iconfont">&#xe665;</i> 搜索</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20">
        <span class="l">
        <a href="javascript:;" onclick="admin_role_edit('添加','fenzhanjiduankou_info.aspx','510')" class="btn btn-success radius"><i class="Hui-iconfont">&#xe600;</i>添加</a>
        <asp:LinkButton ID="lbtn_delete" runat="server" CssClass="btn btn-danger radius" OnClientClick="return check()" onclick="lbtn_delete_Click"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
        </span>
        </div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                    <th width="25"><input type="checkbox" name="" value=""></th>
                        <th>序号</th>
                        <th>分站号</th>
                        <th>端口号</th>
                        <th>波特率</th>
                        <th>所属测区</th>
                        <th>面巷信息</th>
                        <th>使用状态</th>
                        <th>查看</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server">
                        <ItemTemplate>
                            <tr class="text-c">
                            <td><asp:CheckBox runat="server" ID="ckb" ToolTip='<%#Eval("stationno") %>' /></td>
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("stationno") %></td>
                                <td><%#Eval("commno") %></td>
                                <td><%#Eval("baudrate") %></td>
                                <td><%#Eval("areaname") %></td>
                                <td><%#Eval("areaface") %></td>
                                <td style='color:<%#Eval("usestate").ToString()=="使用"?"green":"red" %>'><%#Eval("usestate") %></td>
                                <td class="td-manage">
                                    <a   onclick="admin_role_edit('详细','fenzhanjiduankou_info.aspx?id=<%#Eval("stationno") %>','1')" class="btn btn-warning radius">查看</a>
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
                "bStateSave": false, //状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0]}// 制定列不参与排序
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