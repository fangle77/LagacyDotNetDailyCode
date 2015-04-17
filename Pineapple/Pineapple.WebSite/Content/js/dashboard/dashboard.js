$(function () {
    initTruncateContentHover();
});

function initTruncateContentHover() {
    $(document).tooltip({
        content: function () {
            return $(this).find(".origin-content").html();
        },
        items: ".truncate-content"
    });
}

function ajaxPostJson(url, postData, callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: postData,
        dataType: "json",
        success: callback,
        error: ExceptionMessage
    });
}

function ExceptionMessage(ex){
   if(typeof(console) !== "undefined" && console && console.error){
   		console.error(ex);
   }
}