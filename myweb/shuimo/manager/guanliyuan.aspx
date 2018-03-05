<%@ Page Language="C#" validateRequest="false"  AutoEventWireup="true" CodeFile="guanliyuan.aspx.cs" Inherits="manager_guanliyuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<script type="text/javascript" src="lib/PIE_IE678.js"></script>
<![endif]-->
    <link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="lib/icheck/icheck.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.1/iconfont.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>添加管理员</title>
</head>
<body>
    <div class="pd-20">
        <form runat="server" id="forma">
            <div class="row cl">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>管理员组：</label>
                <div class="formControls col-3">
                    <span class="select-box" style="width: 150px;">
                     <asp:DropDownList ID="ddljiaose" runat="server" CssClass="select">
                     </asp:DropDownList>
                    </span>
                </div>
                <div class="col-7" ><asp:Label ID="msg" runat="server" style="color:red;"></asp:Label></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>管理员名称：</label>
                <div class="formControls col-8">
                    <asp:TextBox ID="txtname" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2" ></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>密码：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtpassok" TextMode="Password" onblur="isSameTextBox(this,txtpassok2)" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>确认密码：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtpassok2" TextMode="Password" onblur="isSameTextBox(this,txtpassok)" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"><span id="sppwd" style="color:red;"></span></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>状态：</label>
                <div class="formControls col-8">
                    <span class="select-box" style="width: 150px;">
                     <asp:DropDownList ID="ddlState" runat="server" CssClass="select">
                         <asp:ListItem Value="启用" Text="启用"></asp:ListItem>
                         <asp:ListItem Value="禁止"  Text="禁止"></asp:ListItem>
                     </asp:DropDownList>
                        </span>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">管理员描述：</label>
                <div class="formControls col-8">
                    <textarea name="" style="width:100%;height:200px" id="txtbeizhu" runat="server" class="textarea" placeholder="说点什么...100个字符以内"></textarea>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <div class="col-9 col-offset-2">
             <asp:Button ID="btnok" runat="server" CssClass="btn btn-primary radius" Text="&nbsp;&nbsp;提交&nbsp;&nbsp;" OnClick="btnok_Click" OnClientClick="return yanzheng()" />
                </div>
            </div>
        </form>
    </div>
    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="lib/icheck/jquery.icheck.min.js"></script>
    <script type="text/javascript" src="lib/Validform/5.3.2/Validform.min.js"></script>
    <script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
    <script type="text/javascript" src="js/H-ui.js"></script>
    <script type="text/javascript" src="js/H-ui.admin.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.skin-minimal input').iCheck({
                checkboxClass: 'icheckbox-blue',
                radioClass: 'iradio-blue',
                increaseArea: '20%'
            });

            $("#form-admin-add").Validform({
                tiptype: 2,
                callback: function (form) {
                    form[0].submit();
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.$('.btn-refresh').click();
                    parent.layer.close(index);
                }
            });
        });
        function yanzheng() {
            var name = $("#ddljiaose").val();
            if (name=="0") {
                alert("请选择管理员组");
                $("#ddljiaose").focus();
                return false;
            }
            var name = $("#txtname").val();
            if (name.length == 0) {
                alert("管理员名称不能为空");
                $("#txtname").focus();
                return false;
            }
            if ($("#txtpassok").val() != $("#txtpassok2").val()) {
                alert("两次输入密码不一致");
                return false;
            }
        }
        var shouldAlert = true;
        function isSameTextBox(_this, target) {
            //alert(target)
            //alert(target.value)
            if (target.value == "") {
                return;
            }
            if (shouldAlert) {
                if (_this.value != target.value) {
                    $("#sppwd").text("确认密码与输入密码不一致");
                    //shouldAlert = false;
                } else {
                    $("#sppwd").text("");
                }
            }

        }
</script>
</body>
</html>
