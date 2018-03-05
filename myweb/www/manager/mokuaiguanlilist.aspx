<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="mokuaiguanlilist.aspx.cs" Inherits="manager_mokuaiguanlilist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<script type="text/javascript" src="lib/PIE_IE678.js"></script>
<![endif]-->
    <link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.7/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="lib/icheck/icheck.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>模块管理</title>
</head>
<body>
    <form id="forma" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 模块管理 <span class="c-gray en">&gt;</span> 模块列表 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c pd-5 bg-1 bk-gray mt-20">
            所属模块：
            <asp:DropDownList ID="ddlmoduleName" runat="server" CssClass="select" style="width:100px;height:30px;"></asp:DropDownList>
            模块名称：
            <input type="text" class="input-text" style="width: 250px" placeholder="输入模块名称" id="qitatiaojian" runat="server" name=""/>
            <input type="text" class="input-text" style="width:250px" placeholder="输入模块地址" id="mokuaidizhi" runat="server" />
            <input type="text" class="input-text" style="width:90px" placeholder="排序数值" onkeyup='this.value=this.value.replace(/\D/gi,"")' id="paixu" runat="server" />
            <input type="text" class="input-text" style="width:90px" placeholder="图标"  id="tubiao" runat="server" />
            <asp:HiddenField ID="hidid" runat="server" />
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"><i class="Hui-iconfont">&#xe665;</i> 搜模块</asp:LinkButton>
            <asp:LinkButton ID="add_module" runat="server" CssClass="btn btn-primary radius" OnClick="add_modules" OnClientClick="return yanzheng();"><i class="Hui-iconfont">&#xe600;</i> 保存</asp:LinkButton>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20" style="display:none;"><span class="l"><a href="javascript:;" onclick="admin_role_edit('添加模块','mokuaiguanli.aspx','510')" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加模块</a></span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th>模块名称</th>
                        <th>所属模块</th>
                        <th>模块地址</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("mmoduleName") %></td>
                                <td><%#Eval("mmmoduleName") %></td>
                                <td><%#Eval("modelHref") %></td>
                                <td class="td-manage">
                                    <asp:LinkButton ID="lbtshan"  runat="server" Text="删除"  CommandName="lbtshan" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('确实要删除吗？')" CssClass="btn btn-danger radius" ></asp:LinkButton> 
                                    <asp:LinkButton ID="lbtn_edit"  runat="server" Text="修改"  CommandName="lbtn_edit" CommandArgument='<%#Eval("id") %>' CssClass="btn btn-warning radius" ></asp:LinkButton> 
                                    <a style="display:none;" onclick="admin_role_edit('详细','mokuaiguanli.aspx?manId=<%#Eval("id") %>','1')" class="btn btn-warning radius">详细</a>
                               </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
          <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
        <script type="text/javascript" src="lib/layer/1.9.3/layer.js"></script>
        <script type="text/javascript" src="lib/My97DatePicker/WdatePicker.js"></script>
        <script type="text/javascript" src="lib/datatables/1.10.0/jquery.dataTables.min.js"></script>
        <script type="text/javascript" src="js/H-ui.js"></script>
        <script type="text/javascript" src="js/H-ui.admin.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.table-sort').dataTable({
                "bStateSave": false,//状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0] }// 制定列不参与排序
                ]
            });

        });
        function admin_role_edit(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        function yanzheng() {
            if ($("#qitatiaojian").val()=="") {
                alert("模块名称不能为空");
                $("#qitatiaojian").focus();
                return false;
            }
            var paixu = $("#paixu").val();
            if (paixu.length == 0) {
                alert("模块序号不能为空");
                $("#paixu").focus();
                return false;
            }
            var reg = new RegExp("^[0-9]*$");
            if (!reg.test(paixu)) {
                alert("请输入数字");
                $("#paixu").focus();
                return false;
            }
        }
</script>
        </form>
</body>
</html>
