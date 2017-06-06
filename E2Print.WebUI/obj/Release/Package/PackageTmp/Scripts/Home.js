/// <reference path="jquery-1.10.2.min.js" />

//http://stackoverflow.com/questions/22795074/why-should-i-reference-jquery-in-a-self-contained-function

(function ($, undefined) {
    $('.category-item-style0  img').on('mouseenter', function () {
        $(this).animate({
            opacity: "0.8"
        }, 300, function () { });
    });
    $('.category-item-style0  img').on('mouseleave', function () {
        $(this).animate({
            opacity: "1"
        }, 100, function () { });
    });

    $('.category-item-style1').on('mouseenter', function () {
        var text = $(this).find('small').text();
        var maxLength = 90;
        if ($(this).hasClass('category-largeItem') && text.length > maxLength * 2) {
            $(this).find('small').text(text.substring(0, maxLength*2+1) + ' ...');
        } else if (text.length > maxLength) {
            $(this).find('small').text(text.substring(0, maxLength+1) + ' ...');
        }
        $(this).find('small').show(200);
        var titleHeight = $(this).children('.category-title').height();
        var parentHeight = $(this).height();
        $(this).children('.category-title').animate({
            top: parentHeight - titleHeight-10
        }, 300, 'linear',function () { });
    });
    $('.category-item-style1').on('mouseleave', function () {
        $(this).find('small').hide(100);
        $(this).children('.category-title').animate({
            top: '60%'
        }, 300, 'linear',function () { });
    });
})(jQuery);


(function RidrectAfterRegister($, undefined) {
    var autoSaveInterval = 6;
    var countDownSecs = autoSaveInterval;

    if ($('.redirect-countdown').length > 0) {
        setInterval(function () {
            $('.redirect-countdown').html(' in ' + countDownSecs + ' seconds');
            if (countDownSecs-- == 0) {
                //It is better than using window.location.href =, because replace() does not put the originating page in the session history, meaning the user won't get stuck in a never-ending back-button fiasco. If you want to simulate someone clicking on a link, use location.href. If you want to simulate an HTTP redirect, use location.replace
                window.location.replace("/Home/Index");
            }
        }, 1000);
    }
})(jQuery);