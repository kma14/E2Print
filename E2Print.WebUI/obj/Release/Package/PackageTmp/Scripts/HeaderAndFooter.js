/// <reference path="jquery-1.10.2.min.js" />

//http://stackoverflow.com/questions/22795074/why-should-i-reference-jquery-in-a-self-contained-function

(function ShowHideFooter($, undefined) {
    //alert($(window).height());
    //alert($(document).height());
    $('body').height($(window).height());

    //var footer = $('.footer');
    //var start = $(footer).offset().top;
    //$.event.add(window, "scroll", function () {
    //    var p = $(window).scrollTop();
    //    alert(p);
    //    $(footer).css('position', ((p) > start) ? 'fixed' : 'static');
    //    $(footer).css('top', ((p) > start) ? '0px' : '');
    //});
    $('i.entypo-cancel').hide();

    $('.contact-header').on('click', function () {
        var isOpen = $('i.entypo-cancel').is(':visible');
        if (isOpen) {
            $('i.entypo-down-open-big').show();
            $('i.entypo-cancel').hide();
            $('.contact-header-sub').css('background-position-y', '-5em');
            $('.contact-header-subsub').css('background-position-y', '0em');
            $('.contact-list').css('background-color', '#060606');
            $('.contact-list').css('opacity', '0.75');
            $("#footer").animate({
                "margin-top": "+=140",
            }, 500, function () {});
        } else {
            $('i.entypo-down-open-big').hide();
            $('i.entypo-cancel').show();
            $('.contact-header-sub').css('background-position-y', '-15em');
            $('.contact-header-subsub').css('background-position-y', '-10em');
            $('.contact-list').css('background-color', '#444');
            $('.contact-list').css('opacity', '1');
            $("#footer").animate({
                "margin-top": "-=140",
            }, 500, function () {});
        }
    });
})(jQuery);

(function($, undefined) {
    var navbar = $('.navbar');
    var start = $(navbar).offset().top - 50;
    $.event.add(window, "scroll", function () {
        var left = $(navbar).offset().left;
        var p = $(window).scrollTop();
        $(navbar).css('position', ((p) > start) ? 'fixed' : 'static');
        if (p > start) {
            $('.navbar-title').fadeIn(300)
            navbar.css('left', left);
        } else {
            $('.navbar-title').hide();
        }
        $(navbar).css('top', ((p) > start) ? '0px' : '');
    });
})(jQuery);

(function () {
    $(".meter > span").each(function () {
        $(this)
            .data("origWidth", $(this).width())
            .width(0)
            .animate({
                width: $(this).data("origWidth")
            }, 1200);
    });
})(jQuery);