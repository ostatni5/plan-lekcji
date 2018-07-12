/// <reference path="../index.html" />
/// <reference path="../libs/jquery-3.1.0.min.js" />
var Zegar = {
    init: function () {
        Zegar.make()
        setInterval(function () { Zegar.make() }, 1000)
    },
    make:function () {        
            var d = new Date();
            var n = d.getMinutes();
            var el = document.getElementById("minuta")
            if (parseInt(n) < 10)
                n = "0" + n
            el.innerHTML = n
        
            var d = new Date();
            var n = d.getHours();
            var el = document.getElementById("godzina")
            if (parseInt(n) < 10)
                n = "0" + n
            el.innerHTML = n
        
            var d = new Date();
            var n = d.getSeconds();
            var el = $("#kropki")
            if (parseInt(n) % 2 == 0)
                el.css("color", "#222")
            else
                el.css("color", "#0069a7")      
        
    }
}