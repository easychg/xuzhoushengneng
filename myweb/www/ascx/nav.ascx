<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="ascx_nav" %>

<%--<div  class="navigation">
          <ul class="menu">
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                    <li><a href='<%#Eval("muban_url").ToString()+"?id="+Eval("id").ToString()%>'><%#Eval("nav_name") %></a>
                        <ul>
                            <asp:Label ID="lbl_navid" runat="server" Text='<%#Eval("id") %>' Visible="false"></asp:Label>
                            <asp:Repeater ID="rptlistb" runat="server">
                                <ItemTemplate>
                                    <li><a href='<%#Eval("muban_url").ToString()+"?id="+Eval("id").ToString()%>'><%#Eval("nav_name") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li> 
                </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>--%>
<div  class="navigation">
          <ul class="menu">
            <li><a href="index.aspx">网站首页</a></li>
            <li><a href="#">关于我</a>
              <ul>
                <li><a href="about.aspx">个人简介</a></li>
                <li><a href="listpic.aspx">个人相册</a></li>
              </ul>
            </li>
            <li><a href="#">我的日记</a>
              <ul>
                <li><a href="articlelist.aspx?nid=7">个人日记</a></li>
                <li><a href="articlelist.aspx?nid=8">学习笔记</a></li>
              </ul>
            </li>
            <li><a href="articlelist.aspx?nid=11">技术文章</a> </li>
            <li><a href="#">给我留言</a> </li>
          </ul>
        </div>