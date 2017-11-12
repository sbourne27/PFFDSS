


function BloodDonors() { }

BloodDonors.dragShape = null;
BloodDonors.dragPixel = null;
BloodDonors.MapDivId = 'theMap6';
BloodDonors._map = null;
BloodDonors._points = [];
BloodDonors._shapes = [];
BloodDonors.ipInfoDbKey = '';

//document.write('<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>');
//document.write('<script src="http://dev.virtualearth.net/mapcontrol/v3/mapcontrol.js"></script>');
//document.write('<script src="http://openlayers.org/api/OpenLayers.js"></script>');

document.write('<script src="http://openlayers.org/en/v3.14.2/build/ol.js"></script>');
document.write('<script src="https://code.jquery.com/jquery-1.11.2.min.js"></script>');

//document.write('link rel="stylesheet" href="http://openlayers.org/en/v3.14.2/css/ol.css" type="text/css"');


//document.write('<script type="text/javascript" src="http://serverapi.arcgisonline.com/jsapi/ve/?v=1.4"></script>');

BloodDonors.LoadMap = function (latitude, longitude, onMapLoaded) {



    //var map = BloodDonors._map;

    //map = new VEMap(BloodDonors.MapDivId);

    //var options = new VEMapOptions();

    //options.EnableBirdseye = false

    //map.SetDashboardSize(VEDashboardSize.Normal);

    //map.AttachEvent("onclick", MouseHandler);

    //function MouseHandler(e) {
    //    var msg = "Mouse: ";
    //    msg += e.eventName;
    //    if (e.eventName == "onclick") {
    //        if (e.leftMouseButton)
    //            msg += "left";
    //        else if (e.rightMouseButton)
    //            msg += "right";
    //        else
    //            msg += "middle";;
    //    }

    //    msg += " Clicked at ";
    //    msg += "X:" + e.mapX + "  Y:" + e.mapY + " with ";
    //    msg += " Shift: " + e.shiftKey;
    //    msg += " Ctrl: " + e.ctrlKey;
    //    msg += " Alt: " + e.altKey;
    //    if (e.elementID != null) { //Gets in when an element generates an event
    //        msg += e.elementID + "<br>";
    //        var shape = map.GetShapeByID(e.elementID);
    //        msg += "Title: " + shape.GetTitle() + " and ";
    //        msg += "Description: " + shape.GetDescription() + "<br>";
    //    }
    //    else
    //        msg += "map";

    //    // Retrieve the pixel position of the cursor  
    //    var pix = new VEPixel(e.mapX, e.mapY);
    //    // Convert the pixel location to latitude / longitude  
    //    var pos = map.PixelToLatLong(pix);

    //    document.getElementById('SiteLatitude').value = pos.Latitude;
    //    document.getElementById('SiteLongitude').value = pos.Longitude;

    //   // alert(msg);
    //}

    //if (onMapLoaded != null)
    //    map.onLoadMap = onMapLoaded;

    //if (latitude != null && longitude != null) {
    //    var center = new VELatLong(latitude, longitude);
    //}

    //map.LoadMap(center, null, null, null, null, null, null, options);


    //var agisve_services = null;
    ////var tileUrl = "http://services2.arcgis.com/sFujhckYoxsqnpfe/arcgis/rest/services/PFFDSS_Base/FeatureServer";
    //var tileUrl = "http://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer";

    //agisve_services = new ESRI.ArcGIS.VE.ArcGISLayerFactory();
    //agisve_services.CreateLayer(tileUrl, "", GetMap);

    //function GetMap(tileSourceSpec, resourceInfo) {
    //    tileSourceSpec.MinZoom = 10;
    //    tileSourceSpec.Opacity = 0.5;
    //    map.AddTileLayer(tileSourceSpec, true);

    //    debugger;
    //    //$get("addMapButton").disabled = true;
    //}




    //var fromProjection = new ol.Projection("EPSG:4326");   // Transform from WGS 1984
    //var toProjection = new ol.Projection("EPSG:900913"); // to Spherical Mercator Projection

    //var map = new ol.Map(document.getElementById(BloodDonors.MapDivId));

    //var serviceUrl = 'http://services2.arcgis.com/sFujhckYoxsqnpfe/arcgis/rest/services/' + 'PFFDSS_Base/FeatureServer/';

    var serviceUrl = 'http://services.arcgis.com/rOo16HdIMeOBI4Mb/arcgis/rest/' +
          'services/PDX_Pedestrian_Districts/FeatureServer/';

    var layer = '0';
    var esrijsonFormat = new ol.format.EsriJSON();

    var styleCache = {
        'ABANDONED': new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'rgba(225, 225, 225, 255)'
            }),
            stroke: new ol.style.Stroke({
                color: 'rgba(0, 0, 0, 255)',
                width: 0.4
            })
        }),
        'GAS': new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'rgba(255, 0, 0, 255)'
            }),
            stroke: new ol.style.Stroke({
                color: 'rgba(110, 110, 110, 255)',
                width: 0.4
            })
        }),
        'OIL': new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'rgba(56, 168, 0, 255)'
            }),
            stroke: new ol.style.Stroke({
                color: 'rgba(110, 110, 110, 255)',
                width: 0
            })
        }),
        'OILGAS': new ol.style.Style({
            fill: new ol.style.Fill({
                color: 'rgba(168, 112, 0, 255)'
            }),
            stroke: new ol.style.Stroke({
                color: 'rgba(110, 110, 110, 255)',
                width: 0.4
            })
        })
    };

    var vectorSource = new ol.source.Vector({
        loader: function (extent, resolution, projection) {
            var url = serviceUrl + layer + '/query/?f=json&' +
                'returnGeometry=true&spatialRel=esriSpatialRelIntersects&geometry=' +
                encodeURIComponent('{"xmin":' + extent[0] + ',"ymin":' +
                    extent[1] + ',"xmax":' + extent[2] + ',"ymax":' + extent[3] +
                    ',"spatialReference":{"wkid":102100}}') +
                '&geometryType=esriGeometryEnvelope&inSR=102100&outFields=*' +
                '&outSR=102100';
            $.ajax({
                url: url, dataType: 'jsonp', success: function (response) {
                    if (response.error) {
                        alert(response.error.message + '\n' +
                            response.error.details.join('\n'));
                    } else {
                        // dataProjection will be read from document
                        var features = esrijsonFormat.readFeatures(response, {
                            featureProjection: projection
                        });
                        if (features.length > 0) {
                            vectorSource.addFeatures(features);
                        }
                    }
                }
            });
        },
        strategy: ol.loadingstrategy.tile(ol.tilegrid.createXYZ({
            tileSize: 512
        }))
    });

    var vector = new ol.layer.Vector({
        source: vectorSource,
        style: function (feature) {
            var classify = feature.get('activeprod');
            return styleCache[classify];
        }
    });


    var attribution = new ol.Attribution({
        html: 'Tiles &copy; <a href="http://services.arcgisonline.com/ArcGIS/' +
            'rest/services/World_Topo_Map/MapServer">ArcGIS</a>'
    });

    var raster = new ol.layer.Tile({
        source: new ol.source.XYZ({
            attributions: [attribution],
            url: 'http://server.arcgisonline.com/ArcGIS/rest/services/' +
                'World_Topo_Map/MapServer/tile/{z}/{y}/{x}'
        })
    });

    var map = new ol.Map({
        layers: [raster, vector],
        target: document.getElementById('theMap6'),
        view: new ol.View({
            center: ol.proj.transform([-97.6114, 38.8403], 'EPSG:4326', 'EPSG:3857'),
            zoom: 7
        })
    });

    var displayFeatureInfo = function (pixel) {
        var features = [];
        map.forEachFeatureAtPixel(pixel, function (feature) {
            features.push(feature);
        });
        if (features.length > 0) {
            var info = [];
            var i, ii;
            for (i = 0, ii = features.length; i < ii; ++i) {
                info.push(features[i].get('field_name'));
            }
            document.getElementById('info').innerHTML = info.join(', ') || '(unknown)';
            map.getTarget().style.cursor = 'pointer';
        } else {
            if (document.getElementById('info') != null) {
                document.getElementById('info').innerHTML = 'Nothing';
            }
            map.getTarget().style.cursor = '';
        }
    };

    map.on('pointermove', function (evt) {
        if (evt.dragging) {
            return;
        }
        var pixel = map.getEventPixel(evt.originalEvent);
        displayFeatureInfo(pixel);
    });

    map.on('click', function (evt) {
        displayFeatureInfo(evt.pixel);
    });


    //var osmlayer = new ol.Layer.OSM("Simple OSM Map");
    //map.addLayer(osmlayer);

    //map.zoomtoMaxExtent;


    //var wms = new OpenLayers.Layer.WMS("NASA Global Mosaic", "http://wms.jpl.nasa.gov/wms.cgi", { layers: "modis,global_mosaic" });
    //map.addLayer(wms);

    //map.addControl(new OpenLayers.Control.LayerSwitcher());
    //map.setCenter(new OpenLayers.LonLat(14.505787300000065, 35.8760686).transform(fromProjection, toProjection), 9);

    BloodDonors._map = map;

    //// Define the tile layer source
    //var tileSource = new Microsoft.Maps.TileSource({ uriConstructor: 'WMSHandler.ashx?q={quadkey}' });

    //// Construct the layer using the tile source
    //var tilelayer = new Microsoft.Maps.TileLayer({ mercator: tileSource, opacity: 0.9 });

    // Push the tile layer to the map
    //BloodDonors._map.entities.push(tilelayer);

    //function init() {

    //    debugger;




    //}

}



