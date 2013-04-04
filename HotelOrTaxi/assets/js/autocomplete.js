function initialize() {
    var input = document.getElementById('to');
    var autocomplete = new google.maps.places.Autocomplete(input);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();

        alert(autocomplete.getBounds());

        $('to').val(place.name);
    });
}
google.maps.event.addDomListener(window, 'load', initialize);