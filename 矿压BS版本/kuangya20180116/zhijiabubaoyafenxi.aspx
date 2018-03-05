<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zhijiabubaoyafenxi.aspx.cs" Inherits="zhijiabubaoyafenxi" %>

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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 综采压力管理 <span class="c-gray en">&gt;</span> 支架不保压分析 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
        日期范围：
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
        <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" style="display:inline-block" >  
                            <ContentTemplate >
                            
                选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160"></asp:DropDownList>
            选择工作面：<asp:DropDownList ID="ddl_gongzuomian" runat="server" OnSelectedIndexChanged="ddl_gongzuomian_changed" AutoPostBack="true" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            
            支架编号：<asp:DropDownList ID="ddl_zhijiabianhao" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
            </ContentTemplate>
            </asp:UpdatePanel>
            <%--<div class="radio-box">
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
              </div>--%>
              <asp:LinkButton ID="search" runat="server" 
                OnClientClick="return requestData();" CssClass="btn btn-success radius" 
                onclick="search_Click" > 刷新</asp:LinkButton>
                <asp:LinkButton ID="lbtn_export" runat="server" 
                CssClass="btn btn-success radius" 
                onclick="lbtn_export_Click" > 导出图片</asp:LinkButton>
            </div>
            
            
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l">
        
        </span></div>
        <div class="mt-20">
            <asp:Panel ID="panel1" runat="server" Width="1370" Height="700" style="position:absolute;background-size: 1370px 700px;">
                <hr id="hrx" style="color:Red;z-index:999;position:absolute;margin-top:700px;margin-left:50px;width:1200px;"/>
                <hr id="hry" style="color:Red;z-index:999;position:absolute;margin-top:88px;margin-left:50px;width:1px;height:520px;"/>
            </asp:Panel>
            <asp:TextBox ID="txtjson" runat="server" Width="1000" style="display:none;" ></asp:TextBox>
            <asp:label ID="txtshow" runat="server" Width="1000" Font-Size="12" ForeColor="Red" style="margin-top:695px;"></asp:label>
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