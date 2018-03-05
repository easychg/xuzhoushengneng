<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        table
        {
            border-collapse:collapse;
            }
        td
        {
            border:1px solid #000;
            text-align:center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="printdiv" runat="server">
    print test
    </div>
    </form>
</body>
<script type="text/javascript">
    window.onload = function () {
        window.print();
    }
</script>
</html>