BloodDonors.EnableMapMouseClickCallback = function () {
    BloodDonors._map.AttachEvent("onmousedown", BloodDonors.onMouseDown);
    BloodDonors._map.AttachEvent("onmouseup", BloodDonors.onMouseUp);
    BloodDonors._map.AttachEvent("onmousemove", BloodDonors.onMouseMove);
}

BloodDonors.onMouseDown = function (e) {
    if (e.elementID != null) {
        BloodDonors.dragShape = BloodDonors._map.GetShapeByID(e.elementID);
        return true;
    }
}

BloodDonors.onMouseUp = function (e) {
    if (BloodDonors.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        BloodDonors.dragPixel = new VEPixel(x, y);
        var LatLong = BloodDonors._map.PixelToLatLong(BloodDonors.dragPixel);
        $("#Latitude").val(LatLong.Latitude.toString());
        $("#Longitude").val(LatLong.Longitude.toString());
        BloodDonors.dragShape = null;

        BloodDonors._map.FindLocations(LatLong, BloodDonors.getLocationResults);
    }
}

BloodDonors.onMouseMove = function (e) {
    if (BloodDonors.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        BloodDonors.dragPixel = new VEPixel(x, y);
        var LatLong = BloodDonors._map.PixelToLatLong(BloodDonors.dragPixel);
        BloodDonors.dragShape.SetPoints(LatLong);
        return true;
    }
}

