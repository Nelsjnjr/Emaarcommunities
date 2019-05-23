'use strict';

// global Variables
var winWidth = $(window).width();
var mobileWidth = 767;
var tabletPWidth = 991;
var languageCode = $('body').attr("lang");

// page init
jQuery(function () {
    initSvgConvert();
    if (winWidth <= 992) {
        mobileMenuClick();
        // mobilehover();
        mapcenter();
    } else {
        desktopSearchTrigger();
        desktopMenu();
        $( window ).resize(function() {
            $('.mobileMenu').removeClass('mobileMenu');
            $('.hamburgers').removeClass('is-active');
            $('body').removeClass('overflow');
        });
    }
    
  
    mobilemenuTrigger();
    // statics();
    scrollTo();
    initResizeWindow();
    initMapPopups();
    exploringSlider();
    headerScroll();
    initSelect();
    fancyboxImages();
    initVideoFullscreen();
    initSideNavigation();
    //Forms Validation
    validatecontactUs('contactusform');
    initInputFormat();

    new WOW().init();
});

function isNullUndefinedOrWhiteSpace(str) {
    return str === undefined || str === null ? true : !/\S/.test(str);
}

function mapcenter() {
    
        var outerContent = $('.map-section-inner');
        var innerContent = $('.map-section-inner .map-section-frame');
       
        outerContent.scrollLeft( (innerContent.width() - outerContent.width()) / 2);

}

function headerScroll() {
    $(window).scroll(function(){
        var sticky = $('.siteHeader'),
            scroll = $(window).scrollTop();
      
        if (scroll >= 300) sticky.addClass('isFixed');
        else if (scroll <= 10) sticky.removeClass('isFixed');
    });
}

// Replace all SVG images with inline SVG
function initSvgConvert(){
    getData.svg();
} 

// banner counting numbers
function statics(){
    $('.running-counter').counterUp({ 
        delay: 50,
        time: 1000
    });
}


function initSideNavigation(){
    var getdata = $('.sideNavigation ul >li.active a').html();
    $('.placehold').html(getdata);
    // console.log('aya kuch' , getdata);

    $('.mobileAccordionHolder').click(function(){
        // $(this).next().toggleClass('pomla');
        $(this).toggleClass('active');
    })

}

// popups init
function initMapPopups() {
    jQuery('.popup-holder').contentPopup({
        mode: 'click',
        btnClose: '.close-ico, .btn-secondary'
    });
    jQuery('.map-item').contentPopup({
        mode: 'click',
        popup: '.map-item-popup',
        btnOpen: '.map-item-link',
        afterShow: function(){
        $('html').addClass('active-popup');
        },
        afterHide: function(){
        $('html').removeClass('active-popup');
        }
    });
}

//window resize 
function initResizeWindow(){
    // When window resize
    $(window).resize(function(){
        var winWidth = $(window).width();

        // If window width is greater then tablet(991) width
        if(winWidth > tabletPWidth ){
            $(".m-menu").removeClass("pakro");
            $(".snWrap").removeClass("open");
        } 
    })
}

// Desktop Search Trigger
function desktopSearchTrigger() {
    $('.sbtn').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        if ($this.hasClass('notini')) {
           
            $this.closest('form').submit();
        } else {
            $this.addClass('notini');
            $('.siteNavigation ul > li > a').fadeOut('fast')
            setTimeout(function () {
                $this.closest('.siteSearch').addClass('active');
                $(".searchinput").focus();
                $('.js-sOnTrigger').find(".sbtn").removeAttr("disabled");
            }, 300);
        }
    });

    $(document).on('click', function (e) {
        if ($(e.target).closest(".js-sOnTrigger").length === 0) {
            $('.sbtn').removeClass('notini');
            $('.siteSearch').removeClass('active');
            $(".searchinput").val('');
            setTimeout(function () {
                
                $('.siteNavigation ul > li > a').fadeIn('fast');
                // $('.js-sOnTrigger').find(".sbtn").addAttr("disabled");
            }, 300);
            // $('.js-sOnTrigger').find(".sbtn").attr("disabled", "disabled");
        }
    });

    $('.clsoebtn').on('click', function (e) {
            $('.sbtn').removeClass('notini');
            $('.siteSearch').removeClass('active');
            $(".searchinput").val('');
            setTimeout(function () {
                
                $('.siteNavigation ul > li > a').fadeIn('fast');
                // $('.js-sOnTrigger').find(".sbtn").addAttr("disabled");
            }, 300);
            // $('.js-sOnTrigger').find(".sbtn").attr("disabled", "disabled");
    });

    // var myEfficientFn = debounce(function () {
    //     // All the taxing stuff you do

    //     if ($(".searchinput").val() == '') {
    //         $(".sresult").hide();

    //     } else {
    //         $(".sresult").show();

    //     }
    // }, 250);

    // the user has stopped typing for 250ms.
    // $('.searchinput').keyup(myEfficientFn);
}

