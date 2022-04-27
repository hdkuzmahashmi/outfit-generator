function GetDamenCategory(url) {
    alert("call");
    
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            alert("success");
        }
    }).done(function (res) {
        //if (res.result === "1") {
            //res.data = res.data == null ? "" : res.data;
            //$("#label_" + count).text(res.data);
            alert(res);
        //}
        //else {
          //  alert(res.message);
        //}

    }).fail(function (xhr) {
        
        alert("errrrrrr"+xhr);
    });
}

