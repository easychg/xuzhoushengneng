<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kuangjingxinxishezhi.aspx.cs"
    Inherits="kuangjingxinxishezhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!--[if lt IE 9]>
    <script type="text/javascript" src="bootstrap/js/html5shiv.js"></script>
    <script type="text/javascript" src="bootstrap/js/respond.min.js"></script>
    <![endif]-->

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <link href="bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.8/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
    
    <!--[if IE 6]>
    <script type="text/javascript" src="bootstrap/js/DD_belatedPNG_0.0.8a-min.js" ></script>
    <script>DD_belatedPNG.fix('*');</script>
    <![endif]-->
    <title>Bootstrap 3</title>
    <style type="text/css">
    a:hover, .active a
    {
        color:#fff;
        }
     .table th, .table td
     {
         line-height:40px;
         height:40px;
         border-top:0px;
         }
     label
     {
         font-weight:100;
         }
         .tab-pane
         {
             width:99%;
             }
             .breadcrumb
             {
                 padding:0 15px;
                 font-size:14px;
                 }
    </style>
</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 系统管理 <span class="c-gray en">&gt;</span> 矿井信息设置设置 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    
    <form runat="server">
    <div class="" style="margin-left: 15px;">
        <div class="row clearfix">
            <div class="col-md-12 column">
                <div class="tabbable" id="tabs-633966">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#panel-822577" data-toggle="tab">测区信息</a> </li>
                        <li><a href="#panel-428793" data-toggle="tab">工作面信息</a> </li>
                        <li><a href="#panel-428794" data-toggle="tab">巷道信息</a> </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" style="border: 0px solid #ddd; border-top: 0px; height: 100%; min-height: 500px;" id="panel-822577">
                            <nav class="navbar" role="navigation">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				
				                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<input type="text" class="form-control" id="txtcequ" runat="server" />
						                </div> 
                                        <asp:LinkButton ID="lbtn_cequ" runat="server" CssClass="btn btn-success" onclick="lbtn_cequ_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
					                </div>
				                </div>
				
			                </nav>
                            <table class="table table-border table-bordered table-hover table-bg table-sort">
                                <thead>
                                    <tr>
                                        <th style="text-align:center;" width="100">序号</th>
                                        <th style="text-align:center;">测区名称</th>
                                    </tr>
                                </thead>
                                <tbody>
                                <asp:Repeater ID="rpt_cequ" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;"><%#Eval("areaname") %></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="panel-428793">
                            <nav class="navbar" role="navigation">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				
				                <div class="collapse navbar-collapse" id="Div1">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group">
                                        测区名称：<asp:DropDownList ID="ddl_AreaInfo" runat="server" CssClass="select" Height="30" Width="120">
                                         </asp:DropDownList>
							                工作面名称：<input type="text" class="form-control" id="txtgongzuomian" runat="server" />
						                </div> 
                                        <asp:LinkButton ID="lbtn_gongzuomian" runat="server" CssClass="btn btn-success" OnClientClick="return check2()" onclick="lbtn_gongzuomian_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
					                </div>
				                </div>
				
			                </nav>
                            <table class="table table-border table-bordered table-hover table-bg table-sort">
                                <thead>
                                    <tr>
                                        <th style="text-align:center;" width="100">序号</th>
                                        <th style="text-align:center;">测区名称</th>
                                        <th style="text-align:center;">工作面名称</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt_workface" runat="server">
                                       <ItemTemplate>
                                           <tr>
                                                <td align="center" style="text-align:center;"><%#Container.ItemIndex+1 %></td>
                                                <td align="center" style="text-align:center;"><%#Eval("areaname") %></td>
                                                <td align="center" style="text-align:center;"><%#Eval("workfacename") %></td>
                                            </tr>
                                       </ItemTemplate> 
                                   </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="panel-428794">
                            <nav class="navbar" role="navigation">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				
				                <div class="collapse navbar-collapse" id="Div2">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group">
                                        测区名称：<asp:DropDownList ID="ddl_cequ2" runat="server" CssClass="select" Height="30" Width="120">
                                         </asp:DropDownList>
							                巷道名称：<input type="text" class="form-control" id="txtxiangdao" runat="server" />
						                </div> 
                                        <asp:LinkButton ID="lbtn_xiangdao" runat="server" CssClass="btn btn-success" OnClientClick="return check3()" onclick="lbtn_xiangdao_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
					                </div>
				                </div>
				
			                </nav>
                            <table class="table table-border table-bordered table-hover table-bg table-sort">
                                <thead>
                                    <tr>
                                        <th style="text-align:center;" width="100">序号</th>
                                        <th style="text-align:center;">测区名称</th>
                                        <th style="text-align:center;">工作面名称</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpt_xiangdao" runat="server">
                                       <ItemTemplate>
                                           <tr>
                                                <td align="center" style="text-align:center;"><%#Container.ItemIndex+1 %></td>
                                                <td align="center" style="text-align:center;"><%#Eval("areaname") %></td>
                                                <td align="center" style="text-align:center;"><%#Eval("roadname") %></td>
                                            </tr>
                                       </ItemTemplate> 
                                   </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="bootstrap/js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script> 
    <script type="text/javascript">
        $(function () {
            $('.table-sort').dataTable({
                "bStateSave": false, //状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0]}// 制定列不参与排序
                ]
            });


        });
        
        function check2() {
            if ($("#ddl_AreaInfo").val() == "0") {
                alert("测区名称不能为空");
                return false;
            }
            if ($("#txtgongzuomian").val() == "") {
                alert("工作面名称不能为空");
                return false;
            }
        }
        function check3() {
            if ($("#ddl_cequ2").val() == "0") {
                alert("测区名称不能为空");
                return false;
            }
            if ($("#txtxiangdao").val() == "") {
                alert("巷道名称不能为空");
                return false;
            }
        }
        function changclass1() {
//            $('#panel-822577').css('display', 'block');
//            $('#panel-428793').css('display', 'none');
//            $('#panel-428794').css('display', 'none');
            $("#tabs-633966 a").eq(0).click();
            alert("操作成功");
        }
        function changclass2() {
            $("#tabs-633966 a").eq(1).click();
            alert("操作成功");
        }
        function changclass22() {
            $("#tabs-633966 a").eq(1).click();
        }
        function changclass3() {
            $("#tabs-633966 a").eq(2).click();
            alert("操作成功");
        }
        function changclass33() {
            $("#tabs-633966 a").eq(2).click();
        }
    </script></form>
</body>
</html>