function mobilemenuTrigger() {
    $('.mobile-menu-trigger').on('click', function() {
        var $this = $(this);
        $this.toggleClass('is-active');
        $('.siteNavigation').toggleClass('mobileMenu');
        $('body, html').toggleClass('overflow');
    });
}


function mobileMenuClick() {
    $('.siteNavigation>ul>li a').on('click', function() {
        $(this).parent().toggleClass('active');
        $(this).parent().siblings().removeClass('active');
    })
}

function desktopMenu() {
    // $('.siteNavigation>ul>li a').hover(function(e) {
    //     // $(this).parent().addClass('active');
    //     // $(this).parent().siblings().removeClass('active');
    //     e.stopPropagation();
    // }, function(e){
    //     $(this).parent().removeClass('active');
    //     e.stopPropagation();
    // });
}

function scrollTo() {
    $(".scrollIndicator").click(function() {
        $('html, body').animate({
            scrollTop: $(".explore").offset().top - 75
        }, 2000);
    });
}

function exploringSlider(){
    var swiper = new Swiper('.exploreSlider .swiper-container', {
        slidesPerView: 4,
        spaceBetween: 30,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        breakpoints: {
            1200: {
                slidesPerView: 4,
                spaceBetween: 15,
            },
            1024: {
              slidesPerView: 3.4,
              spaceBetween: 20,
            },
            991: {
              slidesPerView: 2.4,
              spaceBetween: 20,
            },
            767: {
              slidesPerView: 1,
              spaceBetween: 15,
            }
        }
    });
}

function initSelect(){
    $('.js-example-basic-single-static').select2();
}

//fancybox Images
function fancyboxImages(){
    $(".fancyimages").fancybox({
        openEffect	: "none",
        closeEffect	: "none",

        helpers : {
    		title : {
    			type : 'inside'
    		}
    	}
    });
}

//Homepage video banner
function initVideoFullscreen(){
   
    $(".various").fancybox({
        fitToView:!1,
        autoSize:!1,
        closeClick:!1,
        openEffect:"none",
        closeEffect:"none",
        width:"100%",
        height:"100%",
        wrapCSS:"fullscreen-overlay"
    });

}

//Forms Validation starts Here
function addMethods(){
    // add the rule here
$.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
}, "Value must not equal arg.");
}


function initInputFormat() {
   
   

    if ($("body").find(".phone-number").length > 0) {
        $('.phone-number').each(function () {
            var field = $(this),
                textMask = field.attr("data-mask");
            if (textMask == undefined || textMask == '') {
                field.mask('Z##############', {
                    'translation': {
                        Z: { pattern: /[0-9+]/ }
                    }
                });
            }
        });
    }
}

function validatecontactUs(tb) {

    if($("#"+tb).length > 0) {
        addMethods();
        var validator = $("#" + tb).validate({
           
            highlight: function (element) {
                $(element).closest("div").addClass("field-error");
            },
            unhighlight: function (element) {
                $(element).closest("div").removeClass("field-error");
            },
            errorPlacement: function (error, element) {
                var placement = $(element).data('error');

                // For custom error placement
                if (placement) {
                    $(placement).append(error)
                } else {
                    error.insertAfter(element);
                }
            }
        });
    }
}
//Loop through all page anchor tag and set target=_blank if the domain is not same as current domain.
$('a').each(function () {
    //comparing the site domain with anchor tag url domain.
    if (this.host.toString() != location.host.toString()) {
        $(this).attr('target', '_blank');
    }
});
//setting mailto link to be in target "_self"
$('a[href^=mailto]').attr('target', '_self');