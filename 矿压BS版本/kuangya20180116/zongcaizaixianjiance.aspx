<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zongcaizaixianjiance.aspx.cs"
    Inherits="zongcaizaixianjiance" %>

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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 综采压力管理 <span class="c-gray en">&gt;</span> 综采在线监测 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
    <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" >  
                            <ContentTemplate >
        <div class="text-c">
        
            选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="200"></asp:DropDownList>
            选择工作面：<asp:DropDownList ID="ddl_gongzuomian" runat="server" CssClass="select" Height="30" Width="200">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            压力类型：<asp:DropDownList ID="ddl_yalileixing" runat="server" CssClass="select" Height="30" Width="200">
            <asp:ListItem Text="液压支架" Value="液压支架"></asp:ListItem>
            </asp:DropDownList>
            
            第<asp:DropDownList ID="ddl_page" runat="server" OnSelectedIndexChanged ="ddl_page_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="50">
                <asp:ListItem Value="between 0 and 0" Text="1"></asp:ListItem>
            </asp:DropDownList>页
            <%--<a onclick="requestData();" class="btn btn-primary radius" ><i class="Hui-iconfont">&#xe665;</i>刷新</a>--%>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20">
            <span class="l">
            传感器组数：</span><input type="text" class="form-control" id="txtchuanganqizushu" runat="server" style="width:60px;" readonly="readonly" />
            第一通道接：</span><input type="text" class="form-control" id="txtdiyitongdaojie" runat="server" style="width:60px;" readonly="readonly" />
            缸径（mm）：</span><input type="text" class="form-control" id="txtgangjing1" runat="server" style="width:60px;" readonly="readonly" />
            第二通道接：</span><input type="text" class="form-control" id="txtdiertongdaojie" runat="server" style="width:60px;" readonly="readonly" />
            缸径（mm）：</span><input type="text" class="form-control" id="txtgangjing2" runat="server" style="width:60px;" readonly="readonly" />
            压力上限（MPa）：</span><input type="text" class="form-control" id="txtyalishangxian" runat="server" style="width:60px;" readonly="readonly" />
            压力下限（MPa）：</span><input type="text" class="form-control" id="txtyalixiaxian" runat="server" style="width:60px;" readonly="readonly" />
            平衡千斤顶报警值（MPa）：</span><input type="text" class="form-control" id="txtbaojingzhi" runat="server" style="width:60px;" readonly="readonly" />
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        <div class="mt-20">
        <img id="img1" runat="server" height="680" />
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
        var cequ=$("#ddl_cequ").val();
        var gongzuomian = $("#ddl_gongzuomian").val();
        var page = $("#ddl_page").val();
        $("#img1").attr("src", "GetImage.ashx?cequ=" + cequ + "&gongzuomian=" + gongzuomian + "&action=image1&page=" + page + "&aa=" + new Date().getMilliseconds());
       
        setTimeout(requestData, 2000);
//        $.ajax({
//            type: "post", //访问WebService使用Post方式请求
//            url: "GetImage.ashx?cequ=" + cequ + "&gongzuomian=" + gongzuomian + "&action=image1&aa=" + new Date().getMilliseconds(), //调用Url(WebService的地址和方法名称组合---WsURL/方法名)
//            data: '{}', //这里是要传递的参数，为Json格式{paraName:paraValue}
//            dataType: 'json',
//            contentType: "Application/Json", // 发送信息至服务器时内容编码类型
//            beforeSend: function (XMLHttpRequest) {
//                XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
//            },
//            success: function (data) {
//                $("#img1").attr("src", data.url);

//                setTimeout(requestData, 10000);
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                /*弹出jqXHR对象的信息*/
//                alert(jqXHR.responseText);
//                alert(jqXHR.status);
//                alert(jqXHR.readyState);
//                alert(jqXHR.statusText);
//                /*弹出其他两个参数的信息*/
//                alert(textStatus);
//                alert(errorThrown);
//            }
//        });
        }

//        window.onload = function () {
//            $(".highcharts-legend").hide();
//            $(".highcharts-credits").hide();
//        }
    </script>
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
