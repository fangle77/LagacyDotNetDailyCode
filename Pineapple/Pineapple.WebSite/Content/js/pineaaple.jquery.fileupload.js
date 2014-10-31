$(function () {
    'use strict';
    $('#fileupload').fileupload({
        url: '/Manager/Attachment/Upload',
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        maxFileSize: 5000000,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png|bmp|ico)$/i,
        add: function (e, data) {
            $.each(data.files,function(i,e){
                console.log(e);
            });
        }
    });
});