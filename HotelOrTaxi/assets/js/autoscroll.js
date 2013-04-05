(function () {
    var scrollPage = function () {
        $.scrollTo(xyz);
    };
    var c = setTimeout(scrollPage, 35000);
    $(document.body).bind('scroll mousedown keydown', function () {
        clearTimeout(c);
    });
})();