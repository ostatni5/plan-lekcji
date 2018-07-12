/// <reference path="Ajax.js" />
/// <reference path="Database.js" />
/// <reference path="../index.html" />
/// <reference path="Database.js" />
/// <reference path="../libs/jquery-3.1.0.min.js" />
function encodeRFC5987ValueChars(str) {
    return encodeURIComponent(str).replace(/['()]/g, escape).replace(/\*/g, '%2A').replace(/%(?:7C|60|5E)/g, unescape);
}
function popraw(text) {
    if (text.length == 1) {
        text = "0" + text
    }
    return text;
}
var Ui = {

    init: function () {
        $("#ekran").on("click", function () {
            $(".ekran-dark").removeClass("animuj")
            $(".ekran-dark").removeClass("animuj-pod_menu")
        })
        $(".menu-dol-box").on("click", function () {
            console.log("klik")
            $(".ekran-dark").removeClass("animuj")
            var id = "#ekran_" + $(this).attr("id")
            console.log(id)
            $(id).toggleClass("animuj");
        })
        $(".ustawienia-btn").on("click", function () {
            console.log("klik")
            $(".pod_menu").removeClass("animuj-pod_menu")
            var id = "#ekran_" + $(this).attr("id").slice(4,20)
            console.log(id)
            if (id == "#ekran_godziny")
                $(id).toggleClass("animuj");
            else
            $(id).toggleClass("animuj-pod_menu");
        })
        $(".close").on("click", function () {
            var item = $(this).parent()
            console.log(item)
            item.removeClass("animuj")
            item.removeClass("animuj-pod_menu")
           // if (item.hasClass("foo"))
           // $(".pod_menu").removeClass("animuj-pod_menu")
        })
        $("#menu_przycisk").on("click", function () {
            $("#przyciski").toggleClass("urkyj-dol");
        })
        $("#godzina").on("click", function () {
            // $("#zegar").toggleClass("smieszek");
            // setTimeout(function () { $("#zegar").toggleClass("smieszek");}, 1500)
            $("#zegar").animate({ left: $("#zegar").position().left - 20 })
            console.log($("#zegar").position())
            setTimeout(function () { $("#zegar").animate({ left: 0 }) }, 3000)
        })
        $("#minuta").on("click", function () {
            // $("#zegar").toggleClass("smieszek");
            // setTimeout(function () { $("#zegar").toggleClass("smieszek");}, 1500)
            $("#zegar").animate({ left: $("#zegar").position().left + 20 })
            console.log($("#zegar").position())
            setTimeout(function () { $("#zegar").animate({ left: 0 }) }, 3000)
        })
        

        var licznik_g = 0;
        var licznik_m = 0;
        var animuj = true;
        var height_liczba;
        var ost_el=undefined;
        $("#ekran_godziny").on("click", ".godziny-odG, .godziny-doG", function () {
            console.log("klik")
            console.log($(this).html())
            console.log($(this).index())
            ost_el = $(this);
            $("#ekran_godziny_wybor").toggleClass("animuj")
            var temp = $(this).text()
            console.log(temp)
            height_liczba = $("#wybor_godzina .lista-liczb div").height();
            $(".widok-minuta").text(temp.split(":")[1])
            $(".widok-godzina").text(temp.split(":")[0])
            licznik_g = parseInt(temp.split(":")[0]) / 1;
            licznik_m = parseInt(temp.split(":")[1]) / 5;
            $("#wybor_godzina .lista-liczb").animate({
                top: "-" + (height_liczba * licznik_g),
            }, 300, function () { animuj = true; })
            $("#wybor_minuta .lista-liczb").animate({
                top: "-" + (height_liczba * licznik_m),
            }, 300, function () { animuj = true; })
            //licznik_g = 0;
            //licznik_m = 0;
            //animuj = true;          

        })


        //wybor - up        
        $(".wybor-up").on("click", function () {
            height_liczba = $("#wybor_godzina .lista-liczb div").height();           
            console.log($(this).parent().attr('id'))
            switch($(this).parent().attr('id'))
            {
                case "wybor_godzina": {
                    
                    if (licznik_g < 23 && animuj) {
                        animuj = false;
                        licznik_g++;
                        console.log(licznik_g)
                        $("#wybor_godzina .lista-liczb").animate({
                            top: "-" + (height_liczba * licznik_g),
                        }, 100, function () { animuj = true; })
                        var temp = $("#wybor_godzina .lista-liczb div").eq(licznik_g + 1).text()
                        console.log(temp)
                        $(".widok-godzina").text(temp)
                    }
                }
                    break;
                case "wybor_minuta": {
                    if (licznik_m < 11 && animuj) {
                        animuj = false;
                        licznik_m++;
                        $("#wybor_minuta .lista-liczb").animate({
                            top: "-" + (height_liczba * licznik_m),
                        }, 100, function () { animuj = true; })
                        var temp = $("#wybor_minuta .lista-liczb div").eq(licznik_m+1).text()
                        $(".widok-minuta").text(temp)
                    }
                }
                    break;
            }

        })
        $(".wybor-down").on("click", function () {
            height_liczba = $("#wybor_godzina .lista-liczb div").height();
            console.log($(this).parent().attr('id'))
            switch ($(this).parent().attr('id')) {
                case "wybor_godzina": {
                    if (licznik_g > 0 && animuj) {
                        animuj = false;
                        licznik_g--;
                        console.log(licznik_g)
                        $("#wybor_godzina .lista-liczb").animate({
                            top: "-" + (height_liczba * licznik_g),
                        }, 100, function () { animuj = true; })
                        var temp = $("#wybor_godzina .lista-liczb div").eq(licznik_g + 1).text()
                        console.log(temp)
                        $(".widok-godzina").text(temp)
                    }
                }
                    break;
                case "wybor_minuta": {
                    if (licznik_m > 0 && animuj) {
                        animuj = false;
                        licznik_m--;
                        $("#wybor_minuta .lista-liczb").animate({
                            top: "-" + (height_liczba * licznik_m),
                        }, 100, function () { animuj = true; })
                        var temp = $("#wybor_minuta .lista-liczb div").eq(licznik_m + 1).text()
                        $(".widok-minuta").text(temp)
                    }
                }
                    break;
            }

        })
        
        $(".submit").on("click", function () {
            if (ost_el != undefined)
            {
                if (ost_el.index() % 3 == 1)
                    var pomcT = "od"
                else
                    var pomcT = "do"

                ost_el.text($(".widok-godzina").text() + ":" + $(".widok-minuta").text())
                var item = $(this).parent()                
                item.removeClass("animuj")
                var obj = {
                    action: "send_dane",
                    typ: pomcT,
                    id: parseInt(ost_el.index()/3),
                    godzina: popraw($(".widok-godzina").text()),
                    minuta: popraw($(".widok-minuta").text()),
                }
                console.log(obj)
                Database.methods.addTabela(obj)
                 .done(function (response) {
                     Ui.methods.showAlert(response) // na razie alert
                 })
                 .fail(function (response) {
                     Ui.methods.showAlert(response.responseText)
                 })
            }
            else
                Ui.methods.showAlert("Błąd")

        })



        $(".baza-operacje").on("click", function () {
            console.log("klik")
            console.log($(this).attr("id"))
            switch ($(this).attr("id")) {
                case "add_tabela":
                    {   
                        
                       
                        var obj = {
                            action: $(this).attr("id"),
                            
                            
                        }
                        Database.methods.addTabela(obj)
                         .done(function (response) {
                             Ui.methods.showAlert(response) // na razie alert
                         })
                         .fail(function (response) {
                             Ui.methods.showAlert(response.responseText)
                         })
                    }
                    break;
                case "del_tabela":
                    {
                        var obj = {
                            action: $(this).attr("id"),
                        }
                        Database.methods.delTabela(obj)
                         .done(function (response) {
                             Ui.methods.showAlert(response) // na razie alert
                         })
                         .fail(function (response) {
                             Ui.methods.showAlert(response.responseText)
                         })
                    }
                    break;
                case "add_dane":
                    {
                        var obj = {
                            action: $(this).attr("id"),
                        }
                        Database.methods.addDane(obj)
                         .done(function (response) {
                             Ui.methods.showAlert(response) // na razie alert
                         })
                         .fail(function (response) {
                             Ui.methods.showAlert(response.responseText)
                         })
                    }
                    break;
                case "del_dane":
                    {
                        var obj = {
                            action: $(this).attr("id"),
                        }
                        Database.methods.delDane(obj)
                         .done(function (response) {
                             Ui.methods.showAlert(response) // na razie alert
                         })
                         .fail(function (response) {
                             Ui.methods.showAlert(response.responseText)
                         })
                    }
                    break;
                case "get_dane":
                    {
                        var currentDate = new Date();
                        var dzien = currentDate.getDay()
                        console.log(dzien)
                        var obj = {
                            action: $(this).attr("id"),
                            dzien: dzien,
                            login: logowanie.user,
                        }
                        Database.methods.getDane(obj)
                         .done(function (response) {
                             Ui.methods.showAlert("Pobrano Dane") // na razie alert
                             //console.log(response)
                             var tabela = JSON.parse(response)
                             console.log(tabela)

                             //console.log(tabela.godziny.length)
                             var kontener = $("#godziny_dane")
                             kontener.html("")
                             var lp = $("<div>")
                             kontener.append(lp)
                             lp.addClass("godziny-head")
                             lp.html("Lp")

                             var odG = $("<div>")
                             kontener.append(odG)
                             odG.addClass("godziny-head")
                             odG.html("Początek")

                             var doG = $("<div>")
                             kontener.append(doG)
                             doG.addClass("godziny-head")
                             doG.html("Koniec")
                             for (var i = 0; i < tabela.godziny.length; i++)
                             {
                                 var lp = $("<div>")
                                 kontener.append(lp)
                                 lp.addClass("godziny-lp")
                                 lp.html(tabela.godziny[i].id)

                                 var odG = $("<div>")
                                 kontener.append(odG)
                                 odG.addClass("godziny-odG")
                                 odG.html(popraw(tabela.godziny[i].odG) + ":" + popraw(tabela.godziny[i].odM))

                                 var doG = $("<div>")
                                 kontener.append(doG)
                                 doG.addClass("godziny-doG")
                                 doG.html(popraw(tabela.godziny[i].doG) + ":" + popraw(tabela.godziny[i].doM))

                                 
                             }
                             //console.log(kontener.html())

                             //console.log(tabela.godziny.length)
                             var kontener = $("#dzisiaj_dane")
                             kontener.html("")
                             var lp = $("<div>")
                             kontener.append(lp)
                             lp.addClass("dzisiaj-head")
                             lp.html("Lp")

                             var odG = $("<div>")
                             kontener.append(odG)
                             odG.addClass("dzisiaj-head")
                             odG.html("Lekcja")

                             var doG = $("<div>")
                             kontener.append(doG)
                             doG.addClass("dzisiaj-head")
                             doG.html("Sala")
                             for (var i = 0; i < tabela.dzien.length; i++) {
                                 var lp = $("<div>")
                                 kontener.append(lp)
                                 lp.addClass("dzisiaj-lp")
                                 lp.html(i+1)

                                 var lekcja = $("<div>")
                                 kontener.append(lekcja)
                                 lekcja.addClass("dzisiaj-lekcja")
                                 lekcja.html(tabela.dzien[i].dluga_nazwa_p)

                                 var sala = $("<div>")
                                 kontener.append(sala)
                                 sala.addClass("dzisiaj-sala")
                                 sala.html(tabela.dzien[i].numer_sali)


                             }
                             //console.log(kontener.html())

                             //console.log(tabela.godziny.length)
                             var kontener = $("#tydzien_dane")
                             kontener.html("")
                             var lp = $("<div>")
                             kontener.append(lp)
                             lp.addClass("tydzien-head")
                             lp.html("Lp")

                             var div = $("<div>")
                             kontener.append(div)
                             div.addClass("tydzien-head")
                             div.html("PN")

                             var div = $("<div>")
                             kontener.append(div)
                             div.addClass("tydzien-head")
                             div.html("WT")

                             var div = $("<div>")
                             kontener.append(div)
                             div.addClass("tydzien-head")
                             div.html("ŚR")

                             var div = $("<div>")
                             kontener.append(div)
                             div.addClass("tydzien-head")
                             div.html("CZ")

                             var div = $("<div>")
                             kontener.append(div)
                             div.addClass("tydzien-head")
                             div.html("PT")

                             for (var i = 0; i < 14; i++) {
                                 var lp = $("<div>")
                                 kontener.append(lp)                                 
                                 lp.html(i+1)
                                 for (var j = 0 ; j < 5; j++) {
                                     var temp = i+j*14
                                     var div = $("<div>")
                                     kontener.append(div)
                                     div.addClass("tydzien-content")
                                         var div_dziecko = $("<div>")
                                         div.append(div_dziecko)
                                         div_dziecko.html(tabela.tydzien[temp].nazwa_krotka_p)
                                         var div_dziecko = $("<div>")
                                         div.append(div_dziecko)
                                         div_dziecko.html(tabela.tydzien[temp].numer_sali)
                                 }

                             }
                             //console.log(kontener.html())
                         })
                         .fail(function (response) {
                             Ui.methods.showAlert(response.responseText)
                         })
                    }
                    break;
            }
        })

    },
    methods: {

        showAlert: function (text) {
            $(".ok-alert").on("click", function () {
                Ui.methods.closeAlert()
            })
            $("#ekran_alert").toggleClass("animuj-alert")
            var pomoc;
            switch (text) {
                case "add_tabela":
                    {
                        pomoc="Dodano Tabelę"
                    }
                    break;
                case "del_tabela":
                    {
                        pomoc = "Usunięto Tabelę"
                    }
                    break;
                case "add_dane":
                    {
                        pomoc = "Dodano Dane"
                    }
                    break;
                case "del_dane":
                    {
                        pomoc = "Usunięto Dane"
                    }
                    break;                
                default:{
                    pomoc = text
                }
                    break;
            }
            console.log(pomoc)
            $("#alert_tekst").html(pomoc)
            zamknij = setTimeout(function () { Ui.methods.closeAlert();console.log("zamk automat")},10000)
        },
        closeAlert: function () {
            // animacja ukrycia ekranu alerta
            clearTimeout(zamknij)
            $("#ekran_alert").removeClass("animuj-alert")
        },
        //pozostałe funkcje

    }
}