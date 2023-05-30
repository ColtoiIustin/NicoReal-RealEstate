//details

function initMapForDetails() {

    let myLatlng = { lat: parseFloat($("#latitude").text()), lng: parseFloat($("#longitude").text()) };

    if ($("#latitude").text() == "" && $("#longitude").text() == "") {
        $("#mapForDetails").remove();
        return;
    }

    let circle;

    //// MAP ////
    const map = new google.maps.Map(document.getElementById("mapForDetails"), {
        zoom: 17,
        center: myLatlng,
    });

    const center = map.getCenter();
    drawCircle(center);

    function drawCircle(center) {
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