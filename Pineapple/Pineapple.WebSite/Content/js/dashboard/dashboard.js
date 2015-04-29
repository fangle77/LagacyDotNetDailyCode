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

var FormHelper = {
	getParams:function(control){
		if(typeof control === "undefined") control = "body";
		
		var inputs = $(control).find('input[name]');
		if(inputs.length == 0 ) return {};
		
		var param = {};
		$.each(inputs,function(i,e){
			var $e = $(e);
			param[$e.attr('name')] = $e.val();
		});
		console.log(param);
		return param;
	}
};