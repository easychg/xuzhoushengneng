<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="manager_index" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <link rel="Bookmark" href="/favicon.ico">
    <link rel="Shortcut Icon" href="/favicon.ico" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5shiv.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<![endif]-->
    <link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.1/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title></title>
    <meta name="keywords" content="" />
    <meta name="description" content="" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar-wrapper">
            <div class="navbar navbar-fixed-top">
                <div class="container-fluid cl">
                    <a class="logo navbar-logo f-l mr-10 hidden-xs" href="#"></a> <a class="logo navbar-logo-m f-l mr-10 visible-xs" href="/aboutHui.shtml">H-ui</a>
                    <span class="logo navbar-slogan f-l mr-10 hidden-xs"></span>
                    <a aria-hidden="false" class="nav-toggle Hui-iconfont visible-xs" href="javascript:;">&#xe667;</a>
                    <nav class="nav navbar-nav" style="display: none">
                        <ul class="cl">
                            <li class="dropDown dropDown_hover"><a href="javascript:;" class="dropDown_A"><i class="Hui-iconfont">&#xe600;</i> 新增 <i class="Hui-iconfont">&#xe6d5;</i></a>
                                <ul class="dropDown-menu menu radius box-shadow">
                                    <li><a href="javascript:;" onclick="article_add('添加资讯','article-add.html')"><i class="Hui-iconfont">&#xe616;</i> 资讯</a></li>
                                    <li><a href="javascript:;" onclick="picture_add('添加资讯','picture-add.html')"><i class="Hui-iconfont">&#xe613;</i> 图片</a></li>
                                    <li><a href="javascript:;" onclick="product_add('添加资讯','product-add.html')"><i class="Hui-iconfont">&#xe620;</i> 产品</a></li>
                                    <li><a href="javascript:;" onclick="member_add('添加用户','member-add.html','','510')"><i class="Hui-iconfont">&#xe60d;</i> 用户</a></li>
                                </ul>
                            </li>
                        </ul>
                    </nav>
                    <nav id="Hui-userbar" class="nav navbar-nav navbar-userbar hidden-xs">
                        <ul class="cl">
                            <li><asp:Label ID="lbl_title" runat="server"></asp:Label></li>
                            <li class="dropDown dropDown_hover">
                                <a href="#" class="dropDown_A">
                                    <asp:Label ID="lbl_name" runat="server"></asp:Label>
                                    <i class="Hui-iconfont">&#xe6d5;</i></a>
                                <ul class="dropDown-menu menu radius box-shadow">

                                    <li>
                                        <asp:LinkButton ID="lbtn_logout" runat="server" Text="退出"
                                            OnClick="lbtn_logout_Click"></asp:LinkButton></li>
                                    <li>
                                        <a   onclick="admin_role_edit('修改密码','user_center.aspx','1')">修改密码</a>
                                    </li>
                                </ul>
                            </li>

                            <li id="Hui-skin" class="dropDown right dropDown_hover"><a href="javascript:;" class="dropDown_A" title="换肤"><i class="Hui-iconfont" style="font-size: 18px">&#xe62a;</i></a>
                                <ul class="dropDown-menu menu radius box-shadow">
                                    <li><a href="javascript:;" data-val="default" title="默认（黑色）">默认（黑色）</a></li>
                                    <li><a href="javascript:;" data-val="blue" title="蓝色">蓝色</a></li>
                                    <li><a href="javascript:;" data-val="green" title="绿色">绿色</a></li>
                                    <li><a href="javascript:;" data-val="red" title="红色">红色</a></li>
                                    <li><a href="javascript:;" data-val="yellow" title="黄色">黄色</a></li>
                                    <li><a href="javascript:;" data-val="orange" title="橙色">橙色</a></li>
                                </ul>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </header>
        <aside class="Hui-aside">
            <div class="menu_dropdown bk_2">
                <dl id="Dl3">
                    <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                        <ItemTemplate>
                            <dt><i class="Hui-iconfont"><%#Eval("tubiao") %></i> <%#Eval("moduleName") %><i class="Hui-iconfont menu_dropdown-arrow">&#xe6d5;</i></dt>
                            <dd>
                                <ul>
                                    <asp:Label ID="rptId" runat="server" Text='<%#Eval("id") %>' style="display:none;"></asp:Label>
                                    <asp:Repeater ID="rpta" runat="server">
                                        <ItemTemplate>
                                            <li><a data-href='<%#Eval("modelHref") %>' data-title="<%#Eval("moduleName") %>" href="javascript:void(0)"><i class="Hui-iconfont"><%#Eval("tubiao") %></i><%#Eval("moduleName") %></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </dd>
                        </ItemTemplate>
                    </asp:Repeater>
                </dl>
            </div>
        </aside>
        <div class="dislpayArrow hidden-xs"><a class="pngfix" href="javascript:void(0);" onclick="displaynavbar(this)"></a></div>
        <section class="Hui-article-box">
            <div id="Hui-tabNav" class="Hui-tabNav hidden-xs">
                <div class="Hui-tabNav-wp">
                    <ul id="min_title_list" class="acrossTab cl">
                        <li class="active">
                            <span title="我的桌面" data-href="">我的桌面</span>
                            <em></em></li>
                    </ul>
                </div>
                <div class="Hui-tabNav-more btn-group"><a id="js-tabNav-prev" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d4;</i></a><a id="js-tabNav-next" class="btn radius btn-default size-S" href="javascript:;"><i class="Hui-iconfont">&#xe6d7;</i></a></div>
            </div>
            <div id="iframe_box" class="Hui-article">
                <div class="show_iframe">
                    <div style="display: none" class="loading"></div>
                    <iframe scrolling="yes" frameborder="0" src="welcome.aspx"></iframe>
                </div>
            </div>
        </section>

        <div class="contextMenu" id="Huiadminmenu">
            <ul>
                <li id="closethis">关闭当前 </li>
                <li id="closeall">关闭全部 </li>
            </ul>
        </div>
        <!--_footer 作为公共模版分离出去-->
        <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
        <script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
        <script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script>
        <script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script>
        <!--/_footer 作为公共模版分离出去-->
        <script type="text/javascript">
            function admin_role_edit(title, url, id, w, h) {
                layer_show(title, url, w, h);
            }
        </script>
    </form>
</body>
</html>
