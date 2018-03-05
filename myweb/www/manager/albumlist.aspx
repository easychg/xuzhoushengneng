<%@ Page Language="C#" AutoEventWireup="true" CodeFile="albumlist.aspx.cs" Inherits="manager_albumlist" %>

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
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.1/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="lib/icheck/icheck.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>管理员组</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 相册管理 <span class="c-gray en">&gt;</span> 相册列表 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
            日期范围：
	
            <input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'datemax\')||\'%y-%M-%d\'}'})" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            -
	
            <input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'datemin\')}',maxDate:'%y-%M-%d'})" id="datemax" runat="server" class="input-text Wdate" style="width: 120px;"/>
            <input type="text" class="input-text" style="width: 250px" placeholder="输入相册标题" id="qitatiaojian" runat="server" name=""/>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"><i class="Hui-iconfont">&#xe665;</i> 查询</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l"><a href="albumadd.aspx" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加相册</a></span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th >导航栏</th>
                        <th>标题</th>
                        <%--<th>相册</th>--%>
                        <th>作者</th>
                        <th>简介</th>
                        <th>访问量</th>
                        <th>是否推荐</th>
                        <th>是否显示</th>
                        <th>添加时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand" >
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("nav_name") %></td>
                                <td><%#Eval("title") %></td>
                                <td style="display:none;"><asp:Label ID="lbl_image" runat="server" Text='<%#Eval("article_image") %>'></asp:Label></td>
                                <td><%#Eval("author") %></td>
                                <td><%#Eval("descr") %></td>
                                <td><%#Eval("visited") %></td>
                                <td><%#Eval("istuijian").ToString()=="1"?"是":"否" %></td>
                                <td><%#Eval("isshow").ToString()=="1"?"是":"否" %></td>
                                <td><%#Eval("addtime") %></td>
                                <td class="td-manage">
<%--                                    <asp:LinkButton ID="lbtn_open" runat="server" Text="启用" CommandName="lbtn_open" CommandArgument='<%#Eval("id") %>' Style="color: #fff" CssClass="btn btn-success radius"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_close" runat="server" Text="禁止" CommandName="lbtn_close" CommandArgument='<%#Eval("id") %>' Style="color: #fff" CssClass="btn btn-primary radius"></asp:LinkButton>--%>
                                    <a href='albumadd.aspx?id=<%#Eval("id") %>' class="btn btn-warning radius">详细</a>
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
</script>
        </form>
</body>
</html>
