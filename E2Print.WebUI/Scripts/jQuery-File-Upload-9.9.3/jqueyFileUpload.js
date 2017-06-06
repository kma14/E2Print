/// <reference path="jquery-1.10.2.min.js" />

/*jslint unparam: true */
/*global window, $ */
$(function () {
    'use strict';
    // Change this to the location of your server-side upload handler:
    $('.fileupload').fileupload({
        //dataType: 'json',
        add:function(e,data){
            data.formData = { 'photoType': '5' };
            data.submit();
        },
        done: function (e, data) {
            alert('d');
            //$.each(data.result.files, function (index, file) {
            //    $('<p/>').text(file.name).appendTo('#files');
            //});
        },
        fail: function(e, data) {
            alert('Fail!');
        },
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progress .progress-bar').css(
                'width', progress + '%'
            );
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});