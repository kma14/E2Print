
(function ($) {
  'use strict';

  /*======== 1. PREELOADER ========*/
  $(window).on('load', function () {
    $('#preloader').fadeOut(500);
  });

  /*======== 1. SMOOTH SCROLLING TO SECTION ========*/
  $('.scrolling  a[href*="#"]').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    var target = $(this).attr('href');

    function customVeocity(set_offset) {
      $(target).velocity('scroll', {
        duration: 800,
        offset: set_offset,
        easing: 'easeOutExpo',
        mobileHA: false
      });
    }

    if ($('#body').hasClass('up-scroll') || $('#body').hasClass('static')) {
      customVeocity(2);
    } else {
      customVeocity(-90);
    }

  });

  /*======== 2. NAVBAR ========*/
  $(window).on('load', function () {

    var header_area = $('.header');
    var main_area = header_area.find('.nav-menuzord');
    var zero = 0;
    var navbarSticky = $('.navbar-sticky');

    $(window).scroll(function () {
      var st = $(this).scrollTop();
      if (st > zero) {
        navbarSticky.addClass('navbar-scrollUp');
      } else {
        navbarSticky.removeClass('navbar-scrollUp');
      }
      zero = st;

      if (main_area.hasClass('navbar-sticky') && ($(this).scrollTop() <= 600 || $(this).width() <= 300)) {
        main_area.removeClass('navbar-scrollUp');
        main_area.removeClass('navbar-sticky').appendTo(header_area);
        header_area.css('height', 'auto');
      } else if (!main_area.hasClass('navbar-sticky') && $(this).width() > 300 && $(this).scrollTop() > 600) {
        header_area.css('height', header_area.height());
        main_area.addClass('navbar-scrollUp');
        main_area.css({ 'opacity': '0' }).addClass('navbar-sticky');
        main_area.appendTo($('body')).animate({ 'opacity': 1 });
      }
    });

    $(window).trigger('resize');
    $(window).trigger('scroll');
  });

  /* ======== ALL DROPDOWN ON HOVER======== */
  if ($(window).width() > 991) {
    $('.navbar-expand-lg .navbar-nav .dropdown').hover(function () {
      $(this).addClass('').find('.dropdown-menu').addClass('show');
    }, function () {
      $(this).find('.dropdown-menu').removeClass('show');
    });
  }

  if ($(window).width() > 767) {
    $('.navbar-expand-md .navbar-nav .dropdown').hover(function () {
      $(this).addClass('').find('.dropdown-menu').addClass('show');
    }, function () {
      $(this).find('.dropdown-menu').removeClass('show');
    });
  }

  //============================== Menuzord =========================
  $('#menuzord').menuzord({
    indicatorFirstLevel: '<i class="fa fa-angle-down"></i>',
    indicatorSecondLevel: '<i class="fa fa-angle-right"></i>'
  });

  //============================== food-slider =========================
  $('.food-slider').owlCarousel({
    loop: true,
    margin: 30,
    nav: false,
    dots: true,
    responsiveClass: true,
    responsive: {
      320: {
        margin: 0,
        items: 1
      },
      480: {
        items: 2
      },
      991: {
        items: 3
      }
    }
  });

  //============================== testimonial-slider =========================
  $('.testimonial').owlCarousel({
    margin: 0,
    loop: true,
    autoplay: true,
    autoplayHoverPause: true,
    dots: true,
    autoplayTimeout: 3000,
    smartSpeed: 1000,
    items: 1
  });

  //============================== package-slider =========================
  $('#package-slider').owlCarousel({
    margin: 0,
    loop: true,
    autoplay: true,
    autoplayHoverPause: true,
    dots: false,
    nav: true,
    navText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>'],
    autoplayTimeout: 3000,
    smartSpeed: 800,
    mouseDrag: false,
    items: 1
  });

  //============================== gallery-photo-slider =========================
  $('#gallery-photo-slider').owlCarousel({
    margin: 0,
    loop: true,
    autoplay: true,
    autoplayHoverPause: true,
    dots: false,
    nav: true,
    navText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>'],
    autoplayTimeout: 3000,
    smartSpeed: 800,
    mouseDrag: false,
    URLhashListener: true,
    startPosition: 'URLHash',
    items: 1
  });

  //===================== owl-carousel active data-hash =====================
  $('.owl-carousel').on('changed.owl.carousel', function (event) {
    var current = event.item.index;
    var hash = $(event.target).find('.owl-item').eq(current).find('.item').attr('data-hash');
    $('.' + hash).addClass('active');
  });

  $('.owl-carousel').on('change.owl.carousel', function (event) {
    var current = event.item.index;
    var hash = $(event.target).find('.owl-item').eq(current).find('.item').attr('data-hash');
    $('.' + hash).removeClass('active');
  });

  //============================== banner-carousel =========================
  $('#banner-carousel').owlCarousel({
    margin: 0,
    loop: true,
    animateOut: 'fadeOut',
    animateIn: 'fadeIn',
    autoplay: true,
    autoplayHoverPause: true,
    autoplayTimeout: 5000,
    smartSpeed: 1200,
    dots: false,
    nav: true,
    navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
    items: 1
  });

  //============================== blog-slider =========================
  $('.blog-slider').owlCarousel({
    margin: 0,
    loop: true,
    autoplay: false,
    autoplayTimeout: 6000,
    dots: false,
    nav: true,
    navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
    items: 1
  });

  //============================== SLICK CARUSEL =========================
  $('.gallery-slider').slick({
    centerMode: true,
    centerPadding: '100px',
    slidesToShow: 1,
    loop: true,
    autoplay: true,
    autoplayHoverPause: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          centerMode: false
        }
      }
    ]
  });

  //============================== BRAND SLIDER =========================
  $('.brand-slider').slick({
    loop: true,
    autoplay: true,
    autoplayHoverPause: true,
    autoplayTimeout: 3000,
    slidesToShow: 4,
    slidesToScroll: 1,
    centerMode: true,
    centerPadding: '0px',
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]

  });

  //============================== Rev-Slider =========================
  var homeMain = $('#rev_slider_12_1');
  if (homeMain.length !== 0) {
    $('#rev_slider_12_1').show().revolution({
      sliderType: 'standard',
      jsFileLocation: '//revs.iamabdus.com/revslider/public/assets/js/',
      sliderLayout: 'fullwidth',
      dottedOverlay: 'none',
      delay: 6000,
      navigation: {
        keyboardNavigation: 'off',
        keyboard_direction: 'horizontal',
        mouseScrollNavigation: 'off',
        mouseScrollReverse: 'default',
        onHoverStop: 'off',
        arrows: {
          style: 'metis',
          enable: true,
          hide_onmobile: true,
          hide_under: 0,
          hide_onleave: true,
          hide_delay: 200,
          hide_delay_mobile: 1200,
          tmp: '',
          left: {
            h_align: 'left',
            v_align: 'center',
            h_offset: 20,
            v_offset: 0
          },
          right: {
            h_align: 'right',
            v_align: 'center',
            h_offset: 20,
            v_offset: 0
          }
        },
        bullets: {
          enable: true,
          hide_onmobile: false,
          style: 'hesperiden',
          hide_onleave: false,
          direction: 'horizontal',
          h_align: 'center',
          v_align: 'bottom',
          h_offset: 0,
          v_offset: 20,
          space: 5,
          tmp: ''
        }
      },
      responsiveLevels: [1240, 1025, 778, 480],
      visibilityLevels: [1240, 1025, 778, 480],
      gridwidth: [1170, 1024, 769, 480],
      gridheight: [745, 743, 557, 557],
      lazyType: 'none',
      shadow: 0,
      spinner: 'off',
      stopLoop: 'on',
      stopAfterLoops: -1,
      stopAtSlide: -1,
      shuffle: 'off',
      autoHeight: 'off',
      disableProgressBar: 'on',
      hideThumbsOnMobile: 'off',
      hideSliderAtLimit: 0,
      hideCaptionAtLimit: 0,
      hideAllCaptionAtLilmit: 0,
      debugMode: false,
      fallbacks: {
        simplifyAll: 'off',
        nextSlideOnWindowFocus: 'off',
        disableFocusListener: false
      }
    });
  }

  //============================== Rev-Slider Home-2 =========================
  var homeCity = $('#rev_slider_18_1');
  if (homeCity.length !== 0) {
    $('#rev_slider_18_1').show().revolution({
      sliderType: 'standard',
      jsFileLocation: '//revs.iamabdus.com/revslider/public/assets/js/',
      sliderLayout: 'fullwidth',
      dottedOverlay: 'none',
      delay: 9000,
      navigation: {
        keyboardNavigation: 'off',
        keyboard_direction: 'horizontal',
        mouseScrollNavigation: 'off',
        mouseScrollReverse: 'default',
        onHoverStop: 'off',
        arrows: {
          style: 'metis',
          enable: true,
          hide_onmobile: true,
          hide_under: 0,
          hide_onleave: true,
          hide_delay: 200,
          hide_delay_mobile: 1200,
          tmp: '',
          left: {
            h_align: 'left',
            v_align: 'center',
            h_offset: 20,
            v_offset: 0
          },
          right: {
            h_align: 'right',
            v_align: 'center',
            h_offset: 20,
            v_offset: 0
          }
        },
        bullets: {
          enable: true,
          hide_onmobile: false,
          style: 'hesperiden',
          hide_onleave: false,
          direction: 'horizontal',
          h_align: 'center',
          v_align: 'bottom',
          h_offset: 0,
          v_offset: 20,
          space: 5,
          tmp: ''
        }
      },
      responsiveLevels: [1240, 1025, 778, 480],
      visibilityLevels: [1240, 1025, 778, 480],
      gridwidth: [1170, 1025, 769, 480],
      gridheight: [745, 743, 557, 557],
      lazyType: 'none',
      shadow: 0,
      spinner: 'off',
      stopLoop: 'off',
      stopAfterLoops: -1,
      stopAtSlide: -1,
      shuffle: 'off',
      autoHeight: 'off',
      disableProgressBar: 'on',
      hideThumbsOnMobile: 'off',
      hideSliderAtLimit: 0,
      hideCaptionAtLimit: 0,
      hideAllCaptionAtLilmit: 0,
      debugMode: false,
      fallbacks: {
        simplifyAll: 'off',
        nextSlideOnWindowFocus: 'off',
        disableFocusListener: false
      }
    });
  }

  //============================== Video Slider =========================
  var homeDestination = $('#rev_slider_19_1');
  if (homeDestination.length !== 0) {
    $('#rev_slider_19_1').show().revolution({
      sliderType: 'standard',
      jsFileLocation: '//revs.iamabdus.com/revslider/public/assets/js/',
      sliderLayout: 'fullwidth',
      dottedOverlay: 'none',
      delay: 9000,
      navigation: {
        onHoverStop: 'off'
      },
      responsiveLevels: [1240, 1025, 778, 480],
      visibilityLevels: [1240, 1025, 778, 480],
      gridwidth: [1170, 1025, 769, 480],
      gridheight: [750, 620, 420, 270],
      lazyType: 'none',
      shadow: 0,
      spinner: 'off',
      stopLoop: 'off',
      stopAfterLoops: -1,
      stopAtSlide: -1,
      shuffle: 'off',
      autoHeight: 'off',
      disableProgressBar: 'on',
      hideThumbsOnMobile: 'off',
      hideSliderAtLimit: 0,
      hideCaptionAtLimit: 0,
      hideAllCaptionAtLilmit: 0,
      debugMode: false,
      fallbacks: {
        simplifyAll: 'off',
        nextSlideOnWindowFocus: 'off',
        disableFocusListener: false
      }
    });
  }
  
  //============================== daterangepicker =========================
  $('input[name="dateRange"]').daterangepicker({
    autoUpdateInput: false,
    singleDatePicker: true,
    locale: {
      cancelLabel: 'Clear'
    }
  });

  $('input[name="dateRange"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY'));
  });

  $('input[name="dateRange"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
  });

  //============================== VIDEOBOX =========================
  var videoBox = $('.video-box img');
  videoBox.on('click', function () {
    var video = '<iframe class="embed-responsive-item"  allowfullscreen src="' + $(this).attr('data-video') + '"></iframe>';
    $(this).replaceWith(video);
  });

  //============================== SELECT 2 =========================
  $('.select2-select').select2();

  //============================== SELECTRIC =========================
  $('.select-option').selectric();

  /*========  syotimer ========*/
  $('#coming-soon').syotimer({
    year: 2020,
    month: 7,
    day: 3,
    hour: 20,
    minute: 30
  });

  /*======== Tooltip  ========*/
  $('[data-toggle="tooltip"]').tooltip();

  /*======== 11.PRICE SLIDER RANGER  ========*/
  var nonLinearStepSlider = document.getElementById('slider-non-linear-step');
  if (nonLinearStepSlider) {
    noUiSlider.create(nonLinearStepSlider, {
      connect: true,
      start: [125, 750],
      range: {
        'min': [0],
        'max': [1000]
      }
    });
  }

  var sliderValue = [
    document.getElementById('lower-value'), // 0
    document.getElementById('upper-value')  // 1
  ];

  // Display the slider value and how far the handle moved
  // from the left edge of the slider.
  var priceRange = document.getElementById('price-range');
  if (priceRange) {
    nonLinearStepSlider.noUiSlider.on('update', function (values, handle) {
      sliderValue[handle].innerHTML = '$' + Math.floor(values[handle]);
    });
  }

  /*======== COUNT INPUT (Quantity) ========*/
  $('.incr-btn').on('click', function (e) {
    var newVal;
    var $button = $(this);
    var oldValue = $button.parent().find('.quantity').val();
    $button.parent().find('.incr-btn[data-action=decrease]').removeClass('inactive');
    if ($button.data('action') === 'increase') {
      newVal = parseFloat(oldValue) + 1;
    } else {
      // Don't allow decrementing below 1
      if (oldValue > 1) {
        newVal = parseFloat(oldValue) - 1;
      } else {
        newVal = 0;
        $button.addClass('inactive');
      }
    }
    $button.parent().find('.quantity').val(newVal);
    e.preventDefault();
  });

  /*======== 18. COUNTER-UP ========*/
  var counter = $('#counter');
  if (counter.length) {
    var a = 0;
    $(window).scroll(function () {
      var oTop = counter.offset().top - window.innerHeight;
      if (a === 0 && $(window).scrollTop() > oTop) {
        $('.counter-value').each(function () {
          var $this = $(this),
            countTo = $this.attr('data-count');
          $({
            countNum: $this.text()
          }).animate({
            countNum: countTo
          },
          {
            duration: 5000,
            easing: 'swing',
            step: function () {
              $this.text(Math.floor(this.countNum));
            },
            complete: function () {
              $this.text(this.countNum);
            }
          });
        });
        a = 1;
      }
    });
  }

  /*======== 8.  RETING ========*/
  var ratingDefault = $('.add-rating-default');
  if (ratingDefault.length !== 0) {
    ratingDefault.rateYo({
      'starWidth': '14px',
      'spacing': '5px',
      'normalFill': '#969696',
      'ratedFill': '#fec701',
      'starSvg': '<svg xmlns="http://www.w3.org/2000/svg" width=24 height="24" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>'
    });
  }

  var ratingLarge = $('.add-rating-large');
  if (ratingLarge.length !== 0) {
    ratingLarge.rateYo({
      'starWidth': '20px',
      'spacing': '5px',
      'normalFill': '#969696',
      'ratedFill': '#fec701',
      'starSvg': '<svg xmlns="http://www.w3.org/2000/svg" width=24 height="24" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>'
    });
  }

  //============================== ISOTOPE =========================
  // init Isotope
  var $grid = $('.grid').imagesLoaded( function() {
    // init Isotope after all images have loaded
    $grid.isotope({
      itemSelector: '.element-item',
    layoutMode: 'fitRows'
    });
  });

  // filter functions
  var filterFns = {
    // show if number is greater than 50
    numberGreaterThan50: function () {
      var number = $(this).find('.number').text();
      return parseInt(number, 10) > 50;
    },
    // show if name ends with -ium
    ium: function () {
      var name = $(this).find('.name').text();
      return name.match(/ium$/);
    }
  };

  // bind filter button click
  $('#filters').on('click', 'button', function () {
    var filterValue = $(this).attr('data-filter');
    // use filterFn if matches value
    filterValue = filterFns[filterValue] || filterValue;
    $grid.isotope({ filter: filterValue });
  });

  // bind sort button click
  $('#sorts').on('click', 'button', function () {
    var sortByValue = $(this).attr('data-sort-by');
    $grid.isotope({ sortBy: sortByValue });
  });

  // change is-checked class on buttons
  $('.button-group').each(function (i, buttonGroup) {
    var $buttonGroup = $(buttonGroup);
    $buttonGroup.on('click', 'button', function () {
      $buttonGroup.find('.is-checked').removeClass('is-checked');
      $(this).addClass('is-checked');
    });
  });

  // element-right-sidebar
  $(window).scroll(function () {
    if ($(this).scrollTop() > 300) {
      $('.element-right-sidebar').addClass('sidebar-fixed');
    } else {
      $('.element-right-sidebar').removeClass('sidebar-fixed');
    }

    if ($(window).scrollTop() + $(window).height() > $(document).height() - 590) {
      $('.element-right-sidebar').addClass('right-sidebar-absolute').removeClass('sidebar-fixed');
    } else {
      $('.element-right-sidebar').removeClass('right-sidebar-absolute');
    }
  });

  // Form Search
  $('.navbar-search .search-link').on('click', function () {
    $('.navbar-search .navbar-search-form').toggleClass('active');
  });

  // favourite-icon
  $('.favourite-icon .icon').on('click', function () {
    $(this).find('i').toggleClass('fa-heart-o');
    $(this).find('i').toggleClass('fa-heart');
  });

  //============================== ICON TOGGLER ANIMATION =========================
  $('.btn-search').on('click', function(e){
    e.preventDefault();
    $('.search-box').toggleClass('visible');
  });

  var toolbarToggle = $('.icon-toggle');
  function closeToolBox() {
    toolbarToggle.removeClass('active');
  }
  toolbarToggle.on('click', function(e) {
    var currentValue = $(this).attr('href');
    if($(e.target).is('.active')) {
      closeToolBox();
    } else {
      closeToolBox();
      $(this).addClass('active');
    }
    e.preventDefault();
  });

  /*======== 12. MAP ========*/
  function initialize() {
    var myLatLng = { lat: 40.697488, lng: -73.979681 };

    //Custom Style
    var styles = [];
    var mapOptions = {
      zoom: 14,
      scrollwheel: false,
      center: new google.maps.LatLng(40.697488, -73.979681),
      styles: styles

    };
    var map = new google.maps.Map(document.getElementById('map'), mapOptions);
    var image = '';
    var marker = new google.maps.Marker({});
  }
  var mapId = $('#map');
  if (mapId.length) {
    google.maps.event.addDomListener(window, 'load', initialize);
  }

})(jQuery);
