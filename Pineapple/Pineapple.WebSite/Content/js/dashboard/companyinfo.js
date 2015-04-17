$(function(){
	var companyInfoTable = $('#companyinfo');
	if(companyInfoTable.length == 0 ) return;
	
	companyInfoTable.on('click',".company-save",function(){
		var parentTr = $(this).closest('tr');
		var key = parentTr.find('.company-key').text();
		var content = parentTr.find('.company-content').val();
		if($.trim(key).length==0 || $.trim(content).length==0) return;
		
		var param = {
			key : key,
			content : content
		};
		ajaxPostJson($('#companySaveUrl').val(),param, function(data){
			if(data == "true"){
				parentTr.find('.company-content-lable').show().text(content);
				parentTr.find('.company-content').hide();
			}
		});
		
		return false;
	});
	
	companyInfoTable.on('click','.company-content-lable',function(){
		$(this).hide().closest('tr').find('.company-content').show().focus();
	});
	
	companyInfoTable.on('click',"#addCompanyInfo",function(){
		var parentTr = $(this).closest('tr');
		var key = parentTr.find('.company-key').val();
		var content = parentTr.find('.company-content').val();
		if($.trim(key).length==0 || $.trim(content).length==0) return;
		
		var param = {
			key : key,
			content : content
		};
		ajaxPostJson($('#companySaveUrl').val(),param, function(data){
			if(data == "true"){
				location.href = location.href;
			}
		});
		
		return false;
	});
	
	companyInfoTable.on('click',".company-delete",function(){
		var parentTr = $(this).closest('tr');
		var key = parentTr.find('.company-key').text();
		if($.trim(key).length==0) return;
		
		var param = {
			key : key
		};
		ajaxPostJson($('#companyDeleteUrl').val(),param, function(data){
			if(data == "true"){
				parentTr.remove();
			}
		});
		
		return false;
	});
});