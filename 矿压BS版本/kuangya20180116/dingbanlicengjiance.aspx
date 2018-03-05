<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dingbanlicengjiance.aspx.cs" Inherits="dingbanlicengjiance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5shiv.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<![endif]-->
    <link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.8/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css"
        id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>管理员</title>
</head>
<body>
    <form id="forma" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 顶板离层管理 <span class="c-gray en">&gt;</span> 顶板离层实时在线监测 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
    <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" >  
                            <ContentTemplate >
        <div class="text-c">
        
            选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="200"></asp:DropDownList>
            选择巷道：<asp:DropDownList ID="ddl_xiangdao" runat="server" CssClass="select" Height="30" Width="200">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            第<asp:DropDownList ID="ddl_page" runat="server" CssClass="select" Height="30" Width="50">
                <asp:ListItem Value="between 0 and 0" Text="1"></asp:ListItem>
            </asp:DropDownList>页
            <%--<a onclick="requestData();" class="btn btn-primary radius" ><i class="Hui-iconfont">&#xe665;</i>刷新</a>--%>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20">
            <span class="l">
            传感器组数：</span><input type="text" class="form-control" id="txtchuanganqizushu" runat="server" style="width:60px;" readonly="readonly" />
            位移预警值：</span><input type="text" class="form-control" id="txtweiyiyujingzhi" runat="server" style="width:60px;" readonly="readonly" />
            位移报警值：</span><input type="text" class="form-control" id="txtweiyibaojingzhi" runat="server" style="width:60px;" readonly="readonly" />
            A基点深度：</span><input type="text" class="form-control" id="txtajidianshendu" runat="server" style="width:60px;" readonly="readonly" />
            B基点深度：</span><input type="text" class="form-control" id="txtbjidianshendu" runat="server" style="width:60px;" readonly="readonly" />
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        <div class="mt-20">
        <img id="img1" runat="server" height="700" />
        </div>
    </div>
    <!--_footer 作为公共模版分离出去-->
    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="lib/layer/2.4/layer.js"></script>
    <script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script>
    <script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script>
    <!--/_footer 作为公共模版分离出去-->
    <!--请在下方写此页面业务相关的脚本-->
    <script type="text/javascript" src="lib/My97DatePicker/4.8/WdatePicker.js"></script>
    <script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="lib/laypage/1.2/laypage.js"></script>
    <!--请在下方写此页面业务相关的脚本-->
    <script type="text/javascript" src="lib/hcharts/Highcharts/5.0.6/js/highcharts.js"></script>
    <script type="text/javascript" src="lib/hcharts/Highcharts/5.0.6/js/modules/exporting.js"></script>
    <script type="text/javascript">
        $(function () {
            requestData();
        });
        function requestData() {
            var cequ = $("#ddl_cequ").val();
            var gongzuomian = $("#ddl_xiangdao").val();
            var page = $("#ddl_page").val();
            $("#img1").attr("src", "GetImage.ashx?cequ=" + cequ + "&roadway=" + gongzuomian + "&action=image2&page="+page+"&aa=" + new Date().getMilliseconds());
            setTimeout(requestData, 2000);
        }
    </script>
    </form>
</body>
</html>

