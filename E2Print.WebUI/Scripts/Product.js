/// <reference path="jquery-1.10.2.min.js" />

//http://stackoverflow.com/questions/22795074/why-should-i-reference-jquery-in-a-self-contained-function

(function ($, undefined) {
    $(document).on('click', '.thumbnail', function () {
        $('.thumbnail').css('border', '1px solid #fff');  //this cancels the css hover effect
        $(this).css({
            'border': '1px solid #f79323'
        });
        var newSrc = $(this).attr('src');
        $('#productPhoto').fadeOut(500, function () {
            $('#productPhoto').attr("src", newSrc);
            $('#productPhoto').fadeIn(500);
        });
    });
})(jQuery);

var dds = new Array();
$(function () {
    $('.wrapper-dropdown-3').each(function (index, item) {
        dds.push(new DropDown($(item)));
    });

    $(document).click(function () {
        $('.wrapper-dropdown-3').removeClass('active');
    });
});
function DropDown(el) {
    this.dd = el;
    this.placeholder = this.dd.children('span');
    this.opts = this.dd.find('ul.dropdown > li');
    this.val = '';
    this.data = '';
    this.index = -1;
    this.initEvents();
}

DropDown.prototype = {
    initEvents: function () {
        var obj = this;
        obj.dd.on('click', function (event) {
            $(this).toggleClass(function () {
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    return '';
                } else {
                    $('.wrapper-dropdown-3').css('z-index', '1');
                    $(this).css('z-index', '99');
                    return 'active';
                }
            });
            //$(this).toggleClass('active');
            return false;
        });

        obj.opts.on('click', function () {
            var opt = $(this);
            obj.val = opt.text();
            obj.data = opt.find('.opt-data').val();
            obj.index = opt.index();
            obj.placeholder.text(obj.val);
            CalculateTotal();
        });
    },
    getValue: function () {
        return this.val;
    },
    getData: function () {
        return this.data;
    },
    getIndex: function () {
        return this.index;
    }
}


function CalculateTotal() {
    var total = 0;
    //var onsaleDiscount = $('#onsaleDiscount').val();
    //var userDiscount = $('#userDiscount').val();
    var products = $.parseJSON($('#products').val());

    $.each(products, function (index, product) {
        var size = $('#size_dd span').text();
        var color = $('#color_dd span').text();
        var material = $('#material_dd span').text();
        var buyingQty = $('#quantity_dd span').text();
        //alert(product.Size + ':' + size + '\n' + product.BuyingQty + ':' + buyingQty + '\n' + product.Color + ':' + color + '\n' + product.Material + ':' + material);
        if (product.Size == size && product.BuyingQty == buyingQty && product.Color == color && product.Material == material) {
            total = product.Price;
            $('.product-detail-id').text(product.Id);
        }
    });
    $('.price-normal').text('$'+total.toFixed(2));
    //$('.price-discount').text('$' + (total * onsaleDiscount * userDiscount).toFixed(2));
}

