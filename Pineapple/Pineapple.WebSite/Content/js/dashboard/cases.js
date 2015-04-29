$(function(){
	var container = $('#caseItemContainer');
	if(container.length == 0) return;
	
	var saveUrl = $('#caseItemSaveUrl').val();
	var deleteUrl = $('#caseItemDeleteUrl').val();
	
	$(container).on('click','.case-item-save',function(){
	console.log(111);
		var panel = $(this).closest('.panel');
		var param = FormHelper.getParams(panel);
		ajaxPostJson(saveUrl,param, function(data){
			if(data == "true"){
				location.href = location.href;
			}
		});
		
		return false;
	});
	
	$(container).on('click','.case-item-delete',function(){
		var panel = $(this).closest('.panel');
		var url = deleteUrl + "/" + panel.find('input[name=CaseItemId]').val();
		ajaxPostJson(url,{}, function(data){
			if(data == "true"){
				location.href = location.href;
			}
		});
		
		return false;
	});
});