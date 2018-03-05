<%@ Page Language="C#" AutoEventWireup="true" CodeFile="articleadd.aspx.cs" Inherits="manager_articleadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link href="css/post-ui7_v20150309143458.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="kindeditor/plugins/code/prettify.css" />
    <!--[if IE 6]>
<script type="text/javascript" src="http://lib.h-ui.net/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>添加文章</title>
</head>
<body>
    <div class="pd-20">
        <form runat="server" id="forma">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>标题：</label>
                <div class="formControls col-8">
                    <asp:TextBox ID="txttitle" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2" ></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>作者：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtauthor"  runat="server" multiple="multiple" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>缩略图：</label>
                <div class="formControls col-8">
                    <asp:TextBox ID="txtname" runat="server" Width="1000px" Text="" Style="display:none;"></asp:TextBox>
                    <input type="hidden" id="status" value="true" />
                    <p>多张：<input type="file" multiple="multiple" id="multiple" /></p>
                    <div id="box" runat="server"></div>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>简介：</label>
                <div class="formControls col-8">
                  <asp:TextBox ID="txtdescr" runat="server" CssClass="input-text"></asp:TextBox>
                </div>
                <div class="col-2"><span id="sppwd" style="color:red;"></span></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">是否显示：</label>
                <div class="formControls col-8">
                    <span class="select-box" style="Width:100px;">
                        <asp:DropDownList ID="ddl_show" runat="server" CssClass="select">
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </div>
                <div class="col-2"><span id="Span1" style="color:red;"></span></div>
            </div>
            <div class="row cl"  style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right">是否推荐：</label>
                <div class="formControls col-8">
                    <span class="select-box" style="Width:100px;">
                        <asp:DropDownList ID="ddl_tuijian" runat="server" CssClass="select">
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </div>
                <div class="col-2"><span id="Span2" style="color:red;"></span></div>
            </div>
            <div class="row cl" style="margin-top:5px">
                <label class="form-label col-2" style="text-align:right"><span class="c-red">*</span>内容：</label>
                <div class="formControls col-8">
                    <textarea name="" style="width:100%;height:500px" id="txtbeizhu" runat="server" class="textarea" placeholder="说点什么..."></textarea>
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

    <script src="js/tools.js" type="text/javascript"></script>
    <script src="js/pictureHandle.js" type="text/javascript"></script>

    <%--<script type="text/javascript" src="js/html5ImgCompress.min.js"></script>
       <script type="text/javascript">
           var j = 1;
           $(function () {
               // 多张
               $('#multiple').on('change', function (e) {
                   var 
              i = 0,
              files = e.target.files,
              len = files.length,
              notSupport = false;

                   // 循环多张图片，需要for循环和notSupport变量支持（检测）
                   for (; i < len; i++) {
                       if (!notSupport) {
                           (function (i) {
                               new html5ImgCompress(files[i], {
                                   before: function (file) {
                                       // console.log('多张: ' + i + ' 压缩前...');
                                   },
                                   done: function (file, base64) { // 这里是异步回调，done中i的顺序不能保证
                                       // console.log('多张: ' + i + ' 压缩成功...');
                                       // insertImg(file, j);

                                       insertImg(base64, j++);
                                   },
                                   fail: function (file) {
                                       console.log('多张: ' + i + ' 压缩失败...');
                                   },
                                   complete: function (file) {
                                       console.log('多张: ' + i + ' 压缩完成...');
                                   },
                                   notSupport: function (file) {
                                       notSupport = true;
                                       alert('浏览器不支持！');
                                   }
                               });
                           })(i);
                       }
                   }
               })
           });

           // html中插入图片
           function insertImg(file, j) {
               var 
          img = new Image(),
          title, src, size, base;

               if (typeof file === 'object') {
                   title = '前';
                   size = file.size;
                   src = URL.createObjectURL(file);
                   base = 1024;
               } else {
                   title = '后';
                   size = file.length;
                   src = file;
                   base = 3333;
               }

               if (!$('.box_' + j).length) {
                   // $('#box').prepend('<div class="box_' + j + '" />'); // 逆序，方便demo多次上传预览
               }

               img.onload = function () {
                   // $('.box_' + j).prepend(img).prepend('<p>图片' + j + '，处理' + title + '</p><p>质量约：<span>' + (size / base).toFixed(2) + 'KB</span>' + '&nbsp;&nbsp;&nbsp;&nbsp;尺寸：<span>' + this.width + '*' + this.height + '</span></p>');
                   var aaa = "<div id='" + j + "a'  class='imgbox hasboth'  style='background-color:#fff'>"
                   var aa = " <div class='w_upload' >    <a class='item_close' onclick=deletepic('" + j + "')>删除</a><span class='item_box'>";
                   var bb = "<img id='" + j + "b' src='" + src + "'  style='background:url(../images/12282498_172718388183_2.gif) no-repeat center;'/></span>";
                   var cc = " ";
                   var dd = "</div></div>";
                   document.getElementById("box").innerHTML += aaa + aa + bb + cc + dd;
                   document.getElementById("txtname").value += src + "#";
                   var strs = document.getElementById("txtname").value;

                   var arr = new Array();
                   arr = strs.split("#");


                   if (arr.length >= 9) {
                       //document.getElementById("uploadifyUploader").style.display = "none";
                       //document.getElementById("divpics").style.display = "";
                   }
               };

               img.src = src;

               file = null; // 处理完后记得清缓存
           };
           function deletepic(aa) {

               var str = document.getElementById("txtname").value;

               var pic = document.getElementById(aa + "b").src;
               str = str.replace(pic + "#", '');

               document.getElementById("txtname").value = str;
               var strs = document.getElementById("txtname").value;


               var oldnode = document.getElementById(aa + "a");
               document.getElementById(aa + "a").parentNode.removeChild(oldnode)

           }
  
</script>--%>
    
    <script charset="utf-8" src="kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" src="kindeditor/plugins/code/prettify.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#txtbeizhu', {
                cssPath: 'kindeditor/plugins/code/prettify.css',
                uploadJson: 'kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: 'kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true,

                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=forma]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=forma]')[0].submit();
                    });
                },
                afterBlur: function () { this.sync(); }
            });
        });
    </script>
    <script type="text/javascript">
        function yanzheng() {
            if ($("#txttitle").val() == "") {
                alert("标题不能为空");
                return false;
            }
            if ($("#txtauthor").val() == "") {
                alert("作者不能为空");
                return false;
            }
            if ($("#txtdescr").val() == "") {
                alert("简介不能为空");
                return false;
            }
            if ($("#txtbeizhu").val() == "") {
                alert("内容不能为空");
                return false;
            }
        }
        
</script>
</body>
</html>
