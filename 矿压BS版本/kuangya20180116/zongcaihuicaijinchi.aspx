<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zongcaihuicaijinchi.aspx.cs" Inherits="zongcaihuicaijinchi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
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
    <title>综采数据分析</title>
</head>
<body>
    <form id="forma" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 综采压力管理 <span class="c-gray en">&gt;</span> 综采回采进尺 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
        
        <asp:UpdatePanel runat ="server" ID ="UpdatePanel1" style="display:inline-block" >  
                            <ContentTemplate >
                            
                选择测区：<asp:DropDownList ID="ddl_cequ" runat="server" OnSelectedIndexChanged ="ddl_cequ_changed" AutoPostBack ="true" CssClass="select" Height="30" Width="160"></asp:DropDownList>
            选择工作面：<asp:DropDownList ID="ddl_gongzuomian" runat="server" CssClass="select" Height="30" Width="160">
            <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            选择日期：<input type="text" onfocus="WdatePicker()" id="datemin" runat="server" class="input-text Wdate" style="width: 120px;"/>
            机头进尺(M)：<asp:TextBox ID="txtjitoujinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"></asp:TextBox>
            机尾进尺(M)：<asp:TextBox ID="txtjiweijinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')"></asp:TextBox>
            中部进尺(M)：<asp:TextBox ID="txtzhongbujinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" ReadOnly="true"></asp:TextBox>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="search" runat="server" CssClass="btn btn-success radius" OnClick="search_Click"> <i class="Hui-iconfont">&#xe665;</i>查询</asp:LinkButton>
            <asp:LinkButton ID="lbtn_add" runat="server" CssClass="btn btn-success radius" OnClick="lbtn_add_Click" OnClientClick="return check();"> <i class="Hui-iconfont">&#xe604;</i>保存</asp:LinkButton>
            <asp:LinkButton ID="lbtn_export" runat="server" CssClass="btn btn-success radius" OnClick="lbtn_export_Click"> <i class="Hui-iconfont">&#xe640;</i>导出</asp:LinkButton>
            <asp:Label ID="lblstate" runat="server" Text="i" Visible="false"></asp:Label>
        </div>
        <div class="cl pd-5 bg-1 bk-gray mt-20" style="text-align:center;"><span>警告！此数据不能随便输入，请注意日期和数据的准确性，只能填入一次，否则任务无法进行。</span></div>
        <div class="cl pd-5 bg-1 bk-gray mt-20"><span class="l">
        机头总进尺(M)：<asp:TextBox ID="txtjitouzongjinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" ReadOnly="true"></asp:TextBox>
        中部总进尺(M)：<asp:TextBox ID="txtzhongbuzongjinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" ReadOnly="true"></asp:TextBox>
        机尾总进尺(M)：<asp:TextBox ID="txtjiweizhongjinchi" runat="server" CssClass="input-text" Width="120" onkeyup="if(isNaN(value))execCommand('undo');cal()" onafterpaste="if(isNaN(value))execCommand('undo')" ReadOnly="true"></asp:TextBox>
        </span></div>
        <div class="mt-20">
            <table class="table table-border table-bordered table-hover table-bg table-sort">
                <thead>
                    <tr class="text-c">
                        <th>序号</th>
                        <th>所属工作面</th>
                        <th>日期</th>
                        <th>机头进尺</th>
                        <th>中部进尺</th>
                        <th>机尾进尺</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptlist" runat="server" OnItemCommand="rptlist_ItemCommand">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td><%#Container.ItemIndex+1 %></td>
                                <td><%#Eval("facename")%></td>
                                <td><%#Eval("datetime")%></td>
                                <td><%#Eval("hcjcnose")%></td>
                                <td><%#Eval("hcjcmid")%></td>
                                <td><%#Eval("hcjctail")%></td>
                                <td>
                                    <asp:Label ID="lblareaname" runat="server" Text='<%#Eval("areaname") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbldatetime" runat="server" Text='<%#Eval("datetime") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lbtn_xiugai" runat="server"  CommandName="lbtn_xiugai" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-warning radius"><i class="Hui-iconfont">&#xe6df;</i>修改</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_delete" runat="server"  CommandName="lbtn_delete" CommandArgument='<%#Eval("facename") %>' Style="color: #fff" CssClass="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i>删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
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
    <script src="js/md5.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('.table-sort').dataTable({
                "bStateSave": false, //状态保存
                "aoColumnDefs": [
                  { "orderable": false, "aTargets": [0]}// 制定列不参与排序
                ]
            });

        });
        function admin_role_edit(title, url, id, w, h) {
            layer_show(title, url, w, h);
        }
        function check() {
            
            var cequ = $("#ddl_cequ").val();
            if (cequ == "0") {
                alert("请选择测区");
                return false;
            }
            var gongzuomian = $("#ddl_gongzuomian").val();
            if (gongzuomian == "0") {
                alert("请选择工作面");
                return false;
            }
            var riqi = $("#datemin").val();
            if (riqi == "") {
                alert("请选择日期");
                return false;
            }
            var txtjitoujinchi = $("#txtjitoujinchi").val();
            if (txtjitoujinchi == "") {
                alert("请输入机头进尺");
                return false;
            }
            var txtjiweijinchi = $("#txtjiweijinchi").val();
            if (txtjiweijinchi == "") {
                alert("请输入机尾进尺");
                return false;
            }
            var jimi = prompt("请输入密码");
            if (hex_md5(jimi) != "5172591894f1bb649bdb35f47679ccb5") {
                alert("密码错误");
                return false;
            }
            
        }
        $(function () {
            $("#txtjitoujinchi").on('keyup', function () {
                cal2();
            });
            $("#txtjiweijinchi").on('keyup', function () {
                cal2();
            });
        })
        function cal2() {
            var txtjitoujinchi = $("#txtjitoujinchi").val();
            var txtjiweijinchi = $("#txtjiweijinchi").val();
            var $zhongbu = $("#txtzhongbujinchi");
            if (txtjitoujinchi != "" && txtjiweijinchi != "") {
                var jitou = parseFloat(txtjitoujinchi);
                var jiwei = parseFloat(txtjiweijinchi);
                $zhongbu.val((jitou + jiwei) / 2);
            }
        }
</script>
        </form>
</body>
</html>