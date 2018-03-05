<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gonghuochangshang_list.aspx.cs" Inherits="gonghuochangshang_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
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
<title>供货厂商列表</title>
</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 搜索条件管理 <span class="c-gray en">&gt;</span> 供货厂商列表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="pd-20">
<form id="Form1" runat="server">
  <div class="text-c"> 供货厂商：
    <input type="text" class="input-text" style="width:250px" placeholder="输入供货厂商" id="inshiyongdanwei" runat="server" />
    <asp:LinkButton ID="lbtn_search" runat="server" CssClass="btn btn-success radius" 
          Text="搜索" onclick="lbtn_search_Click"></asp:LinkButton>
    <asp:LinkButton ID="lbtn_save" runat="server" CssClass="btn btn-primary radius" 
          Text="保存" onclick="lbtn_save_Click"></asp:LinkButton>
  </div>
  <div class="cl pd-5 bg-1 bk-gray mt-20">
  <span class="l">
  <asp:LinkButton ID="lbtn_dels" runat="server" CssClass="btn btn-danger radius" 
          Text="删除" OnClientClick="return check()" onclick="lbtn_dels_Click"></asp:LinkButton>
  </span>
    <%--<span class="l"><a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"> 删除</a>
    <a href="javascript:;" onclick="user_add('550','','添加用户','user-add.html')" class="btn btn-primary radius"><i class="icon-plus"></i> 添加用户</a></span>
    <span class="r">共有数据：<strong>88</strong> 条</span>--%>
  </div>
  <div class="mt-20">
  <table class="table table-border table-bordered table-hover table-bg table-sort">
    <thead>
      <tr class="text-c">
        <th width="25"><input type="checkbox" name="" value=""></th>
        <th width="80">序号</th>
        <th>名称</th>
        <th width="130">添加时间</th>
        
      </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpt_shiyongdanwei" runat="server">
    <ItemTemplate>
     <tr class="text-c">
        <td><asp:CheckBox runat="server" ID="ckb" ToolTip='<%#Eval("gonghuochangshang_id") %>' /></td>
        <td width="80"><%#Container.ItemIndex+1 %></td>
        <td><%#Eval("mingcheng")%></td>
        <td><%#Eval("addtime") %></td>
        </tr>
    </ItemTemplate>
    </asp:Repeater>
     
    </tbody>
  </table>
  </div>
  <div id="pageNav" class="pageNav"></div>
  </form>
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
<script type="text/javascript">
    window.onload = (function () {
        // optional set
        pageNav.pre = "&lt;上一页";
        pageNav.next = "下一页&gt;";
        // p,当前页码,pn,总页面
        pageNav.fn = function (p, pn) { $("#pageinfo").text("当前页:" + p + " 总页: " + pn); };
        //重写分页状态,跳到第三页,总页33页
        pageNav.go(1, 13);
    });
    //    $('.table-sort').dataTable({
    //        "lengthMenu": false, //显示数量选择 
    //        "bFilter": false, //过滤功能
    //        "bPaginate": false, //翻页信息
    //        "bInfo": false, //数量信息
    //        "aaSorting": [[1, "desc"]], //默认第几个排序
    //        "bStateSave": true, //状态保存
    //        "aoColumnDefs": [
    //        //{"bVisible": false, "aTargets": [ 3 ]} //控制列的隐藏显示
    //	  {"orderable": false, "aTargets": [0, 8, 9]}// 制定列不参与排序
    //	]
    //    });
    $('.table-sort').dataTable({
        "bStateSave": false, //状态保存
        "aoColumnDefs": [
						  { "orderable": false, "aTargets": [0]}// 制定列不参与排序
						]
    });


    function check() {
        if ($(":checkbox:checked").length == 0) {
            alert("请选择需要操作的数据");
            return false;
        }
        return confirm('确定操作吗？');
    }
</script>
</body>
</html>