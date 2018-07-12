/// <reference path="../libs/jquery-3.1.0.min.js" />
/// <reference path="Zegar.js" />
/// <reference path="Ajax.js" />
/// <reference path="UI.js" />
/// <reference path="Database.js" />
/// <reference path="Settings.js" />
var Database = {

    methods: {

        //utworzenie tabeli, przesyłam obiekt do wysłania Ajaxem

        addTabela: function (obj) {
            return Ajax.send(obj, Settings.urls.databaseUrl)
        },
        delTabela: function (obj) {
            return Ajax.send(obj, Settings.urls.databaseUrl)
        },
        addDane: function (obj) {
            return Ajax.send(obj, Settings.urls.databaseUrl)
        },
        delDane: function (obj) {
            return Ajax.send(obj, Settings.urls.databaseUrl)
        },
       getDane: function (obj) {
            return Ajax.send(obj, Settings.urls.jsonUrl)
       },

      sendDane: function (obj) {
          return Ajax.send(obj, Settings.urls.databaseUrl)
      },
      userLogIn: function (obj) {
          return Ajax.send(obj, Settings.urls.registerUrl)
      },

    }
}