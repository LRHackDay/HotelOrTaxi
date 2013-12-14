$(document).ready(function () {
    var searchBoxHolder = $('.search-box-holder'),
            fromEle = $('#from');
    bindChangeClick(searchBoxHolder, fromEle);
    bindLocateClick(searchBoxHolder, fromEle);
    geoLocate();
});

function bindChangeClick(searchBoxHolder, fromEle) {
    $('#change-location').click(function () {
        toggleView(searchBoxHolder, fromEle, 'From (postcode, street, etc.)');
        fromEle.removeAttr('disabled');
        $('#fromlatlong').val('');
    });
}

function bindLocateClick(searchBoxHolder, fromEle) {
    $('#find-my-loc').click(function () {
        toggleView(searchBoxHolder, fromEle, 'Use current location');
        fromEle.attr('disabled', 'disabled');
        geoLocate();
    });
}

function geoLocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(bindLatLong, errorFunction);
    } else {
        alert('It seems like Geolocation, which is required for this page, is not enabled in your browser. Please use a browser which supports it.');
    }
}

function toggleView(searchBoxHolder, fromEle, placeholderText) {
    searchBoxHolder.toggleClass('s-on');
    searchBoxHolder.toggleClass('s-on-change');
    searchBoxHolder.toggleClass('s-on-find');
    fromEle.attr('placeholder', placeholderText);
}

function bindLatLong(position) {
    $('#fromlatlong').val(position.coords.latitude + ',' + position.coords.longitude);
}

function errorFunction(position) {

}

function attachValidation() {
    $('#fight-button').click(function (e) {
        var from = $('#fromlatlong').val(),
            to = $('#tolatlong').val();
        if (from.length === 0 || to.length === 0) {
            e.preventDefault();
            alert('Please enter your current destination and desired location');
        }
    });
}