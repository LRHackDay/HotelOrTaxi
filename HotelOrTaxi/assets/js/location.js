$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successFunction, errorFunction);
    } else {
        alert('It seems like Geolocation, which is required for this page, is not enabled in your browser. Please use a browser which supports it.');
    }
});

function successFunction(position) {
    $('#fromlatlong').val(position.coords.latitude + ',' + position.coords.longitude);
}

function errorFunction(position) {
    alert("Geolocation error!");
}