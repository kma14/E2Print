/// <reference path="jquery-1.10.2.min.js" />
$('#category').change(function () {
    $.post("/Category/GetCategoryPhoto", { categoryId: $(this).val() }, function (data) {
        //alert(JSON.stringify(data));
        $('div#categoryPhotos').html('');
        $('div#categoryPhotos').append('<span id="photoUrl" style="display:inline-block;float:left;line-height:70px;margin:0px 20px;"></span><a href="#" class="deletePhoto dark" style="display:inline-bock;float:left;line-height:70px;font-size:1.3em"></a><div style="clear:both;"></div>');
        $.each(data.Message, function (index, value) {
            $("<img class=\"thumbnail\"  style=\"float:left;\" src=\"" + value + "\"/>").insertBefore('span#photoUrl');
        })
    });
});

$(document).on('click', '.thumbnail', function () {
    $('span#photoUrl').html($(this).attr('src'));
    $('a.deletePhoto').html ('[Delete]');
    $('.thumbnail').removeClass('thumbnail-selected');
    $(this).addClass('thumbnail-selected');
});

$(document).on('click', '.deletePhoto', function () {
    var data = { fileUrl: $('span#photoUrl').text() };
    alert(JSON.stringify(data));
    $.post("/Photo/DeletePhoto", data, function (data) {
        if (data.Result == 'OK') {
            $('.thumbnail-selected').remove();
        } else {
            alert(data.Message);
        }
    });
});


