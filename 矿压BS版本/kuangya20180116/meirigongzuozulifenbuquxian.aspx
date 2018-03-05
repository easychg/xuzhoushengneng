﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="meirigongzuozulifenbuquxian.aspx.cs" Inherits="meirigongzuozulifenbuquxian" %>

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
            <input type="text" onfocus="WdatePicker()" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
            <input type="text" onfocus="WdatePicker()" id="datemax" runat="server" class="input-text Wdate" style="width: 100px;"/>
            <%---
            <input type="text" onfocus="WdatePicker({dateFmt: 'HH:mm:ss'})" id="datemax2" runat="server" class="input-text Wdate" style="width: 100px;"/>--%>
        <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" style="display:inline-block" >  
                            <ContentTemplate >
                            
                选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160"></asp:DropDownList>
            选择工作面：<asp:DropDownList ID="ddl_gongzuomian" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            压力上限（MPa）：<input type="text" id="shangxian" value="0" runat="server" readonly="readonly" style="width:50px;" />
            压力下限（MPa）：<input type="text" id="xiaxian" value="0" runat="server" readonly="readonly" style="width:50px;" />
            </ContentTemplate>
            </asp:UpdatePanel>
            <%--压力阈值（MPa）：<input type="text" id="yuzhi" runat="server" value="0" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" class="input-text" style="width:120px;" />--%>
            <%--<div class="mt-20 skin-minimal" style="display:inline-block;">
            <div class="radio-box">
                <input type="radio" id="zhengjia" runat="server" value="3" name="r1" checked />
                <label for="zhengjia">整架</label>
              </div>
              <div class="radio-box">
                <input type="radio" id="zuozhu"  runat="server" value="1" name="r1" />
                <label for="zuozhu">左柱</label>
              </div>
              <div class="radio-box">
                <input type="radio" id="youzhu" value="2" runat="server" name="r1" />
                <label for="youzhu">右柱</label>
              </div>
            </div>--%>
            <asp:LinkButton ID="search" runat="server" 
                OnClientClick="return requestData();" CssClass="btn btn-success radius" 
                onclick="search_Click" > 刷新</asp:LinkButton>
                <asp:LinkButton ID="lbtn_export" runat="server" 
                 CssClass="btn btn-success radius" 
                onclick="lbtn_export_Click" > 导出图片</asp:LinkButton>
                <asp:LinkButton ID="lbtn_export_ribao" runat="server" 
                 CssClass="btn btn-success radius" 
                onclick="lbtn_export_ribao_export_Click" > 导出报表</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l">
        
        </span></div>
        <div class="mt-20">
        <asp:Image ID="img1" runat="server" Height="700" />
            <%--<div id="container" style="min-width: 700px; height: 400px">
            </div>--%>
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
            //requestData();
        });
        function requestData() {

            var t1 = $("#datemin").val();
            if (t1 == "") {
                alert("请选择日期");
                return false;
            }
            var t2 = $("#datemax").val();
            if (t2 == "") {
                alert("请选择开始时间");
                return false;
            }
            var t3 = $("#datemax2").val();
            if (t3 == "") {
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
            var yuzhi = $("#yuzhi").val();
            if (yuzhi == "") {
                alert("请输入阈值");
                return false;
            }
            
//            $.ajax({
//                type: "post", //访问WebService使用Post方式请求
//                url: "WebService.asmx/MRGZZLFBQX", //调用Url(WebService的地址和方法名称组合---WsURL/方法名)
//                data: '{"cequ":"' + cequ + '","gongzuomian":"' + gongzuomian + '","yuzhi":"' + yuzhi + '","t1":"' + _t1 + '","t2":"' + _t2 + '"}', //这里是要传递的参数，为Json格式{paraName:paraValue}
//                dataType: 'json',
//                contentType: "Application/Json", // 发送信息至服务器时内容编码类型
//                beforeSend: function (XMLHttpRequest) {
//                    XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
//                },
//                success: function (data) {
//                    var json = eval('(' + data.d + ')');
//                    var _data = "";
//                    if (_r == "0") {
//                        _data = json.series2;
//                    }
//                    if (_r == "1") {
//                        _data = json.series0;
//                    }
//                    if (_r == "2") {
//                        _data = json.series1;
//                    }

//                    //操作成功
//                    //alert(json.resultmessage);
//                    Highcharts.chart('container', {
//                        title: {
//                            text: '综采工作阻力历史数据分析曲线',
//                            x: -20 //center
//                        },
//                        subtitle: {
//                            text: '',
//                            x: -20
//                        },
//                        xAxis: {
//                            categories: json.categories
//                        },
//                        yAxis: {
//                            title: {
//                                text: 'P（MPa）'
//                            }, plotLines: [{
//                                color: 'red',            //线的颜色，定义为红色
//                                dashStyle: 'dash', //标示线的样式，默认是solid（实线），这里定义为长虚线
//                                value: json.max,                //定义在哪个值上显示标示线，这里是在x轴上刻度为3的值处垂直化一条线
//                                width: 2                 //标示线的宽度，2px
//                    , label: {
//                        text: "" + json.max + "报警值",     //标签的内容
//                        align: 'right',                //标签的水平位置，水平居左,默认是水平居中center
//                        x: 0                         //标签相对于被定位的位置水平偏移的像素，重新定位，水平居左10px
//                    },
//                                zIndex: 0
//                            }]
//                        },
//                        tooltip: {
//                            valueSuffix: 'P（MPa）'
//                        },
//                        legend: {
//                            layout: 'vertical',
//                            align: 'right',
//                            verticalAlign: 'middle',
//                            borderWidth: 0
//                        },
//                        series: [{
//                            name: '压力',
//                            data: _data
//                        }]
//                    });
//                    $(".highcharts-credits").hide(); //去除水印
//                    //setTimeout(requestData, 10000);
//                },
//                error: function (jqXHR, textStatus, errorThrown) {
//                    /*弹出jqXHR对象的信息*/
//                    //alert(jqXHR.responseText);
//                    //alert(jqXHR.status);
//                    //alert(jqXHR.readyState);
//                    //alert(jqXHR.statusText);
//                    /*弹出其他两个参数的信息*/
//                    //alert(textStatus);
//                    //alert(errorThrown);
//                }
//            });
//            return false;
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