/// <reference path="../libs/jquery-3.1.0.min.js" />
/// <reference path="Zegar.js" />
/// <reference path="Database.js" />
/// <reference path="UI.js" />
/// <reference path="Ajax.js" />
/// <reference path="Settings.js" />
var Ajax = {
    send: function (obj, url, dataType) {
        return $.ajax({
            type: "POST",
            url: url,
            data: obj,
            dataType: dataType,

        })
    }
}

