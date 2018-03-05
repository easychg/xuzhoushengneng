<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Register src="ascx/nav.ascx" tagname="nav" tagprefix="uc1" %>
<!doctype html>
<html>
<head>
<meta charset="gb2312">
<title>个人博客模板古典系列之——江南墨卷</title>
<meta name="keywords" content="个人博客模板古典系列之——江南墨卷" />
<meta name="description" content="个人博客模板古典系列之——江南墨卷" />
<link href="css/base.css" rel="stylesheet">
<link href="css/index.css" rel="stylesheet">
<!--[if lt IE 9]>
<script src="js/modernizr.js"></script>
<![endif]-->
<script type="text/javascript" src="js/jquery.js"></script>
</head>
<body>
<div class="topnav">
<a href="http://www.yangqq.com/download/div/2017-07-16/785.html" target="_blank">个人博客模板古典系列之——江南墨卷</a>——作品来自<a href="http://www.yangqq.com" target="_blank">杨青个人博客网站</a>
</div>
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
  <div class="jztop"></div>
  <div class="container">
    <div class="bloglist f_l">
        <asp:Repeater ID="rpt_main" runat="server">
            <ItemTemplate>
                <h3><a href='<%#Eval("muban_url") %>?id=<%#Eval("id") %>'><%#Eval("title") %></a></h3>
                  <figure><img src='<%#Eval("article_image").ToString().Split('#')[0] %>' alt="【<%#Eval("nav_name") %>】<%#Eval("title") %>"></figure>
                  <ul>
                    <p> <%#Eval("descr")%></p>
                    <a title="【<%#Eval("nav_name") %>】<%#Eval("title") %>" href="<%#Eval("muban_url") %>?id=<%#Eval("id") %>" target="_blank" class="readmore">阅读全文&gt;&gt;</a>
                  </ul>
                  <p class="dateview"><span><%#Eval("addtime") %></span><span>作者：<%#Eval("author") %></span><span>个人博客：[<a href="/jstt/bj/"><%#Eval("nav_name") %></a>]</span></p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="r_box f_r">
      <div class="tit01">
        <h3 class="tit">关注我</h3>
        <div class="gzwm">
          <ul>
            <li><a class="email" href="#" target="_blank">我的电话</a></li>
            <li><a class="qq" href="#mailto:admin@admin.com" target="_blank">我的邮箱</a></li>
            <li><a class="tel" href="#" target="_blank">我的QQ</a></li>
            <li><a class="prize" href="#">个人奖项</a></li>
          </ul>
        </div>
      </div>
      <!--tit01 end-->
      
      <div class="tuwen">
        <h3 class="tit">图文推荐</h3>
        <ul>
            <asp:Repeater ID="rpt_tuijian" runat="server">
                <ItemTemplate>
                    <li><a href="<%#Eval("muban_url") %>?id=<%#Eval("id") %>"><img src="<%#Eval("article_image").ToString().Split('#')[0] %>"><b><%#Eval("title") %></b></a>
                        <p><span class="tulanmu"><a href="#"><%#Eval("nav_name") %></a></span><span class="tutime"><%#Eval("addtime") %></span></p>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
      </div>
      <div class="ph">
        <h3 class="tit">点击排行</h3>
        <ul class="rank">
            <asp:Repeater ID="rpt_dianji" runat="server">
                <ItemTemplate>
                    <li><a href="<%#Eval("muban_url") %>?id=<%#Eval("id") %>" title="【<%#Eval("nav_name") %>】<%#Eval("title") %>" target="_blank">【<%#Eval("nav_name") %>】<%#Eval("title") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
      </div>
      <div class="ad"> <img src="images/03.jpg"> </div>
    </div>
  </div>
  <!-- container代码 结束 -->
  <div class="jzend"></div>
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

