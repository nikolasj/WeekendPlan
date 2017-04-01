var myMap,
    myPlacemark;

function init(la,lat,content,description) {
    myMap = new ymaps.Map("map", {
        center: [la, lat],
        zoom: 7
    });

    myPlacemark = new ymaps.Placemark([la, lat], {
        hintContent: "<strong>"+content+"</strong>",
        balloonContent: description
    });
       
    myMap.geoObjects.add(myPlacemark);    
}

function init_many(la, lat, content, description, array_coords) {

   
    myMap = new ymaps.Map("map", {
        center: [la, lat],
        zoom: 7
    });

    var points = [];
    points.push("Москва, Красная площадь");

    $.ajax({
        url: "/Opportunity/GetPlaceCoords",
        type: "GET",
        data: { leagueId: 5 },
        success: function (data) {
            // alert(JSON.stringify(data)); //how entire object in JSON format
            $.each(data, function (i, obj) {
                //alert(obj);
                myPlacemark = new ymaps.Placemark([obj.Lon, obj.Lat], {
                    hintContent: "<strong>" + obj.Content + "</strong>",
                    balloonContent: obj.Description
                });
                points.push([obj.Lon, obj.Lat]);
                myMap.geoObjects.add(myPlacemark);
            });

            var multiRouteModel = new ymaps.multiRouter.MultiRouteModel(points);
            multiRouteModel.setParams({ routingMode: 'masstransit' }, true); 

            ymaps.modules.require([
            'MultiRouteCustomView'
            ], function (MultiRouteCustomView) {
                // Создаем экземпляр текстового отображения модели мультимаршрута.
                // см. файл custom_view.js
                new MultiRouteCustomView(multiRouteModel);
            });

            var multiRoute = new ymaps.multiRouter.MultiRoute(multiRouteModel, {
                // Автоматически устанавливать границы карты так, чтобы маршрут был виден целиком.
                boundsAutoApply: true
            });

            // Добавляем мультимаршрут на карту.
            myMap.geoObjects.add(multiRoute);
        }
    });

   

    


    
}