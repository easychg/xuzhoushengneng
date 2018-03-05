<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panel1" runat="server" Width="1600" Height="1000" style="position:absolute;">
            <%--<hr id="hrx" style="color:Red;z-index:999;position:absolute;margin-top:100px;margin-left:50px;width:1200px;"/>
            <hr id="hry" style="color:Red;z-index:999;position:absolute;margin-top:100px;margin-left:50px;width:1px;height:600px;"/>--%>
        </asp:Panel>
        <asp:TextBox ID="txtjson" runat="server" Width="1000" style="display:;" ></asp:TextBox>
        <asp:label ID="txtshow" runat="server" Width="1000" Font-Size="12" ForeColor="Red" style="margin-top:800px;"></asp:label>
    </div>
    </form>
</body>
<script type="text/javascript">
    $(function () {
        var json = JSON.parse($("#txtjson").val());
        $("#panel1").mousemove(function (e) {
            var l = 50;
            var t = 100;
            var w = 1200;
            var h = 600;
            var X = $('#panel1').offset().left;
            var Y = $('#panel1').offset().top;
            
            var minX = l + X;
            var minY = t + Y;
            var maxX = minX + w;
            var maxY = minY + h;

            if (e.pageX <= maxX && e.pageX >= minX && e.pageY <= maxY && e.pageY >= minY) {
                var offsetX = e.pageX - minX;
                $("#txtshow").text(json[offsetX].content);
                $("#hrx").css("margin-top", e.pageY-7 + "px");
                $("#hry").css("margin-left", e.pageX-7 + "px");
            } else {
                $("#hrx").css("margin-top", "100px");
                $("#hry").css("margin-left", "50px");
            }
        });
    })
    
</script>
</html>
