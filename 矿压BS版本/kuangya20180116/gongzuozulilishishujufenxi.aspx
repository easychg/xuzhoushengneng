<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gongzuozulilishishujufenxi.aspx.cs" Inherits="gongzuozulilishishujufenxi" %>

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
    <title>工作阻力历史数据分析</title>
</head>
<body>
    <form id="forma" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 综采压力管理 <span class="c-gray en">&gt;</span> 工作阻力历史数据分析 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
        日期范围：
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
        <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" style="display:inline-block" >  
                            <ContentTemplate >
                            
                选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160"></asp:DropDownList>
            选择工作面：<asp:DropDownList ID="ddl_gongzuomian" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            压力类型：<asp:DropDownList ID="ddl_yalileixing" runat="server" OnSelectedIndexChanged ="ddl_yalileixing_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            <asp:ListItem Text="液压支架" Value="液压支架"></asp:ListItem>
            </asp:DropDownList>
            支架编号：<asp:DropDownList ID="ddl_zhijiabianhao" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="search" runat="server" 
                OnClientClick="return requestData();" CssClass="btn btn-success radius" 
                onclick="search_Click" > 刷新</asp:LinkButton>
                <asp:LinkButton ID="lbtn_export" runat="server" 
                CssClass="btn btn-success radius" 
                onclick="lbtn_export_Click" > 导出图片</asp:LinkButton>
            
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l">
        进尺数据详情：机头总进尺（M）<input type="text" id="head" runat="server" value="0.00" readonly="readonly" />中部总进尺（M）<input type="text" id="body" runat="server" value="0.00" readonly="readonly" />机尾总进尺（M）<input type="text" id="foot" runat="server" value="0.00" readonly="readonly" />
        </span></div>
        <div class="mt-20">
             <asp:Panel ID="panel1" runat="server" Width="1370" Height="700" style="position:absolute;background-size: 1370px 700px;">
                <hr id="hrx" style="color:Red;z-index:999;position:absolute;margin-top:700px;margin-left:50px;width:1200px;"/>
                <hr id="hry" style="color:Red;z-index:999;position:absolute;margin-top:88px;margin-left:50px;width:1px;height:520px;"/>
            </asp:Panel>
            <asp:TextBox ID="txtjson" runat="server" Width="1000" style="display:none;" ></asp:TextBox>
            <asp:label ID="txtshow" runat="server" Width="1000" Font-Size="12" ForeColor="Red" style="margin-top:695px;"></asp:label>
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
         <!--请在下方写此页面业务相关的脚本-->
    <script type="text/javascript" src="lib/hcharts/Highcharts/5.0.6/js/highcharts.js"></script>
    <script type="text/javascript" src="lib/hcharts/Highcharts/5.0.6/js/modules/exporting.js"></script>
    <script type="text/javascript">
        $(function () {
            var json = JSON.parse($("#txtjson").val());
            $("#panel1").mousemove(function (e) {
                var l = 50;
                var t = 88;
                var w = 1200;
                var h = 520;
                var X = $('#panel1').offset().left;
                var Y = $('#panel1').offset().top;

                var minX = l + X;
                var minY = t + Y;
                var maxX = minX + w;
                var maxY = minY + h;

                if (e.pageX <= maxX && e.pageX >= minX && e.pageY <= maxY && e.pageY >= minY) {
                    var offsetX = e.pageX - minX;
                    $("#txtshow").text(json[offsetX].content);
                    $("#hrx").css("margin-top", e.pageY - Y + "px");
                    $("#hry").css("margin-left", e.pageX - X + "px");
                } else {
                    $("#hrx").css("margin-top", "88px");
                    $("#hry").css("margin-left", "50px");
                }
            });
        })
    
