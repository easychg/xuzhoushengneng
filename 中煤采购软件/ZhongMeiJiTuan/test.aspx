<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload ID="FileUpload1" runat="server" />  
        <asp:LinkButton ID="lbtn_daoru" runat="server"  CssClass="btn btn-warning radius" onclick="lbtn_daoru_Click" Text="导入"></asp:LinkButton>
    <asp:TextBox ID="txtb" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