BloodDonors.onEndDrag = function (e) {
    $("#Latitude").val(e.LatLong.Latitude.toString());
    $("#Longitude").val(e.LatLong.Longitude.toString());
}

BloodDonors.ClearMap = function () {
    if (BloodDonors._map != null) {
        BloodDonors._map.Clear();
    }
    BloodDonors._points = [];
    BloodDonors._shapes = [];
}

BloodDonors.LoadPin = function (LL, name, description, draggable) {
    if (LL.Latitude == 0 || LL.Longitude == 0) {
        return;
    }

    var shape = new VEShape(VEShapeType.Pushpin, LL);

    if (draggable == true) {
        shape.Draggable = true;
        shape.onenddrag = BloodDonors.onEndDrag;
    }

    //Make a Pushpin with a title and description
    shape.SetTitle("<span class=\"pinTitle\"> " + escape(name) + "</span>");

    if (description !== undefined) {
        shape.SetDescription("<p class=\"pinDetails\">" + escape(description) + "</p>");
    }

    BloodDonors._map.AddShape(shape);
    BloodDonors._points.push(LL);
    BloodDonors._shapes.push(shape);
}

BloodDonors.FindAddressOnMap = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    BloodDonors._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, BloodDonors._callbackForLocation);
}

BloodDonors._callbackForLocation = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    BloodDonors.ClearMap();

    if (places == null) {
        BloodDonors._map.ShowMessage(VEErrorMessage);
        return;
    }

    //Make a pushpin for each place we find
    $.each(places, function (i, item) {
        var description = "";
        if (item.Description !== undefined) {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude,
                        item.LatLong.Longitude);

        BloodDonors.LoadPin(LL, item.Name, description, true);
    });

    //Make sure all pushpins are visible
    if (BloodDonors._points.length > 1) {
        BloodDonors._map.SetMapView(BloodDonors._points);
    }

    //If we've found exactly one place, that's our address.
    //lat/long precision was getting lost here with toLocaleString, changed to toString
    if (BloodDonors._points.length === 1) {
        $("#Latitude").val(BloodDonors._points[0].Latitude.toString());
        $("#Longitude").val(BloodDonors._points[0].Longitude.toString());
    }
}



BloodDonors._renderDonors = function (donors) {
   
    BloodDonors.ClearMap();

    $.each(donors, function (i, donor) {

        var LL = new VELatLong(donor.Latitude, donor.Longitude, 0, null);

        // Add Pin to Map
        BloodDonors.LoadPin(LL, donor.DonorID, donor.Description, false);

      
    });

    // Adjust zoom to display all the pins.
    if (BloodDonors._points.length > 1) {
        BloodDonors._map.SetMapView(BloodDonors._points);
    }

    // Display the event's pin-bubble on hover.
    $(".DonorsItem").each(function (i, Donors) {
        $(Donors).hover(
            function () { BloodDonors._map.ShowInfoBox(BloodDonors._shapes[i]); },
            function () { BloodDonors._map.HideInfoBox(BloodDonors._shapes[i]); }
        );
    });

}

BloodDonors.FindAddress = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    BloodDonors._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, BloodDonors._callbackUpdateMapDonors);

}

BloodDonors._callbackUpdateMapDonors = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    
    var center = BloodDonors._map.GetCenter();

    $.post("/Search/SearchByLocation",
           { latitude: center.Latitude, longitude: center.Longitude },
           BloodDonors._renderDonors,
           "json");
}