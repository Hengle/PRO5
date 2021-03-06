    studio.menu.addMenuItem({
    name: "Scripts\RenameMarkers",
    isEnabled: function () {
    var markers = studio.window.editorSelection();
    return markers.length > 0 ? true : false;
    },

    execute: function () {
        var markers = studio.window.editorSelection();

        var markerName = studio.system.getText("Marker Name", "");

        for (i = 0; i < markers.length; i++) {
            markers[i].name = markerName;
        }
    }
});