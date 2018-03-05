<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xitongcanshushezhi.aspx.cs" Inherits="xitongcanshushezhi" %>

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
         line-height:15px;
         height:15px;
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
                 .form-group .form-control,.form-group .select
                 {
                     height:30px;
                     width:100px;
                     }
    </style>
</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 系统管理 <span class="c-gray en">&gt;</span> 系统参数设置 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

    <div class="" style="margin-left: 15px;">
        <div class="row clearfix">
            <div class="col-md-12 column">
                <div class="tabbable" id="tabs-633966">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#panel-822577" data-toggle="tab">综采压力</a> </li>
                        
                        <li><a href="#panel-428793" data-toggle="tab">围岩移动</a> </li>
                        <li><a href="#panel-428794" data-toggle="tab">锚杆/索应力</a> </li>
                        <li><a href="#panel-weiyanyingli" data-toggle="tab">围岩应力</a> </li>
                        <li><a href="#panel-huozhusuoliang" data-toggle="tab">活柱缩量</a> </li>
                    </ul>
                    <div class="tab-content">
                        
                        <div class="tab-pane active" style="border: 0px solid #ddd; border-top: 0px; height: 100%; min-height: 500px;" id="panel-822577">
                            <nav class="navbar" role="navigation" style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<asp:DropDownList ID="ddl_cequ1" runat="server" OnSelectedIndexChanged ="ddl_test1_changed" AutoPostBack ="true" CssClass="select" Height="30"></asp:DropDownList>
                                            工作面名称：<asp:DropDownList ID="ddl_gongzuomian1" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            传感器组数：<asp:TextBox class="form-control" id="txtchuanganqizushu1" onkeyup="if(isNaN(value))execCommand('undo');" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server"></asp:TextBox>
                                            第一压力通道接：<asp:DropDownList ID="ddl_diyiyalitongdaojie1" runat="server" CssClass="select" Height="30" Width="120">
                                                <asp:ListItem Text="左柱" Value="左柱"></asp:ListItem>
                                                <asp:ListItem Text="右柱" Value="右柱"></asp:ListItem>
                                                <asp:ListItem Text="平衡千斤顶" Value="平衡千斤顶"></asp:ListItem>
                                                <asp:ListItem Text="不使用" Value="不使用"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                            缸径（mm）：<asp:TextBox class="form-control" onkeyup="if(isNaN(value))execCommand('undo');"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="gangjing1_1" runat="server"></asp:TextBox>
                                            压力上线（MPa）：<asp:TextBox class="form-control" onkeyup="if(isNaN(value))execCommand('undo');"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="yalishangxian1" runat="server" ></asp:TextBox>
                                            <br />第二压力通道接：<asp:DropDownList ID="ddl_dieryalitongdaojie1" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="左柱" Value="左柱"></asp:ListItem>
                                                <asp:ListItem Text="右柱" Value="右柱"></asp:ListItem>
                                                <asp:ListItem Text="平衡千斤顶" Value="平衡千斤顶"></asp:ListItem>
                                                <asp:ListItem Text="不使用" Value="不使用"></asp:ListItem>
                                            </asp:DropDownList>
                                            缸径（mm）：<asp:TextBox class="form-control" onkeyup="if(isNaN(value))execCommand('undo');" onafterpaste="if(isNaN(value))execCommand('undo')"  id="gangjing1_2" runat="server"></asp:TextBox>
                                            压力下线（MPa）：<asp:TextBox class="form-control" onkeyup="if(isNaN(value))execCommand('undo');"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="yalixiaxian1" runat="server"></asp:TextBox>
                                            平衡千斤顶报警值（MPa）：<asp:TextBox class="form-control" onkeyup="if(isNaN(value))execCommand('undo');"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="pinghengqianjindingbaojingzhi1" runat="server" ></asp:TextBox>
                                            <asp:LinkButton ID="lbtn_gongzuomiancanshu1" runat="server" CssClass="btn btn-success" OnClientClick="return check1_1()" onclick="lbtn_gongzuomiancanshu1_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_search1" runat="server" CssClass="btn btn-primary" onclick="lbtn_search1_Click"><i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
                                            <asp:Label ID="lbl_state1_1" runat="server" Text="i" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
			                </nav>
                            <div>
                            <table class="table table-border table-bordered table-hover table-bg ">
                                <thead>
                                <tr><th colspan="11" style="text-align:center;font-size:20px;">
                                工作面列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="100" width="200">序号</th>
                                        <th style="text-align:center;" width="100">工作面名称</th>
                                        <th style="text-align:center;" width="100">第一通道</th>
                                        <th style="text-align:center;" width="100">第二通道</th>
                                        <th style="text-align:center;" width="100">缸径1</th>
                                        <th style="text-align:center;" width="100">缸径2</th>
                                        <th style="text-align:center;" width="100">压力上线</th>
                                        <th style="text-align:center;" width="100">压力下线</th>
                                        <th style="text-align:center;" width="100">压力报警值</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="border:0px;"></th>
                                    </tr>
                                </thead>
                                </table>
                                </div>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg ">
                                <tbody >
                                
                                <asp:Repeater ID="rpt_gongzuomiancanshu1" runat="server" OnItemCommand="rpt_gongzuomiancanshu1_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="100"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("facename") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("firstconnect") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("secondconnect") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("firstd") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("sencondd") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("pressuremax") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("pressuremin") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("pressurealarm") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_facename" runat="server" Text='<%#Eval("facename") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                               
                                </tbody>
                            </table>
                            </div>
                            <br />
                            <nav class="navbar" role="navigation"  style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div3">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
                                            所属工作面：<asp:DropDownList ID="ddl_gongzuomian1_2" runat="server"  CssClass="select" Height="30"></asp:DropDownList>
                                            传感器编号：<input type="text" class="form-control" id="txtchuangganqibianhao" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            压力类型：<asp:DropDownList ID="ddl_yalileixing" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="液压支架" Value="液压支架"></asp:ListItem>
                                                <asp:ListItem Text="平衡千斤顶" Value="平衡千斤顶"></asp:ListItem>
                                            </asp:DropDownList>
                                            支架编号：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtzhijiabianhao" runat="server" />
                                            距离材料巷/安装位置：<input type="text" class="form-control"  id="txtjulicailiaoxiang" runat="server" />
                                            使用状态：<asp:DropDownList ID="ddl_state_12" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtn_chuanganqi" runat="server" CssClass="btn btn-success" OnClientClick="return check1_2()" onclick="lbtn_chuanganqi_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:Label ID="lbl_state1_2" runat="server" Text="i" Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_cequmingcheng1_2" runat="server" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
				
			                </nav>
                            
                            <table class="table table-border table-bordered table-hover table-bg">
                                <thead>
                                <tr><th colspan="8" style="text-align:center;font-size:20px;">
                                传感器列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="150">序号</th>
                                        <th style="text-align:center;" width="150">传感器编号</th>
                                        <th style="text-align:center;" width="150">支架编号</th>
                                        <th style="text-align:center;" width="150">压力类型</th>
                                        <th style="text-align:center;" width="150">安装位置</th>
                                        <th style="text-align:center;" width="150">使用状态</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="text-align:center;"></th>
                                    </tr>
                                </thead>
                                </table>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg">
                                <tbody>
                                <asp:Repeater ID="rpt_chuanganqi" runat="server" OnItemCommand="rpt_chuanganqi_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="150"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("sensorno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("bracketno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("type") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("distance") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("usestate") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_facename" runat="server" Text='<%#Eval("facename") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_bracketno" runat="server" Text='<%#Eval("bracketno") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                                </tbody>
                            </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="panel-428793">
                            <nav class="navbar" role="navigation" style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div1">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<asp:DropDownList ID="ddl_cequ2_1" runat="server" OnSelectedIndexChanged ="ddl_test2_changed" AutoPostBack ="true" CssClass="select" Height="30"></asp:DropDownList>
                                            巷道名称：<asp:DropDownList ID="ddl_xiangdao_2_1" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            传感器组数：<input type="text" class="form-control" id="txtchuanganqizushu2_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            
                                            位移预警值（mm）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtweiyiyujingzhi2_1" runat="server" />
                                            
                                            位移报警值（mm）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtweiyibaojingzhi2_1" runat="server" />
                                            <asp:LinkButton ID="lbtn_save2_1" runat="server" CssClass="btn btn-success" OnClientClick="return check2_1()" onclick="lbtn_save2_1_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_search2" runat="server" CssClass="btn btn-primary" onclick="lbtn_search2_Click"><i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ2_1" runat="server" Text="i" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
			                </nav>
                            <div>
                            <table class="table table-border table-bordered table-hover table-bg ">
                                <thead>
                                <tr><th colspan="11" style="text-align:center;font-size:20px;">
                                巷道参数列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="100" width="200">序号</th>
                                        <th style="text-align:center;" width="200">巷道名称名称</th>
                                        <th style="text-align:center;" width="100">位移预警值</th>
                                        <th style="text-align:center;" width="100">位移报警值</th>
                                        <th style="text-align:center;" width="100">传感器组数</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="border:0px;"></th>
                                    </tr>
                                </thead>
                                </table>
                                </div>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg ">
                                <tbody >
                                
                                <asp:Repeater ID="rpt_xiangdaocanshuliebiao" runat="server" OnItemCommand="rpt_xiangdaocanshuliebiao_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="100"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="200"><%#Eval("roadwayname") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("disyujingvale") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("displacementalarm") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("sennumber") %></td>
                                            
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                               
                                </tbody>
                            </table>
                            </div>
                            <br />
                            <nav class="navbar" role="navigation"  style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div6">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
                                            所属巷道：<asp:DropDownList ID="ddl_suoshuxiangdao2_2" runat="server"  CssClass="select" Height="30"></asp:DropDownList>
                                            传感器编号：<input type="text" class="form-control" id="txtchuanganqibianhao2_2" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            安装位置：<input type="text" class="form-control"  id="txtanzhuangweizhi2_2" runat="server" />
                                            A基点深度（m）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtajidianshendu" runat="server" />
                                            B基点深度（m）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtbjidianshendu" runat="server" />
                                            使用状态：<asp:DropDownList ID="ddl_shiyongzhuangtai2_2" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtn_save_2_2" runat="server" CssClass="btn btn-success" OnClientClick="return check2_2()" onclick="lbtn_save_2_2_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ2_2" runat="server" Text="i" Visible="false"></asp:Label>
                                            <asp:Label ID="lblcequmingcheng2_2" runat="server" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
				
			                </nav>
                            
                            <table class="table table-border table-bordered table-hover table-bg">
                                <thead>
                                <tr><th colspan="8" style="text-align:center;font-size:20px;">
                                传感器列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="150">序号</th>
                                        <th style="text-align:center;" width="150">传感器编号</th>
                                        <th style="text-align:center;" width="150">安装位置</th>
                                        <th style="text-align:center;" width="150">A基点深度</th>
                                        <th style="text-align:center;" width="150">B基点深度</th>
                                        <th style="text-align:center;" width="150">使用状态</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="text-align:center;"></th>
                                    </tr>
                                </thead>
                                </table>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg">
                                <tbody>
                                <asp:Repeater ID="rpt_chuanganqiliebiao2_2" runat="server" OnItemCommand="rpt_chuanganqiliebiao2_2_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="150"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("sensorno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("location") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("pointdeptha") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("pointdepthb") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("usestate") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                                </tbody>
                            </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="panel-428794">
                            <nav class="navbar" role="navigation" style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div2">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<asp:DropDownList ID="ddl_cequmingcheng3_1" runat="server" OnSelectedIndexChanged ="ddl_test3_changed" AutoPostBack ="true" CssClass="select" Height="30"></asp:DropDownList>
                                            巷道名称：<asp:DropDownList ID="ddl_xiangdaomingcheng3_1" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            传感器组数：<input type="text" class="form-control" id="txtchuanganqizushu3_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            
                                            直径（mm）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtzhijing3_1" runat="server" />
                                            锚杆抗拉强度（MPa):<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtmaogankanglaqiangdu3_1" runat="server" />
                                            <input type="button" value="计算" class="btn btn-primary" onclick="fun()" />
                                            <br />锚杆报警值（KN）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtmaoganbaojingzhi3_1" runat="server" />
                                            
                                            锚杆预紧力（KN）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtmaoganyujinli3_1" runat="server" />
                                            锚索预紧力（KN）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtmaosuoyujinli3_1" runat="server" />
                                            锚索报警值（mm）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtmaosuobaojingzhi3_1" runat="server" />
                                            <asp:LinkButton ID="lbtn_save3_1" runat="server" CssClass="btn btn-success" OnClientClick="return check3_1()" onclick="lbtn_save3_1_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_search3" runat="server" CssClass="btn btn-primary" onclick="lbtn_search3_Click"><i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ3_1" runat="server" Text="i" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
			                </nav>
                            <div>
                            <table class="table table-border table-bordered table-hover table-bg ">
                                <thead>
                                <tr><th colspan="11" style="text-align:center;font-size:20px;">
                                巷道参数列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="100" width="200">序号</th>
                                        <th style="text-align:center;" width="200">巷道名称</th>
                                        <th style="text-align:center;" width="100">直径</th>
                                        <th style="text-align:center;" width="100">锚杆抗拉强度</th>
                                        <th style="text-align:center;" width="100">锚杆报警值</th>
                                        <th style="text-align:center;" width="100">锚杆预紧力</th>
                                        <th style="text-align:center;" width="100">锚索报警值</th>
                                        <th style="text-align:center;" width="100">锚索预紧力</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="border:0px;"></th>
                                    </tr>
                                </thead>
                                </table>
                                </div>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg ">
                                <tbody >
                                
                                <asp:Repeater ID="rpt_xiangdaocanshuliebiao3_1" runat="server" OnItemCommand="rpt_xiangdaocanshuliebiao3_1_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="100"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="200"><%#Eval("roadwayName")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("d") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("p") %></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("AlarmMaxMGF")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("AlarmMGF")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("AlarmMaxMSF")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("AlarmMSF")%></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                               
                                </tbody>
                            </table>
                            </div>
                            <br />
                            <nav class="navbar" role="navigation"  style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div7">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
                                            所属巷道：<asp:DropDownList ID="ddl_suoshuxiangdao3_2" runat="server"  CssClass="select" Height="30"></asp:DropDownList>
                                            传感器编号：<input type="text" class="form-control" id="txtchuanganqibianhao3_2" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            锚固类型：<asp:DropDownList ID="ddl_maoguleixing3_2" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="锚杆" Value="锚杆"></asp:ListItem>
                                                <asp:ListItem Text="锚索" Value="锚索"></asp:ListItem>
                                            </asp:DropDownList>
                                            安装位置：<input type="text" class="form-control"  id="txtanzhuangweizhi3_2" runat="server" />
                                            初装值（KN）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtchuzhuangzhi3_2" runat="server" />
                                            使用状态：<asp:DropDownList ID="ddl_shiyongzhuangtai3_2" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtn_save_3_2" runat="server" CssClass="btn btn-success" OnClientClick="return check3_2()" onclick="lbtn_save_3_2_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ3_2" runat="server" Text="i" Visible="false"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
				
			                </nav>
                            
                            <table class="table table-border table-bordered table-hover table-bg">
                                <thead>
                                <tr><th colspan="8" style="text-align:center;font-size:20px;">
                                传感器列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="150">序号</th>
                                        <th style="text-align:center;" width="150">传感器编号</th>
                                        <th style="text-align:center;" width="150">锚固类型</th>
                                        <th style="text-align:center;" width="150">安装位置</th>
                                        <th style="text-align:center;" width="150">初装值</th>
                                        <th style="text-align:center;" width="150">使用状态</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="text-align:center;"></th>
                                    </tr>
                                </thead>
                                </table>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg">
                                <tbody>
                                <asp:Repeater ID="rpt_chuanganqiliebiao3_2" runat="server" OnItemCommand="rpt_chuanganqiliebiao3_2_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="150"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("sensorno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("mgtype") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("location") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("starvalue") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("usestate") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_SenNumber" runat="server" Text='<%#Eval("sensorno") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                                </tbody>
                            </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="panel-weiyanyingli">
                            <nav class="navbar" role="navigation" style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div4">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<asp:DropDownList ID="ddl_cequmingcheng4_1" OnSelectedIndexChanged ="ddl_test4_changed" AutoPostBack ="true" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            巷道名称：<asp:DropDownList ID="ddl_xiangdaomingcheng4_1" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            传感器组数：<input type="text" class="form-control" id="txtchuanganqizushu4_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            <asp:LinkButton ID="lbtn_save4_1" runat="server" CssClass="btn btn-success" OnClientClick="return check4_1()" onclick="lbtn_save4_1_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_search4" runat="server" CssClass="btn btn-primary" onclick="lbtn_search4_Click"><i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ4_1" runat="server" Text="i" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
			                </nav>
                            <div>
                            <table class="table table-border table-bordered table-hover table-bg ">
                                <thead>
                                <tr><th colspan="11" style="text-align:center;font-size:20px;">
                                巷道参数列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="100" width="200">序号</th>
                                        <th style="text-align:center;" width="200">巷道名称</th>
                                        <th style="text-align:center;" width="100">传感器组数</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="border:0px;"></th>
                                    </tr>
                                </thead>
                                </table>
                                </div>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg ">
                                <tbody >
                                
                                <asp:Repeater ID="rpt_xiangdaocanshuliebiao4_1" runat="server" OnItemCommand="rpt_xiangdaocanshuliebiao4_1_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="100"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="200"><%#Eval("roadwayName")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("sennumber")%></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                               
                                </tbody>
                            </table>
                            </div>
                            <br />
                            <nav class="navbar" role="navigation"  style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div8">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
                                            所属巷道：<asp:DropDownList ID="ddl_suoshuxiangdao4_2" runat="server"  CssClass="select" Height="30"></asp:DropDownList>
                                            传感器编号：<input type="text" class="form-control" id="txtchuanganqibianhao4_2" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            
                                            安装位置：<input type="text" class="form-control"  id="txtanzhuangweizhi4_2" runat="server" />
                                            安装深度（m）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtanzhuangshendu4_2" runat="server" />
                                            使用状态：<asp:DropDownList ID="ddl_shiyongzhuangtai4_2" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtn_save_4_2" runat="server" CssClass="btn btn-success" OnClientClick="return check4_2()" onclick="lbtn_save_4_2_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ4_2" runat="server" Text="i" Visible="false"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
				
			                </nav>
                            
                            <table class="table table-border table-bordered table-hover table-bg">
                                <thead>
                                <tr><th colspan="8" style="text-align:center;font-size:20px;">
                                传感器列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="150">序号</th>
                                        <th style="text-align:center;" width="150">传感器编号</th>
                                        <th style="text-align:center;" width="150">位置（m）</th>
                                        <th style="text-align:center;" width="150">深度（m）</th>
                                        <th style="text-align:center;" width="150">使用状态</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="text-align:center;"></th>
                                    </tr>
                                </thead>
                                </table>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg">
                                <tbody>
                                <asp:Repeater ID="rpt_chuanganqiliebiao4_2" runat="server" OnItemCommand="rpt_chuanganqiliebiao4_2_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="150"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("sensorno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("location") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("depth") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("usestate") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_roadwayname" runat="server" Text='<%#Eval("roadwayname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_SenNumber" runat="server" Text='<%#Eval("sensorno") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("roadwayname") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                                </tbody>
                            </table>
                            </div>
                        </div>
                        <div class="tab-pane" id="panel-huozhusuoliang">
                    
                            <nav class="navbar" role="navigation" style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div5">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
							                测区名称：<asp:DropDownList ID="ddl_cequmingcheng5_1" runat="server" OnSelectedIndexChanged ="ddl_test_changed" AutoPostBack ="true" CssClass="select" Height="30"></asp:DropDownList>
                                            工作面名称：<asp:DropDownList ID="ddl_gongzuomianmingcheng5_1" runat="server" CssClass="select" Height="30"></asp:DropDownList>
                                            传感器组数：<input type="text" class="form-control" id="txtchuanganqizushu5_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            缩量预警值（mm）：<input type="text" class="form-control" id="txtsuoliangyujingzhi5_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            缩量报警值（mm）：<input type="text" class="form-control" id="suoliangbaojingzhi5_1" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            <asp:LinkButton ID="lbtn_save5_1" runat="server" CssClass="btn btn-success" OnClientClick="return check5_1()" onclick="lbtn_save5_1_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_search5" runat="server" CssClass="btn btn-primary" onclick="lbtn_search5_Click"><i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ5_1" runat="server" Text="i" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
			                </nav>
                            <div>
                            <table class="table table-border table-bordered table-hover table-bg ">
                                <thead>
                                <tr><th colspan="11" style="text-align:center;font-size:20px;">
                                工作面参数列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="100" width="200">序号</th>
                                        <th style="text-align:center;" width="200">工作面名称</th>
                                        <th style="text-align:center;" width="100">缩量预警值</th>
                                        <th style="text-align:center;" width="100">缩量报警值</th>
                                        <th style="text-align:center;" width="100">传感器组数</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="border:0px;"></th>
                                    </tr>
                                </thead>
                                </table>
                                </div>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg ">
                                <tbody >
                                
                                <asp:Repeater ID="rpt_gongzuomiancanshuliebiao5_1" runat="server" OnItemCommand="rpt_gongzuomiancanshuliebiao5_1_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="100"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="200"><%#Eval("facename")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("yujingvalue")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("alarmvalue")%></td>
                                            <td align="center" style="text-align:center;" width="100"><%#Eval("sennumber")%></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_facenamee" runat="server" Text='<%#Eval("facename") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
                                        </tr>
                                   </ItemTemplate> 
                               </asp:Repeater>
                               
                                </tbody>
                            </table>
                            </div>
                            <br />
                            <nav class="navbar" role="navigation"  style="border:1px solid #d2d2d2;">
				                <div class="navbar-header">
					                 <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button> 
				                </div>
				                <div class="collapse navbar-collapse" id="Div9">
					                <div class="navbar-form navbar-left" role="search">
						                <div class="form-group"> 
                                            所属工作面：<asp:DropDownList ID="ddl_suoshugongzuomian5_2" runat="server"  CssClass="select" Height="30"></asp:DropDownList>
                                            传感器编号：<input type="text" class="form-control" id="txtchuanganqibianhao5_2" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" />
                                            
                                            支架编号：<input type="text" class="form-control"  id="txtzhijiabianhao5_2" runat="server" />
                                            距离材料巷（m）：<input type="text" class="form-control" onkeyup="if(isNaN(value))execCommand('undo');cal()"  onafterpaste="if(isNaN(value))execCommand('undo')"  id="txtjulicailiaoxiang5_2" runat="server" />
                                            使用状态：<asp:DropDownList ID="ddl_shiyongzhuangtai5_2" runat="server" CssClass="select" Height="30">
                                                <asp:ListItem Text="使用" Value="使用"></asp:ListItem>
                                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtn_save_5_2" runat="server" CssClass="btn btn-success" OnClientClick="return check5_2()" onclick="lbtn_save_5_2_Click"><i class="Hui-iconfont">&#xe600;</i>保存</asp:LinkButton>
                                            <asp:Label ID="lbl_cequ5_2" runat="server" Text="i" Visible="false"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
						                </div> 
                                        
					                </div>
				                </div>
				
			                </nav>
                            
                            <table class="table table-border table-bordered table-hover table-bg">
                                <thead>
                                <tr><th colspan="8" style="text-align:center;font-size:20px;">
                                传感器列表
                                </th></tr>
                                    <tr>
                                        <th style="text-align:center;" width="150">序号</th>
                                        <th style="text-align:center;" width="150">传感器编号</th>
                                        <th style="text-align:center;" width="150">支架编号</th>
                                        <th style="text-align:center;" width="150">距离材料巷（m）</th>
                                        <th style="text-align:center;" width="150">使用状态</th>
                                        <th style="text-align:center;" width="200">操作</th>
                                        <th style="text-align:center;"></th>
                                    </tr>
                                </thead>
                                </table>
                                <div style="height:220px;overflow-y:scroll;margin-top:-22px;" >
                                <table class="table table-border table-bordered table-hover table-bg">
                                <tbody>
                                <asp:Repeater ID="rpt_chuanganqiliebiao5_2" runat="server" OnItemCommand="rpt_chuanganqiliebiao5_2_ItemCommand">
                                   <ItemTemplate>
                                       <tr>
                                            <td align="center" style="text-align:center;" width="150"><%#Container.ItemIndex+1 %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("sensorno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("bracketno") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("distance") %></td>
                                            <td align="center" style="text-align:center;" width="150"><%#Eval("usestate") %></td>
                                            <td align="center" style="text-align:center;" width="200">
                                            <asp:Label ID="lbl_facename" runat="server" Text='<%#Eval("facename") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_areaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_SenNumber" runat="server" Text='<%#Eval("sensorno") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_delete" runat="server" OnClientClick="return confirm('删除此数据可能对其它数据产生影响，是否删除');"  CommandName="lbtn_delete" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                            </td>
                                            <td style="border:0px;"></td>
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

        function fun() {
            var $baojingzhi = $("#txtmaoganbaojingzhi3_1");
            var $zhijing = $("#txtzhijing3_1");
            var $qiangdu = $("#txtmaogankanglaqiangdu3_1");
            
            if ($zhijing.val() == "") {
                $zhijing.val("0");
                $qiangdu.val("0");
                $baojingzhi.val("0");
            }
            
            if ($qiangdu.val() == "") {
                $zhijing.val("0");
                $qiangdu.val("0");
                $baojingzhi.val("0");
            }
//            Dim r As Single = Convert.ToSingle(TxtD.Text) / 2000
//            Dim p As Single = Convert.ToSingle(TxtPa.Text) * 10 ^ 6
//            Dim f As Single = Math.PI * r ^ 2 * p * 0.8 / 1000

            var r=parseFloat($zhijing.val());
            var p = parseFloat($qiangdu.val());
            var jieguo = Math.PI * (r / 2000) * (r / 2000) * (p * 1000000) * 0.8 / 1000;
            $baojingzhi.val(Math.round( parseFloat(jieguo)));
            
        }
        function check1_1() {
            if ($("#ddl_cequ1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_gongzuomian1").val() == "0") {
                alert("请选择工作面名称");
                return false;
            }
            if ($("#txtchuanganqizushu1").val() == "") {
                alert("传感器组数不能为空");
                return false;
            }
            if ($("#gangjing1_1").val() == "") {
                alert("第一压力通道缸径不能为空");
                return false;
            }
            if ($("#yalishangxian1").val() == "") {
                alert("压力上线不能为空");
                return false;
            }
            if ($("#gangjing1_2").val() == "") {
                alert("第二压力通道缸径不能为空");
                return false;
            }
            if ($("#yalixiaxian1").val() == "") {
                alert("压力下线不能为空");
                return false;
            }
            if ($("#pinghengqianjindingbaojingzhi1").val() == "") {
                alert("平衡千斤顶报警值不能为空");
                return false;
            }

        }
        function check1_2() {
            if ($("#ddl_cequ1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_gongzuomian1_2").val() == "0") {
                alert("请选择所属工作面");
                return false;
            }
            if ($("#txtchuangganqibianhao").val() == "") {
                alert("传感器编号不能为空");
                return false;
            }
            if ($("#txtzhijiabianhao").val() == "") {
                alert("支架编号不能为空");
                return false;
            }
            if ($("#txtjulicailiaoxiang").val() == "") {
                alert("距离材料巷不能为空");
                return false;
            }
        }
        function check2_1() {
            if ($("#ddl_cequ2_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_xiangdao_2_1").val() == "0") {
                alert("请选择巷道名称");
                return false;
            }
            if ($("#txtchuanganqizushu2_1").val() == "") {
                alert("传感器组数不能为空");
                return false;
            }
            if ($("#txtweiyiyujingzhi2_1").val() == "") {
                alert("位移预警值不能为空");
                return false;
            }
            if ($("#txtweiyibaojingzhi2_1").val() == "") {
                alert("位移报警值不能为空");
                return false;
            }
        }
        function check2_2() {
            if ($("#ddl_cequ2_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_suoshuxiangdao2_2").val() == "0") {
                alert("请选择所属巷道");
                return false;
            }
            if ($("#txtchuanganqibianhao2_2").val() == "") {
                alert("传感器编号不能为空");
                return false;
            }
            if ($("#txtanzhuangweizhi2_2").val() == "") {
                alert("安装位置不能为空");
                return false;
            }
            if ($("#txtajidianshendu").val() == "") {
                alert("A基点深度不能为空");
                return false;
            }
            if ($("#txtbjidianshendu").val() == "") {
                alert("B基点深度不能为空");
                return false;
            }
        }
        function check3_1() {
            if ($("#ddl_cequmingcheng3_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_xiangdaomingcheng3_1").val() == "0") {
                alert("请选择所属巷道");
                return false;
            }
            if ($("#txtchuanganqizushu3_1").val() == "") {
                alert("传感器组数不能为空");
                return false;
            }
            if ($("#txtzhijing3_1").val() == "") {
                alert("直径不能为空");
                return false;
            }
            if ($("#txtmaogankanglaqiangdu3_1").val() == "") {
                alert("锚杆抗拉强度不能为空");
                return false;
            }
            if ($("#txtmaoganbaojingzhi3_1").val() == "") {
                alert("锚杆报警值不能为空");
                return false;
            }
            if ($("#txtmaoganyujinli3_1").val() == "") {
                alert("锚杆预紧力不能为空");
                return false;
            }
            if ($("#txtmaosuoyujinli3_1").val() == "") {
                alert("锚索预紧力不能为空");
                return false;
            }
            if ($("#txtmaosuobaojingzhi3_1").val() == "") {
                alert("锚索报警值不能为空");
                return false;
            }
        }
        function check3_2() {
            if ($("#ddl_cequmingcheng3_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_suoshuxiangdao3_2").val() == "0") {
                alert("请选择所属巷道");
                return false;
            }
            if ($("#txtchuanganqibianhao3_2").val() == "") {
                alert("传感器编号不能为空");
                return false;
            }
            if ($("#txtanzhuangweizhi3_2").val() == "") {
                alert("安装位置不能为空");
                return false;
            }
            if ($("#txtchuzhuangzhi3_2").val() == "") {
                alert("初装值不能为空");
                return false;
            }
        }
        function check4_1() {
            if ($("#ddl_cequmingcheng4_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_xiangdaomingcheng4_1").val() == "0") {
                alert("请选择所属巷道");
                return false;
            }
            if ($("#txtchuanganqizushu4_1").val() == "") {
                alert("传感器组数不能为空");
                return false;
            }
        }
        function check4_2() {
            if ($("#ddl_cequmingcheng4_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_suoshuxiangdao4_2").val() == "0") {
                alert("请选择所属巷道");
                return false;
            }
            if ($("#txtchuanganqibianhao4_2").val() == "") {
                alert("传感器编号不能为空");
                return false;
            }
            if ($("#txtanzhuangweizhi4_2").val() == "") {
                alert("安装位置不能为空");
                return false;
            }
            if ($("#txtanzhuangshendu4_2").val() == "") {
                alert("安装深度不能为空");
                return false;
            }
        }
        function check5_1() {
            if ($("#ddl_cequmingcheng5_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_gongzuomianmingcheng5_1").val() == "0") {
                alert("请选择工作面名称");
                return false;
            }
            if ($("#txtchuanganqizushu5_1").val() == "") {
                alert("传感器组数不能为空");
                return false;
            }
            if ($("#txtsuoliangyujingzhi5_1").val() == "") {
                alert("缩量预警值不能为空");
                return false;
            }
            if ($("#suoliangbaojingzhi5_1").val() == "") {
                alert("缩量报警值不能为空");
                return false;
            }
        }
        function check5_2() {
            if ($("#ddl_cequmingcheng5_1").val() == "0") {
                alert("请选择测区名称");
                return false;
            }
            if ($("#ddl_suoshugongzuomian5_2").val() == "0") {
                alert("请选择所属工作面名称");
                return false;
            }
            if ($("#txtchuanganqibianhao5_2").val() == "") {
                alert("传感器编号不能为空");
                return false;
            }
            if ($("#txtzhijiabianhao5_2").val() == "") {
                alert("支架编号不能为空");
                return false;
            }
            if ($("#txtjulicailiaoxiang5_2").val() == "") {
                alert("距离材料巷不能为空");
                return false;
            }
        }
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
            //            $('#panel-822577').css('display', 'none');
            //            $('#panel-428793').css('display', 'block');
            //            $('#panel-428794').css('display', 'none');
            //alert("操作成功");
        }
        function changclass3() {
            $("#tabs-633966 a").eq(2).click();
            //            $('#panel-822577').css('display', 'none');
            //            $('#panel-428793').css('display', 'none');
            //            $('#panel-428794').css('display', 'block');
            //alert("操作成功");
        }
        function changclass4() {
            $("#tabs-633966 a").eq(3).click();
            //            $('#panel-822577').css('display', 'none');
            //            $('#panel-428793').css('display', 'none');
            //            $('#panel-428794').css('display', 'block');
            //alert("操作成功");
        }
        function changclass5() {
            $("#tabs-633966 a").eq(4).click();
            //            $('#panel-822577').css('display', 'none');
            //            $('#panel-428793').css('display', 'none');
            //            $('#panel-428794').css('display', 'block');
            //alert("操作成功");
        }
    </script>
    
    </form>
</body>
</html>