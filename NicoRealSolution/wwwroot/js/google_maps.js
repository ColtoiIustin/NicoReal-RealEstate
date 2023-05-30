//custom Andrei
//create and edit

function initMap() {

    let myLatlng = { lat: parseFloat($("#latitude").val()), lng: parseFloat($("#longitude").val()) };
    let zoom = 16;

    if ($("#latitude").val() == "" && $("#longitude").val() == "") {
        myLatlng = { lat: 44.43341610898733, lng: 26.103434616556406 };
        zoom = 10;
    }

    let circle;

    //// AUTOCOMPLETE ////
    const options = {
        fields: ["geometry.location"],
    };

    const autocomplete = new google.maps.places.Autocomplete(
        document.getElementById("autocomplete"), options
    );

    // Event listener for place_changed event
    autocomplete.addListener("place_changed", function () {
        const place = autocomplete.getPlace();
        if (place.geometry) {
            map.setCenter(place.geometry.location);
            map.setZoom(16);
        }
    });

    //// MAP ////
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: zoom,
        center: myLatlng,
    });

    map.addListener("center_changed", () => {
        marker.setPosition(map.getCenter());

        //lat si long
        const center = map.getCenter();
        drawCircle(center);
        updateInputFields(center.lat(), center.lng());
    });

    //// MARKER ////
    const marker = new google.maps.Marker({
        position: myLatlng,
        map,
        title: "Click to zoom",
    });

    const center = map.getCenter();
    drawCircle(center);

    // Function to update input fields with latitude and longitude values
    function updateInputFields(latitude, longitude) {
        $("#latitude").val(latitude);
        $("#longitude").val(longitude);
    }

    function drawCircle(center) {
        if (circle) {
            circle.setMap(null); // Remove existing circle from the map
        }

        circle = new google.maps.Circle({
            map: map,
            center: center,
            radius: 100, // Radius in meters (200 meters diameter)
            strokeColor: "#FF0000",
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: "#FF0000",
            fillOpacity: 0.35,
        });
    }

}

