﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="about" %>
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
      <div class="logo f_l">个人博客模板古典系列之——江南墨卷</div>
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
        <h2 class="nh1"><span>您现在的位置是：<a href="/" target="_blank">网站首页</a>>><a href="#" target="_blank">信息浏览</a></span><b>个人简介</b></h2>
        <div class="f_box">
          <p class="a_title">个人简介</p>
          <p class="p_title"></p>
          <!--  <p class="box_p"><span>发布时间：2017-07-07 15:12:42</span><span>作者：唐孝文</span><span>来源：稽查支队</span><span>点击：111056</span></p>--> 
          <!-- 可用于内容模板 --> 
        </div>
        <ul class="about_content" id="content" runat="server">
          
        </ul>
        <!--    <div class="nextinfos">
      <p>上一篇：<a href="/">区中医医院开展志愿服务活动</a></p>
      <p>下一篇：<a href="/">广安区批准“单独两孩”500例</a></p>
    </div>--> 
        <!-- 可用于内容模板 --> 
        
        <!-- container代码 结束 --> 
      </div>
    </div>
    <div class="blank"></div>
  </div>
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