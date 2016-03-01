var mapRef
var infoWindow;
var geocoder;
var controles;
$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}
$.parseObject = function(arrayForm)
{
    obj = {};
    $.each(arrayForm, function (index, o) {
        o.value == null ? '' : o.value;
        if (!obj[o.name] == null) {
            if (!obj[o.name].push) {
                obj[o.name] = [obj[o.name]];
            }
            obj[o.name].push(o.value);
        }
        else {
            obj[o.name] = o.value;
        }
    });
    return obj;
}
var MapControlx=function() {
    MapControlx.prototype.Init = function (posx, posy, zoom) {

        var mapOptions = {
            zoom: zoom,
            center: new google.maps.LatLng(posx, posy)
        };
        mapRef = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        new google.maps.Marker(
            {
                map: mapRef,
                position: mapRef.getCenter(),
                icon: {
                    path: google.maps.SymbolPath.CIRCLE,
                    scale: 6,
                    fillColor: '#0040FF',
                    strokeColor: "#81BEF7",
                    fillOpacity: 0.9,
                    strokeOpacity:0.7,
                    shape: { coord: [posx, posy, 30], type: 'circle' }

                }
            });
        infoWindow = new google.maps.InfoWindow();
        geocoder = new google.maps.Geocoder();
        mapRef.addListener("rightclick", function (position) {
            getPosition = position.latLng;
            $("#formdialog").dialog("open");
            $("#lat").val(getPosition.lat());
            $("#lng").val(getPosition.lng());
            geocoder.geocode({ 'location': getPosition }, function (result, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (result[0]) {
                        $.each(result[0].address_components, function (index, o) {
                            if (o.types[0] == "street_number") {
                                $("#number").val(o.long_name);
                            }
                            if (o.types[0] == "route") {
                                $("#street").val(o.long_name);
                            }

                            if (o.types[0] == "locality") {
                                $("#city").val(o.long_name);
                            }
                            if (o.types[0] == "country") {
                                $("#country").val(o.long_name);
                            }

                        });
                    }
                }
            });


        });
    };
    MapControlx.prototype.addMarket = function (posx, posy) {
        var marker=new google.maps.Marker(
            {
                map: mapRef,
                position:new google.maps.LatLng(posx,posy)
            });
        marker.addListener("click", function () {
            var thisMarker = this;
            geocoder.geocode({ 'location': thisMarker.getPosition() }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        infoWindow.setContent(thisMarker.getPosition().lat() + "<br/>" + thisMarker.getPosition().lng() + "<hr/>" + results[1].formatted_address);
                        infoWindow.open(mapRef, thisMarker);
                    }
                }
                else {
                    infoWindow.setContent(thisMarker.getPosition().lat() + "<br/>" + thisMarker.getPosition().lng());
                    infoWindow.open(mapRef, thisMarker);
                }
            });
        });
    };
    MapControlx.prototype.loadBussiness = function () {
        $.ajax(
            {
                url: "/api/MapApi/",
                method: "get",
                contentType: "application/json",
                success: function (Response) {
                    $.each(Response, function (index, o) {
                        var marker = new google.maps.Marker(
                        {
                            map: mapRef,
                            position: new google.maps.LatLng(o.lat, o.lng),
                            title: o.name,
                            type: o.businessType,
                            businessid: o.businessid,
                            draggable:true
                        });

                        marker.addListener("click", function () {
                            var thisMarkr = this;
                            $.ajax(
                                {
                                    url: "/api/MapApi/"+this.businessid,
                                    method: "get",
                                    contentType: "application/json",
                                    success: function (Response) {
                                        infoWindow.setContent("<div class='infowindow'><table class='table'><tr><th colspan='2'>" + Response.name + "</th></tr><tr><th>Número:</th><td>" + Response.address.number + "</td></tr><tr><th>Calle:</th><td>" + Response.address.street + "</td></tr><tr><th>Ciudad:</th><td>" + Response.address.city + "</td></tr><tr><th>País:</th><td>" + Response.address.country + "</td></tr></table></div>");
                                        infoWindow.open(mapRef, thisMarkr);
                                    },
                                    error: function (err, msg) {
                                        alert(err.responseText);
                                    }
                                });
                        });

                        marker.addListener("dragend", function () {
                            
                        });
                    });
                },
                error: function (err,msg) {
                    alert(err.responseText);
                }
            });
    }
    MapControlx.prototype.setMapType = function (mapType) {
        var maptype;
        switch (mapType) {
            case 1:
                maptype = google.maps.MapTypeId.HYBRID;
                break;
            case 2:
                maptype = google.maps.MapTypeId.ROADMAP;
                break;
            case 3:
                maptype = google.maps.MapTypeId.SATELLITE;
                break;
            case 4:
                maptype = google.maps.MapTypeId.TERRAIN;
                break;
            default:
                maptype = google.maps.MapTypeId.ROADMAP;
                break;
        }
        if (maptype != null) {
            mapRef.setMapTypeId(maptype);
        }
    }

    
};
$(function () {
    controles = new MapControlx();
    var zoom = $.urlParam("zoom") == null ? 15 : parseInt($.urlParam("zoom"));
    var mapType = $.urlParam("type") == null ? 2 : parseInt($.urlParam("type"));
    geocoder = new google.maps.Geocoder();

    if ($.urlParam("lat") != null && $.urlParam("lng") != null) {
        
        controles.Init($.urlParam("lat"), $.urlParam("lng"),zoom);
        controles.loadBussiness();
        controles.setMapType(mapType);
    }
    else if ($.urlParam("ciudad") != null) {
        geocoder.geocode({ 'address': $.urlParam("ciudad") }, function (result,status) {
            if (status === google.maps.GeocoderStatus.OK) {
                controles.Init(result[0].geometry.location.lat(), result[0].geometry.location.lng(), zoom);
                controles.loadBussiness();
                controles.setMapType(mapType);
            }
        });
    }
    else
    {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (objPosition) {
                controles.Init(objPosition.coords.latitude, objPosition.coords.longitude, 15);
                controles.loadBussiness();
                controles.setMapType(mapType);
            });
        }
    }
    
    $("#formdialog").dialog({
        autoOpen:false,
        dialogClass: "no-close",
        buttons: [
            {
                text: "Guardar",
                click: function () {                    
                    alert(JSON.stringify($.parseObject($("#formdialog > form").serializeArray())));
                },
                icons: {primary:"ui-icon-heart"}
            },
            {
                text: "Cerrar",
                click: function () {
                    $(this).dialog("close")
                }
            }
        ],
        closeOnEscape: false,
        title: "Nuevo Comercio",
        modal:true
    });

});