var Control;
var infowindow = new google.maps.InfoWindow();
var mapControls=function (){

	var MapRef;
	var initialize=function(posx,posy,zoomp)
		{
			var mapOptions=
				{
					zoom:zoomp,
					center:new google.maps.LatLng(posx,posy)
				};

			MapRef=new google.maps.Map(document.getElementById("map-canvas"),mapOptions);

		};

	var addMarker=function(posx,posy,style)
	{
		var optionMarker;
			switch(style){

				case "currentLocation":
					optionMarker={
						position:new google.maps.LatLng(posx,posy),
						map:MapRef,
						title:"Mi Localización",
						icon:{
      						path: google.maps.SymbolPath.CIRCLE,
      						scale: 4,
      						strokeColor:"#1223ff"

   							},
   						animation: google.maps.Animation.DROP,
   						draggable:true
					}
				break;


				default:
				optionMarker={position:new google.maps.LatLng(posx,posy),map:MapRef}
				break;

			}


		var marker=new google.maps.Marker(optionMarker);
	};

	function addPlaces(place)
	{
	 	var marker=new google.maps.Marker({
			position:place.geometry.location,
			map:MapRef,
			animation: google.maps.Animation.DROP
		});

		google.maps.event.addListener(marker, 'click', function() {
			var content="<strong>"+place.name+"</strong><br/>Ubicación:"+place.vicinity+"<br/>";
			if(typeof(place.photos)!="undefined" && place.photos.length>0)
			{
				content+="<div align='center'>"
				for(var i=0;i<place.photos.length;i++)
					content+="<img src='"+place.photos[i].getUrl({maxWidth:200,maxHeight:150})+"' widh='"+place.photos[i].width+"' />";
				content+="</div>"
			}
		    infowindow.setContent(content);
		    infowindow.open(MapRef, this);
	  	});
	}

	var getPlaces=function(searchType){
		var request = {
    		location: MapRef.getCenter(),
    		radius: 5000,
    		keyword:searchType
  			};

  
  		var service = new google.maps.places.PlacesService(MapRef);
  		service.nearbySearch(request, callbackPlaces);
	}

	function callbackPlaces(results, status){
		if (status == google.maps.places.PlacesServiceStatus.OK) {
		    for (var i = 0; i < results.length; i++) {
		      addPlaces(results[i]);
		    }
	 	}
	}

	return {
		init:initialize,
		addMarker:addMarker,
		searchPlaces:getPlaces
	};
};

$(function(){

	if (navigator.geolocation) { /* Si el navegador tiene geolocalizacion */
		Control=mapControls();
        navigator.geolocation.getCurrentPosition(getPosition, errores);
    }
            else{
                alert('Oops! Tu navegador no soporta geolocalización. Bájate Chrome, que es gratis!');
            }	
});

function getPosition(position)
{
	var lat=position.coords.latitude;
	var lng=position.coords.longitude;
	Control.init(lat,lng,14);
	Control.addMarker(lat,lng,"currentLocation");
}

function errores(err) {
    /*Controlamos los posibles errores */
    if (err.code == 0) {
      alert("Oops! Algo ha salido mal");
    }
    if (err.code == 1) {
      alert("Oops! No has aceptado compartir tu posición");
    }
    if (err.code == 2) {
      alert("Oops! No se puede obtener la posición actual");
    }
    if (err.code == 3) {
      alert("Oops! Hemos superado el tiempo de espera");
    }
}