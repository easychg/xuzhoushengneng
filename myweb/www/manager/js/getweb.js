function deletenew(newid) {
   
    $.ajax({
        type: "post", //访问WebService使用Post方式请求

        url: "service.asmx/deletenew", //调用Url(WebService的地址和方法名称组合---WsURL/方法名)

        data: '{"newid":"'+newid+'"}',
        //这里是要传递的参数，为Json格式{paraName:paraValue}
        dataType: 'json',
        contentType: "Application/Json", // 发送信息至服务器时内容编码类型
        async: false,
        cache: false,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
        },
        success: function (data) {
            var json = eval('(' + data.d + ')');
            var chengong = json.nums;       


        },
        error: function (err) {
            alert("err:" + err);
        }
    });
}
function deletehuiyuan(huiyuanid) {

    $.ajax({
        type: "post", //访问WebService使用Post方式请求

        url: "service.asmx/deletehuiyuan", //调用Url(WebService的地址和方法名称组合---WsURL/方法名)

        data: '{"huiyuanid":"' + huiyuanid + '"}',
        //这里是要传递的参数，为Json格式{paraName:paraValue}
        dataType: 'json',
        contentType: "Application/Json", // 发送信息至服务器时内容编码类型
        async: false,
        cache: false,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
        },
        success: function (data) {
            var json = eval('(' + data.d + ')');
            var chengong = json.nums;


        },
        error: function (err) {
            alert("err:" + err);
        }
    });
}
function deletepaimai(paimaiid) {
    
    $.ajax({
        type: "post", //访问WebService使用Post方式请求

        url: "service.asmx/deletepaimai", //调用Url(WebService的地址和方法名称组合---WsURL/方法名)

        data: '{"paimaiid":"' + paimaiid + '"}',
        //这里是要传递的参数，为Json格式{paraName:paraValue}
        dataType: 'json',
        contentType: "Application/Json", // 发送信息至服务器时内容编码类型
        async: false,
        cache: false,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "Application/Json"); // 接受的数据类型。(貌似不起作用，因为WebService的请求/返回 类型是相同的，由于请求的是Json，所以，返回的默认是Json)
        },
        success: function (data) {
            var json = eval('(' + data.d + ')');
            var chengong = json.nums;


        },
        error: function (err) {
            alert("err:" + err);
        }
    });
}