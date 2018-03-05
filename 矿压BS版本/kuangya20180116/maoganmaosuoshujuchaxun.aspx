<%@ Page Language="C#" AutoEventWireup="true" CodeFile="maoganmaosuoshujuchaxun.aspx.cs" Inherits="maoganmaosuoshujuchaxun" %>

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
    <title>综采数据分析</title>
</head>
<body>
    <form id="forma" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 锚杆/索应力管理 <span class="c-gray en">&gt;</span> 锚杆/索数据查询 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
        日期范围：
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
        <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" style="display:inline-block" >  
                            <ContentTemplate >
                            
                选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160"></asp:DropDownList>
            选择巷道：<asp:DropDownList ID="ddl_roadway" runat="server" OnSelectedIndexChanged ="ddl_roadway_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            安装位置：<asp:DropDownList ID="ddl_anzhuangweizhi" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"> 查询</asp:LinkButton>
            <asp:LinkButton ID="lbtn_export" runat="server" CssClass="btn btn-success radius" OnClick="lbtn_export_Click"> 导出</asp:LinkButton>
            
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l"></span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th>测区名称</th>
                        <th>巷道名称</th>
                        <th>时间</th>
                        <th>传感器编号</th>
                        <th>位置</th>
                        <th>应力值（kN）</th>
                        <th>报警状态</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("AreaName")%></td>
                                <td><%#Eval("roadwayname")%></td>
                                <td><%#Eval("time")%></td>
                                <td><%#Eval("sensorno")%></td>
                                <td><%#Eval("location")%></td>
                                <td><%#Eval("Stress")%></td>
                                <td><%#Eval("AlarmH")%></td>
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