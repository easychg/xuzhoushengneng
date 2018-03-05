<%@ Page Language="C#" AutoEventWireup="true" CodeFile="articlelist.aspx.cs" Inherits="articlelist" %>
<%@ Register src="ascx/nav.ascx" tagname="nav" tagprefix="uc1" %>
<!doctype html>
<html>
<head>
<meta charset="gb2312">
<title>个人博客模板古典系列之——江南墨卷</title>
<meta name="keywords" content="个人博客模板古典系列之——江南墨卷" />
<meta name="description" content="个人博客模板古典系列之——江南墨卷" />
<link href="css/base.css" rel="stylesheet">
<link href="css/main.css" rel="stylesheet">
<!--[if lt IE 9]>
<script src="js/modernizr.js"></script>
<![endif]-->
<script type="text/javascript" src="js/jquery.js"></script>
</head>
<body>
<div id="wrapper">
<header>
  <div class="headtop"></div>
  <div class="contenttop">
    <div class="logo f_l"><%=getTile()%></div>
    <div class="search f_r">
      <form action="/e/search/index.php" method="post" name="searchform" id="searchform">
        <input name="keyboard" id="keyboard" class="input_text" value="请输入关键字" style="color: rgb(153, 153, 153);" onfocus="if(value=='请输入关键字'){this.style.color='#000';value=''}" onblur="if(value==''){this.style.color='#999';value='请输入关键字'}" type="text">
        <input name="show" value="title" type="hidden">
        <input name="tempid" value="1" type="hidden">
        <input name="tbname" value="news" type="hidden">
        <input name="Submit" class="input_submit" value="搜索" type="submit">
      </form>
    </div>
    <div class="blank"></div>
    <nav>
      <%--导航栏--%>
        <uc1:nav ID="nav1" runat="server" />
    </nav>
    <SCRIPT type=text/javascript>
        // Navigation Menu
        $(function () {
            $(".menu ul").css({ display: "none" }); // Opera Fix
            $(".menu li").hover(function () {
                $(this).find('ul:first').css({ visibility: "visible", display: "none" }).slideDown("normal");
            }, function () {
                $(this).find('ul:first').css({ visibility: "hidden" });
            });
        });
</SCRIPT> 
  </div>
</header>
<div class="container">
  <div class="con_content">
    <div class="about_box">
      <h2 class="nh1"><span>您现在的位置是：<%=getLocation()%></h2>
      <div class="dtxw box">
        <asp:Repeater ID="rpt_main" runat="server">
            <ItemTemplate>
                 <li>
                  <div class="dttext f_l">
                    <ul>
                      <h2><a href='article.aspx?id=<%#Eval("id") %>'><%#Eval("title") %></a></h2>
                      <p><%#Eval("descr") %>.</p>
                      <span><%#Eval("addtime") %></span>
                    </ul>
                  </div>
                  <div class="xwpic f_r"><a href='article.aspx?id=<%#Eval("id") %>'><img src='<%#Eval("article_image").ToString().Split('#')[0] %>'></a></div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
      </div>
      <div class="pagelist"><%=getPage()%></div>
    </div>
  </div>
  <div class="blank"></div>
  <!-- container代码 结束 -->
  
  <footer>
    <div class="footer">
      <div class="f_l">
        <p>All Rights Reserved 版权所有：<a href="http://www.yangqq.com">杨青个人博客</a> 备案号：蜀ICP备00000000号</p>
      </div>
      <div class="f_r textr">
        <p>Design by DanceSmile</p>
      </div>
    </div>
  </footer>
</div>
</body>
</html>