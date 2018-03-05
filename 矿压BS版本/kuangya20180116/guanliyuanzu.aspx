<%@ Page Language="C#" validateRequest="false"  AutoEventWireup="true" CodeFile="guanliyuanzu.aspx.cs" Inherits="manager_guanliyuanzu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5shiv.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<![endif]-->
<link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
<link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.8/iconfont.css" />
<link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
<link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
<!--[if IE 6]>
<script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>添加管理员</title>
</head>
<body>
    <div class="pd-20" style="padding-left:100px">
        <form runat="server" id="forma">
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>组名称：</label>
                <div class="formControls col-8">
                    <asp:TextBox ID="txtname" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2" ></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2">组权限值：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtpassok" runat="server" Text="999" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2">排序：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="paixu" runat="server" Text="0" CssClass="input-text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2">组状态：</label>
                <div class="formControls col-8">
                     <asp:DropDownList ID="ddlState" runat="server" CssClass="select">
                         <asp:ListItem Value="启用">启用</asp:ListItem>
                         <asp:ListItem Value="禁止">禁止</asp:ListItem>
                     </asp:DropDownList>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2">组描述：</label>
                <div class="formControls col-8">
                    <textarea name="" style="width:100%;height:200px" id="txtbeizhu" runat="server" class="textarea" placeholder="说点什么...100个字符以内"></textarea>
                
                </div>
                <div class="col-2"></div>
            </div>
            <%--<div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">地区：</label>
                <div class="formControls col-8" id="control">
                    <input type="button" value="点击打开地区..." class="btn btn-success radius" style="font-size:16px;width:160px;line-height:40px;" id="control_btn" />
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl" style="margin-top:5px;display:none;" id="controled">
                <label class="form-label col-2" style="text-align:right"></label>
                <div class="formControls col-8" style="border:1px solid #d2d2d2;">
                    <asp:Repeater ID="rpt_sheng" runat="server" OnItemDataBound="rpt_sheng_ItemDataBound">
                        <ItemTemplate>
                            <dl >
                                <dt style="background-color:#dddddd;line-height:40px;padding-left:15px;padding-right:15px;"><asp:CheckBox ID="ckb" onclick="ckb(this);" runat="server" ToolTip='<%#Eval("diqu_id") %>' /><b><%#Eval("diqu_name") %></b><input type="button" value="展开" class="btn btn-success radius control" style="line-height:40px;width:60px;font-size:16px;margin-right:0px;float:right;" /></dt>
                                <dd style="background-color:#f9f9f9;line-height:30px;padding-left:3em;display:none;">
                                    <asp:Repeater ID="rpta_sheng" runat="server">
                                        <ItemTemplate>
                                            <div style="display:inline-block"><asp:CheckBox ID="ckba" onclick="ckba(this);" runat="server" ToolTip='<%#Eval("diqu_id") %>' /><%#Eval("diqu_name") %></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </dd>
                            </dl>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-2"></div>
            </div>--%>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2"><span class="c-red">*</span>组权限：</label>
                <div class="formControls col-8" style="border:1px solid #d2d2d2;">
                    <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                        <ItemTemplate>
                            <dl >
                                <dt style="background-color:#dddddd;line-height:40px;padding-left:15px;"><asp:CheckBox ID="ckb" onclick="ckb(this);" runat="server" ToolTip='<%#Eval("id") %>' /><b><%#Eval("moduleName") %></b></dt>
                                <dd style="background-color:#f9f9f9;line-height:30px;padding-left:3em;">
                                    <asp:Repeater ID="rpta" runat="server">
                                        <ItemTemplate>
                                            <div style="display:inline-block"><asp:CheckBox ID="ckba" onclick="ckba(this);" runat="server" ToolTip='<%#Eval("id") %>' /><%#Eval("moduleName") %></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </dd>
                            </dl>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl" style="margin-top:20px;">
                <div class="col-9 col-offset-2">
             <asp:Button ID="btnok" runat="server" CssClass="btn btn-primary radius" Text="&nbsp;&nbsp;提交&nbsp;&nbsp;" OnClick="btnok_Click" OnClientClick="return yanzheng()" />
                </div>
            </div>
        </form>
    </div>
        <!--_footer 作为公共模版分离出去-->
<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script> 
<script type="text/javascript" src="lib/layer/2.4/layer.js"></script>
<script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script> 
<script type="text/javascript" src="static/h-ui.admin/js/H-ui.admin.js"></script> <!--/_footer 作为公共模版分离出去-->

<!--请在下方写此页面业务相关的脚本-->
<script type="text/javascript" src="lib/My97DatePicker/4.8/WdatePicker.js"></script> 
<script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script> 
<script type="text/javascript" src="lib/laypage/1.2/laypage.js"></script>
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
            //$("#ckb").click(function () {
            //    if (this.checked) {
            //        $(this).parent().children().prop("checked", true);
            //    } else {
            //        $(this).parent().children().prop("checked", false);
            //    }
            //})
        });
        function yanzheng() {
            var name = $("#txtname").val();
            if (name.length == 0) {
                alert("组名称不能为空");
                $("#txtname").focus();
                return false;
            }
            if ($(":checkbox:checked").length == 0) {
                alert("请选择组权限");
                return false;
            }
        }
        function ckb(_this) {
            if ($(_this).prop("checked")) {
                $(_this).parent().parent().parent().find(":checkbox").prop("checked", true);
            } else {
                $(_this).parent().parent().parent().find(":checkbox").prop("checked", false);
            }
        }
        function ckba(_this) {
            if ($(_this).prop("checked")) {
                $(_this).parent().parent().parent().siblings().find(":checkbox").prop("checked", true);
            } else {
                $(_this).parent().parent().parent().siblings().find(":checkbox").prop("checked", false);
                $(_this).parent().parent().siblings().find(":checkbox").each(function () {
                    if ($(this).prop("checked")) {
                        $(_this).parent().parent().parent().siblings().find(":checkbox").prop("checked", true);
                        return;
                    }
                })
            }
        }
</script>
    <script type="text/javascript">
        $("#control").click(function () {
            $("#controled").toggle(1000, function () {
                if ($("#control_btn").val() == "点击打开地区...") {
                    $("#control_btn").val("点击关闭地区...");
                } else {
                    $("#control_btn").val("点击打开地区...");
                }
            });
        })
        $(".control").click(function () {
            var $control = $(this);
            $control.parent().siblings().toggle(300, function () {
                if ($control.val()=="展开") {
                    $control.val("关闭");
                } else {
                    $control.val("展开");
                }
            });
        })
    </script>
</body>
</html>

