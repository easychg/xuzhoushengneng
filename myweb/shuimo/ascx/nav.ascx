<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="ascx_nav" %>

<div  class="navigation">
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
        </div>