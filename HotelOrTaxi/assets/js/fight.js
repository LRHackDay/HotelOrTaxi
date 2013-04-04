$(document).ready(function () {
    var from = getParameterByName('From');
    var to = getParameterByName('To');
    var fromlatlong = getParameterByName('fromlatlong');
    var tolatlong = getParameterByName('tolatlong');
    var searchUrl = '/results?From=' + from + '&To=' + to + '&fromlatlong=' + fromlatlong + '&tolatlong=' + tolatlong;

    $.get(searchUrl, function (data) {
        $('.modal-fight').replaceWith($(data).find('.search-results'));
        window.history.pushState({}, "Search Results", searchUrl);
    });
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}