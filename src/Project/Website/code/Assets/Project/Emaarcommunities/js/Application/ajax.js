// global Variables
var winWidth = $(window).width(),
    mobileWidth = 767,
    tabletPWidth = 991,
    languageCode = $('body').attr("lang"),
    pagenumber = $('.loadmore').data('pagenumber'),
    dataPageSize = $('#templateInitializor').data('page-size'),
    itemId = $('#templateInitializor').data('item-id'),
    listItemTemplateId = $('#templateInitializor').data('listitemtemplateid'),
    showFilters = $('#templateInitializor').data('show-filters'),
    total = dataPageSize,
    yearFolderItemName = $('#templateInitializor').data('itemyearname'),
    parentItemId = $('#templateInitializor').data('listparentitemid'),
    AjaxUrl = $('#templateInitializor').data('service-url');
    // https://jahanzebsabir.com/service/search/GetImageGalleryJson?pageSize=1&pageNumber=0&filters=”Years:4bde1ee9d4f57a05071a9f343de64|Albums:4bde1ee6659d4f57a05071a9f343de64”
    // page init
    jQuery(function () {
        getImageGallery();
        loadMore();
        searchInput();
    });

function isNullUndefinedOrWhiteSpace(str) {
    return str === undefined || str === null ? true : !/\S/.test(str);
}

function filterOnChange() {
    $('.js-example-basic-single').on('change', function () {
        $('.loadmore').attr('data-pagenumber', 0);
        pagenumber = $('.loadmore').data('pagenumber');
        getImageGalleryOnChange(0);
    });
}

function getImageGallery(pagination) {
    var parameterlabel = [],
        n = 0;
    AjaxUrl = $('#templateInitializor').data('service-url');

    var dataParam = { pageNumber: pagenumber, pageSize: dataPageSize, filter: '', itemId: itemId, listItemTemplateId: listItemTemplateId, showFilters: showFilters, parentItemId: parentItemId };

    // var JSONurl = AjaxUrl + "?" + yearLabel + "=" + year + "&" + albumLabel + "=" + albums;
    getData.filter(AjaxUrl, pagination, dataParam);

}

function searchInput() {
    $('#searchBox').on('submit', function (e) {
        e.preventDefault();
        var searchValue = $(this).find('.input-field').val(),
            AjaxUrl = $('#templateInitializor').data('service-url'),
            pagination = 0,
            dataParam = { pageNumber: pagenumber, pageSize: dataPageSize, filter: '', searchTerm: searchValue };
        getData.results(AjaxUrl, pagination, dataParam);
    })
}

function getImageGalleryOnChange(pagination) {
    var parameterlabel = [],
        n = 0,
        filterParam = '';
    AjaxUrl = $('#templateInitializor').data('service-url');

    $('.filters-support').each(function () {
        var $this = $(this);
        var $value = $this.val();
        parameterlabel[n] = { Label: $this.data('label'), Value: $value };
        n++;
    });

    // console.log(parameterlabel);

    if (parameterlabel.length) {
        for (index in parameterlabel) {
            var sep = index == 0 ? '' : '|';
            filterParam += sep + parameterlabel[index].Label + ':' + parameterlabel[index].Value;
        }
    }

    var chkPagi = pagination ? pagination : pagenumber;
    filterParam = {
        pageNumber: chkPagi,
        pageSize: dataPageSize,
        filter: filterParam,
        itemId: itemId,
        listItemTemplateId: listItemTemplateId,
        showFilters: showFilters,
        parentItemId: parentItemId
    };

    getData.results(AjaxUrl, pagination, filterParam);
}

function getImageGalleryOnLoad(pagination) {
    var parameterlabel = [],
        n = 0,
        filterParam = '';
    AjaxUrl = $('#templateInitializor').data('service-url');

    $('.filters-support').each(function () {
        var $this = $(this);
        var $value = $this.val();
        parameterlabel[n] = { Label: $this.data('label'), Value: $value };
        n++;
    });

    // console.log(parameterlabel);

    if (parameterlabel.length) {
        for (index in parameterlabel) {
            var sep = index == 0 ? '' : '|';
            filterParam += sep + parameterlabel[index].Label + ':' + parameterlabel[index].Value;
        }
    }

    var chkPagi = pagination ? pagination : pagenumber;
    filterParam = {
        pageNumber: chkPagi,
        pageSize: dataPageSize,
        filter: filterParam,
        itemId: itemId,
        listItemTemplateId: listItemTemplateId,
        showFilters: showFilters,
        parentItemId: parentItemId
    };

    getData.resultsLoad(AjaxUrl, pagination, filterParam);
}

function loadMore() {
    $('.loadmore').on('click', function () {
        var datatotalcount = $('.loadmore').data('count');
        if ((total * (pagenumber + 1)) < datatotalcount) {
            pagenumber += 1;
            $('.loadmore').attr('data-pagenumber', pagenumber);
            getImageGalleryOnLoad();
        }
    });
}