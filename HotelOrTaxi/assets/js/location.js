$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successFunction, errorFunction);
    } else {
        alert('It seems like Geolocation, which is required for this page, is not enabled in your browser. Please use a browser which supports it.');
    }

    validateSearchForm();
});

function successFunction(position) {
    $('#fromlatlong').val(position.coords.latitude + ',' + position.coords.longitude);
}

function errorFunction(position) {
       
}

function validateSearchForm() {
    $('#fight-button').click(function (e) {
        var from = $('#from').val(),
            to = $('#to').val();
        if (from.length === 0 || to.length === 0) {
            e.preventDefault();
            alert('Please enter your current destination and desired location');
        }
    });
}