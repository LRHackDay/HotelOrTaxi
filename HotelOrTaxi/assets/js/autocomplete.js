function initialize() {
    var from = document.getElementById('from');
    var to = document.getElementById('to');

    var autocompleteFrom = new google.maps.places.Autocomplete(from);
    var autocompleteTo = new google.maps.places.Autocomplete(to);

    var bounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(49.383639452689664, -17.39866406249996),
        new google.maps.LatLng(59.53530451232491, 8.968523437500039)
    );

    autocompleteFrom.setBounds(bounds);
    autocompleteTo.setBounds(bounds);

    google.maps.event.addListener(autocompleteFrom, 'place_changed', function () {
        var place = autocompleteFrom.getPlace();

        $('#from').val(place.name);
        $('#fromlatlong').val(place.geometry.location.lat() + ',' + place.geometry.location.lng());
    });
    
    google.maps.event.addListener(autocompleteTo, 'place_changed', function () {
        var place = autocompleteTo.getPlace();

        $('#to').val(place.name);
        $('#tolatlong').val(place.geometry.location.lat() + ',' + place.geometry.location.lng());
    });
}
google.maps.event.addDomListener(window, 'load', initialize);