</script>
    <script type="text/javascript">
        $(function () {
            //requestData();
        });
        function requestData() {
            var t1 = $("#datemin").val();
            if (t1 == "") {
                alert("请选择开始时间");
                return false;
            }
            var t2 = $("#datemax").val();
            if (t2 == "") {
                alert("请选择结束时间");
                return false;
            }
            var cequ = $("#ddl_cequ").val();
            if (cequ == "0") {
                alert("请选择测区");
                return false;
            }
            var gongzuomian = $("#ddl_gongzuomian").val();
            if (gongzuomian == "0") {
                alert("请选择工作面");
                return false;
            }
            var zhijiabianhao = $("#ddl_zhijiabianhao").val();
            if (zhijiabianhao == "0") {
                alert("请选择支架编号");
                return false;
            }
//            if (t1 != "" && t2 != "" && cequ != "0" && gongzuomian != "0" && zhijiabianhao != "0") {
//                $.ajax({
//                    type: "post", //访问WebService使用Post方式请求
//                    url: "WebService.asmx/ZCGZZLLSSJFXQX", //调用Url(WebService的地址和方法名称组合---WsURL/方法名)
//                    data: '{"cequ":"' + cequ + '","gongzuomian":"' + gongzuomian + '","bracketno":"' + zhijiabianhao + '","t1":"' + t1 + '","t2":"' + t2 + '"}', //这里是要传递的参数，为Json格式{paraName:paraValue}
//                    dataType: 'json',
//                    contentType: "Application/Json", // 发送信息至服务器时内容编码类型
//                    beforeSend: function (XMLHttpRequest) {
//                        XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
//                    },
//                    success: function (data) {
//                        var json = eval('(' + data.d + ')');
//                        $("#head").val(json.head);
//                        $("#body").val(json.body);
//                        $("#foot").val(json.foot);

//                        //                //操作成功
//                        //                //alert(json.resultmessage);
//                        //                Highcharts.chart('container', {
//                        //                    title: {
//                        //                        text: '综采工作阻力历史数据分析曲线',
//                        //                        x: -20 //center
//                        //                    },
//                        //                    subtitle: {
//                        //                        text: '',
//                        //                        x: -20
//                        //                    },
//                        //                    xAxis: {
//                        //                        categories: json.categories
//                        //                    },
//                        //                    yAxis: {
//                        //                        title: {
//                        //                            text: 'P（MPa）'
//                        //                        }, plotLines: [{
//                        //                            color: 'red',            //线的颜色，定义为红色
//                        //                            dashStyle: 'dash', //标示线的样式，默认是solid（实线），这里定义为长虚线
//                        //                            value: json.max,                //定义在哪个值上显示标示线，这里是在x轴上刻度为3的值处垂直化一条线
//                        //                            width: 2                 //标示线的宽度，2px
//                        //                    , label: {
//                        //                        text: "" + json.max + "报警值",     //标签的内容
//                        //                        align: 'right',                //标签的水平位置，水平居左,默认是水平居中center
//                        //                        x: 0                         //标签相对于被定位的位置水平偏移的像素，重新定位，水平居左10px
//                        //                    },
//                        //                            zIndex: 0
//                        //                        }]
//                        //                    },
//                        //                    tooltip: {
//                        //                        valueSuffix: 'P（MPa）'
//                        //                    },
//                        //                    legend: {
//                        //                        layout: 'vertical',
//                        //                        align: 'right',
//                        //                        verticalAlign: 'middle',
//                        //                        borderWidth: 0
//                        //                    },
//                        //                    series: [{
//                        //                        name: '压力1',
//                        //                        data: json.series0
//                        //                    }, {
//                        //                        name: '压力2',
//                        //                        data: json.series1
//                        //                    }]
//                        //                });
//                        //                $(".highcharts-credits").hide(); //去除水印
//                        //setTimeout(requestData, 10000);
//                    },
//                    error: function (jqXHR, textStatus, errorThrown) {
//                        /*弹出jqXHR对象的信息*/
//                        //alert(jqXHR.responseText);
//                        //alert(jqXHR.status);
//                        //alert(jqXHR.readyState);
//                        //alert(jqXHR.statusText);
//                        /*弹出其他两个参数的信息*/
//                        //alert(textStatus);
//                        //alert(errorThrown);
//                    }
//                });
//            }
//            return true;
        }
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