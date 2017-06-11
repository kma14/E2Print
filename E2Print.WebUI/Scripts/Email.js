/// <reference path="jquery-1.10.2.min.js" />

$(document).on('click', 'button.email-send', function () {
    var emailContent = $(this).siblings('.email-content').val();
    var userEmail = $(this).siblings('.email-userEmail').val();
    var userCellphone = $(this).siblings('.email-userCellphone').val();

    $('span.email-sending').show();
    $('.email-content').prop('disabled', true);
    $('.email-userEmail').prop('disabled', true);
    $('.email-userCellphone').prop('disabled', true);
    $('button.email-send').prop('disabled', true);
    $('button.email-cancel').prop('disabled', true);

    debugger;
    $.post('/Home/SendEmail', { emailContent: emailContent, userEmail: userEmail, userCellphone: userCellphone }, function (data) {
        alert(data.Message);
        if (data.Result == 'succeed') {
            $('.email-content').val('');
            $('.email-userEmail').val('');
            $('.email-userCellphone').val('');
            $('.email-content').prop('disabled', false);
            $('.email-userEmail').prop('disabled', false);
            $('.email-userCellphone').prop('disabled', false);
            $('button.email-send').prop('disabled', false);
            $('button.email-cancel').prop('disabled', false);
            $('span.email-sending').hide();
        }
    })
});

$(document).on('click', 'button.email-cancel', function () {
    $(this).siblings('.email-content').val('');
    $(this).siblings('.email-userEmail').val('');
    $(this).siblings('.email-userCellphone').val('');
});

//click button set map center
//$(document).on('click'), '', function () {
//        map.setCenter({
//            lat: newLat,
//            lng: newLng
//        });
//}
function initMap() {
    var uluru = { lat: -36.937575, lng: 174.810542 };

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 11,
        center: uluru,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(-36.973213, 174.787805),
        icon: "../Content/Images/MapMarker.png",
        draggable: false,
        map: map
    });
    var marker2 = new google.maps.Marker({
        position: new google.maps.LatLng(-36.904366, 174.809929),
        icon: "../Content/Images/MapMarker.png",
        draggable: false,
        map: map
    });
}

//(function initializeGoogleMap() {
//    var mapCanvas = document.getElementById('map');
//    var mapOptions = {
//        center: new google.maps.LatLng(-36.937575, 174.810542),
//        zoom: 11,
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    }
//    var map = new google.maps.Map(mapCanvas, mapOptions);

//    var marker = new google.maps.Marker({
//        position: new google.maps.LatLng(-36.973213, 174.787805),
//        icon: "../Content/Images/MapMarker.png",
//        draggable: false,
//        map: map
//    });
//    var marker2 = new google.maps.Marker({
//        position: new google.maps.LatLng(-36.904366, 174.809929),
//        icon: "../Content/Images/MapMarker.png",
//        draggable: false,
//        map: map
//    });
//})(window, document, jQuery);
