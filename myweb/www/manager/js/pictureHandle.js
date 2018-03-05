var bianhao = 0;
var _width = 100;
var _height = 100;
$(function () {
    var _upFile = document.getElementById("multiple");

    _upFile.addEventListener("change", function () {

        if (_upFile.files.length === 0) {
            alert("请选择图片");
            return;
        }
        document.getElementById("status").value = "false";
        for (var i = 0; i < _upFile.files.length; i++) {
            sleep(1);
            var oFile = _upFile.files[i];
            //if (!rFilter.test(oFile.type)) { alert("You must select a valid image file!"); return; }  

            /*  if(oFile.size>5*1024*1024){  
            message(myCache.par.lang,{cn:"照片上传：文件不能超过5MB!请使用容量更小的照片。",en:"证书上传：文件不能超过100K!"})  
         
            return;  
            }*/
            if (!new RegExp("(jpg|jpeg|png)+", "gi").test(oFile.type)) {
                alert("照片上传：文件类型必须是JPG、JPEG、PNG");
                return;
            }

            var reader = new FileReader();
            reader.onload = function (e) {
                var base64Img = e.target.result;
                //压缩前预览
                //  $("#preview").attr("src",base64Img);  
                var ddv = document.getElementById("box");
                //var changdu = ddv.getElementsByTagName("img");
                //Number(changdu.length) + Number(1);

                //赋值到隐藏域传给后台

                //   document.getElementById("txtname2").value += req.responseText + ",";
                var aaa = "<div id='div" + bianhao + "'  class='imgbox hasboth'  style='background-color:#fff;width:" + _width + "px;height:" + _height + "px;'>";
                var aa = " <div class='w_upload' style='width:" + _width + "px;height:" + _height + "px;' >    <a class='item_close' onclick=deletepic('" + bianhao + "')>删除</a><span style='width:" + _width + "px;height:" + _height + "px;' class='item_box'>";
                var bb = "<img id='img" + bianhao + "' src='" + base64Img + "'  style='background:url(images/aa1.gif) no-repeat center;width:" + _width + "px;height:" + _height + "px;'/></span>";
                //var aaa = "<div id='div" + bianhao + "'  class='imgbox hasboth'  style='background-color:#fff;'>";
                //var aa = " <div class='w_upload' >    <a class='item_close' onclick=deletepic('" + bianhao + "')>删除</a><span class='item_box'>";
                //var bb = "<img id='img" + bianhao + "' src='"+base64Img+"'  style='background:url(images/aa1.gif) no-repeat center;'/></span>";
                var dds = "</div></div>";
                $("#box").append(aaa + aa + bb + dds);
                bianhao++;
                //--执行resize。  
                var _ir = ImageResizer({
                    resizeMode: "auto"
                    , dataSource: base64Img
                    , dataSourceType: "base64"
                    , maxWidth: 600 //允许的最大宽度  
                    , maxHeight: 600 //允许的最大高度。  
                    , onTmpImgGenerate: function (img) {

                    }
                    , success: function (resizeImgBase64, canvas) {
                        //压缩后预览
                        // $("#nextview").attr("src", resizeImgBase64);
                        // ///  $("#divp").append("<img src='" + resizeImgBase64 + "' />");
                        // var ddva = document.getElementById("divpics2");
                        // var changdua = ddva.getElementsByTagName("img");
                        // var bianhaoa = Number(changdua.length);
                        //// alert(bianhao);
                        // $("#img" + bianhaoa).attr("src", resizeImgBase64);
                        var tupian = resizeImgBase64 + "#";
                        document.getElementById("txtname").value += tupian;
                        document.getElementById("status").value = "true";
                        //      $("#divpics2").append("<img src='" + resizeImgBase64 + "' />");
                        //$('#upTo').on('click', function () {
                        //    //alert('传给后台base64文件数据为：'+resizeImgBase64.substr(22));
                        //    $.ajax({
                        //        url: 'server.php',
                        //        type: 'POST',
                        //        dataType: 'json',
                        //        data: {
                        //            imgOne: $('#imgOne').val()
                        //        },
                        //        success: function (data) {
                        //            alert(data.msg);
                        //        }
                        //    });
                        //});

                    }
                    , debug: true
                });

            };
            reader.readAsDataURL(oFile);
        }

    }, false);

});
function sleep(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}
function deletepic(aa) {
    var oldnode = document.getElementById("div" + aa);
    document.getElementById("div" + aa).parentNode.removeChild(oldnode)
    var ddv = document.getElementById("box");
    var changdu = ddv.getElementsByTagName("img");
    var stra = "";
    for (var i = 0; i < changdu.length; i++) {
        var url = window.location.href;
        var newurl = url.split('manager')[0];
        stra += changdu[i].src + "#";
        stra = stra.replace(newurl, '')
    }
    document.getElementById("txtname").value = stra;
}

//         <asp:TextBox ID="txtname" runat="server" Width="1000px" Text="" style="display:none"></asp:TextBox>
//        <p>多张：<input type="file" multiple="multiple" id="multiple" style="opacity: 0;" /></p>

//<div id="box" class="msg_b" style="overflow:visible"></div>