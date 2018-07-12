/// <reference path="../libs/jquery-3.1.0.min.js" />
/// <reference path="Database.js" />
/// <reference path="Zegar.js" />
/// <reference path="UI.js" />
/// <reference path="Ajax.js" />
/// <reference path="Settings.js" />
var main = {

    init: function () {
        //np inicjalizacja wyglądu elementów interfejsu
        //poszczególnych ekranów
        Captcha.init();
        //Ui.init();
    },
    rand:function (min, max) {
        min = parseInt(min, 10);
        max = parseInt(max, 10);

        if (min > max) {
            var tmp = min;
            min = max;
            max = tmp;
        }

        return Math.floor(Math.random() * (max - min + 1) + min);
    },
    mixArray:function (arr) {
        for (var i=0; i<arr.length; i++) { //wykonujemy pętlę po całej tablicy
            var j = Math.floor(Math.random() * arr.length); //losujemy wartość z przedziału 0 - tablica.length-1
            var temp = arr[i]; //pod zmienną temp podstawiamy wartość bieżącego indexu
            arr[i] = arr[j]; //pod bieżący index podstawiamy wartość z indexu wylosowanego
            arr[j] = temp; //pod wylosowany podstawiamy wartość z bieżącego indexu
        }
        return arr;
    },
}