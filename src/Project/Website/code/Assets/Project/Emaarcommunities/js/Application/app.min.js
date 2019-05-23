'use strict';

/* To get JSON DATA  */
var getData = (function ($) {

    var spinner = '<div class="spinner"><div class="lds-ripple"><div></div><div></div></div></div>';

    var _fancyboxImage = function () {
        if ($('.fancyimages').length) {
            $(".fancyimages").fancybox({
                openEffect: "none",
                closeEffect: "none",

                helpers: {
                    title: {
                        type: 'inside'
                    }
                }
            });
        }

        if ($('.various').length) {
            $(".various").fancybox({
                fitToView: !1,
                autoSize: !1,
                closeClick: !1,
                openEffect: "none",
                closeEffect: "none",
                width: "100%",
                height: "100%",
                wrapCSS: "fullscreen-overlay"
            });
        }

    }
    // To get the Forum tabs data and populate it accordingly
    var _tabs = function (url, tabClass, tabContentClass, pagination) {

        // For loading
        $("#tabs-head-result").html(spinner);

        // To get Data
        $.get(url, function (data) {
            if (data.error == null) {

                var source = $("#tabs-head").html(),
                    template = Handlebars.compile(source),
                    html = template(data.Result);
                $("#tabs-head-result").html(html);


                var source2 = $("#tabs-content").html(),
                    template2 = Handlebars.compile(source2),
                    html2 = template2(data.Result);
                $("#tabs-content-result").html(html2);

                var tabIndex = $(tabClass).find(".active").attr("data-tab-index");
                $(tabContentClass).hide();
                $(tabContentClass).each(function () {
                    if ($(this).attr("data-tab") == tabIndex) {
                        $(this).fadeIn();
                    }
                })

                $(tabClass).find("li").click(function () {
                    if ($(this).hasClass("disbaled")) {
                        return false
                    }
                    $(this).addClass("active").siblings().removeClass("active");
                    var tabIndex = $(this).attr("data-tab-index");
                    $(tabContentClass).hide();
                    $(tabContentClass).each(function () {
                        if ($(this).attr("data-tab") == tabIndex) {
                            $(this).fadeIn();
                        }
                    })
                })


            } else { }
        })
    }

    var _results = function (url, pagination, dataparam) {
        var loadID = ".loadmore",
            tempDiv = "#filter-template",
            galleryTemp = "#gallery-template",
            errorTemp = "#error-template",
            resultDiv = "#filter-template-result",
            errMessage = $('#templateInitializor').data('error-message'),
            pageNumber = $('.loadmore').attr('data-pagenumber');
        var dataPageSize = $('#templateInitializor').data('page-size');

        $("#filter-template-result").html(spinner);
        pageNumber = pagination ? pagination : pageNumber;
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(dataparam),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                if (data.results.results != null && data.results.results.length > 0) {
                    var source2 = $(galleryTemp).html(),
                        template2 = Handlebars.compile(source2);

                    $('.spinner').remove();
                    for (var j = 0; dataPageSize > j && j <= data.results.Totalcount && j < data.results.results.length; j++) {
                        $(resultDiv).append(template2(data.results.results[j]));
                    }

                    if (data.results != null && (dataPageSize * (parseInt(pageNumber) + 1)) < data.results.Totalcount) {
                        $(".loadmore").css({ 'display': 'inline-block' });
                    } else {
                        $(".loadmore").hide();
                    }
                    _fancyboxImage();
                } else {
                    $('.spinner').remove();
                    var source = $(tempDiv).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    if (!$('.select2').length) {
                        $('.selectFilters').html(html);
                    }

                    if ($('.selectFilters .js-example-basic-single').length) {
                        $('.js-example-basic-single').select2();
                    }

                    var source = $(errorTemp).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    $(resultDiv).append(template(errMessage));
                    $(".loadmore").hide();
                }
            },
            error: function (error) {
                // console.log(error);
                $(".loadmore").hide();
            }
        });
    }

    var _resultsLoad = function (url, pagination, dataparam) {
        var loadID = ".loadmore",
            tempDiv = "#filter-template",
            galleryTemp = "#gallery-template",
            errorTemp = "#error-template",
            resultDiv = "#filter-template-result",
            pageNumber = $('.loadmore').attr('data-pagenumber');
        var dataPageSize = $('#templateInitializor').data('page-size');

        $("#filter-template-result").append(spinner);
        pageNumber = pagination ? pagination : pageNumber;
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(dataparam),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                if (data.results.results != null && data.results.results.length > 0) {
                    var source2 = $(galleryTemp).html(),
                        template2 = Handlebars.compile(source2);

                    $('.spinner').remove();

                    for (var j = 0; j < data.results.Totalcount && j < data.results.results.length; j++) {
                        $(resultDiv).append(template2(data.results.results[j]));
                    }
                    _fancyboxImage();
                    if (data.results != null && (dataPageSize * (parseInt(pageNumber) + 1)) < data.results.Totalcount) {
                        $(".loadmore").css({ 'display': 'inline-block' });
                    } else {
                        $(".loadmore").hide();
                    }

                } else {
                    $('.spinner').remove();
                    var source = $(tempDiv).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    if (!$('.select2').length) {
                        $('.selectFilters').html(html);
                    }

                    if ($('.selectFilters .js-example-basic-single').length) {
                        $('.js-example-basic-single').select2();
                    }

                    var source = $(errorTemp).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    $(resultDiv).append(template(data.ErrorMessage));
                    $(".loadmore").hide();
                }
            },
            error: function (error) {
                // console.log(error);
                $(".loadmore").hide();
            }
        });
    }

    var _filter = function (url, pagination, dataparam) {
        // For loading
        var loadID = ".loadmore",
            tempDiv = "#filter-template",
            galleryTemp = "#gallery-template",
            sectionTitleTemp = "#search-header",
            errorTemp = "#error-template",
            resultDiv = "#filter-template-result",
            dataPageSize = $('#templateInitializor').data('page-size'),
            errMessage = $('#templateInitializor').data('error-message'),
            pageNumber = $('.loadmore').data('pagenumber');
        $("#filter-template-result").html(spinner);
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(dataparam),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                if (data.results.results != null && data.results.results.length > 0) {
                    if ($(tempDiv).length) {
                        var source = $(tempDiv).html(),
                            template = Handlebars.compile(source),
                            html = template(data.filters);
                        if (!$('.select2').length) {
                            $('.selectFilters').html(html);
                        }

                        if ($('.selectFilters .js-example-basic-single').length) {
                            $('.js-example-basic-single').select2();
                        }
                    }

                    var source2 = $(galleryTemp).html(),
                        template2 = Handlebars.compile(source2);
                    $('.spinner').remove();

                    for (var j = 0; j < data.results.Totalcount && j < dataPageSize && j < data.results.results.length; j++) {
                        $(resultDiv).append(template2(data.results.results[j]));
                    }

                    if (data.results != null && (dataPageSize * (pageNumber + 1)) < data.results.Totalcount) {
                        $(".loadmore").css({ 'display': 'inline-block' });
                        // $(".loadmore").attr({ "data-pagenumber": 1 });
                        $(".loadmore").attr({ "data-count": data.results.Totalcount });
                    } else {
                        $(".loadmore").hide();
                    }
                    filterOnChange();
                    _fancyboxImage();

                    if ($('.search-section').length) {
                        var source3 = $(sectionTitleTemp).html(),
                            template3 = Handlebars.compile(source3);
                        $('.section-header').append(template3(data.results.Totalcount));
                    }
                    if (pagenumber == 0 && $('#templateInitializor').data('itemyearname') != "") {
                        $('#select2-customyear-container').text($('#templateInitializor').data('itemyearname'));
                    }

                } else {
                    $('.spinner').remove();
                    var source = $(tempDiv).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    if (!$('.select2').length) {
                        $('.selectFilters').html(html);
                    }

                    if ($('.selectFilters .js-example-basic-single').length) {
                        $('.js-example-basic-single').select2();
                    }

                    var source = $(errorTemp).html(),
                        template = Handlebars.compile(source),
                        html = template(data.filters);
                    $(resultDiv).append(template(errMessage));
                    $(".loadmore").hide();
                    filterOnChange();

                    if (pagenumber == 0 && $('#templateInitializor').data('itemyearname') != "") {
                        $('#select2-customyear-container').text($('#templateInitializor').data('itemyearname'));
                    }
                }
            },
            error: function (error) {
                // console.log(error);
                $(".loadmore").hide();
            }
        });
    }


    var _svg = function () {
        $("img.svg").each(function () {
            var $img = $(this);
            var imgID = $img.attr('id');
            var imgClass = $img.attr('class');
            var imgURL = $img.attr('src');
            $.get(imgURL, function (data) {
                // Get the SVG tag, ignore the rest
                var $svg = $(data).find('svg');
                // Add replaced image's ID to the new SVG
                if (typeof imgID !== 'undefined') {
                    $svg = $svg.attr('id', imgID);
                }
                // Add replaced image's classes to the new SVG
                if (typeof imgClass !== 'undefined') {
                    $svg = $svg.attr('class', imgClass + ' replaced-svg');
                }
                // Remove any invalid XML tags as per http://validator.w3.org
                $svg = $svg.removeAttr('xmlns:a');
                // Replace image with new SVG
                $img.replaceWith($svg);
            }, 'xml');
        });
    }

    var _dot = function () {
        $(".dotdotdot").dotdotdot({
            ellipsis: '... '
        });
    }

    return {
        // To Init
        tabs: function (url, tabClass, tabContentClass, pagination) {
            _tabs(url, tabClass, tabContentClass, pagination);
        },

        // To Init
        filter: function (url, pagination, loadmoreID, templateID, resultId) {
            _filter(url, pagination, loadmoreID, templateID, resultId);
        },

        results: function (url, pagination, loadmoreID, templateID, resultId) {
            _results(url, pagination, loadmoreID, templateID, resultId);
        },

        resultsLoad: function (url, pagination, loadmoreID, templateID, resultId) {
            _resultsLoad(url, pagination, loadmoreID, templateID, resultId);
        },

        svg: function () {
            _svg();
        },

        dot: function () {
            _dot();
        }
    };
})(jQuery);