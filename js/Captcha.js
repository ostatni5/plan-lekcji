/// <reference path="../libs/jquery-3.1.0.min.js" />
/// <reference path="Zegar.js" />
/// <reference path="Database.js" />
/// <reference path="UI.js" />
/// <reference path="Ajax.js" />
/// <reference path="Settings.js" />
var Captcha = {

    init: function () {
        Captcha.create()
        $("#captcha_losuj").on("click", function () {
            Captcha.create()
        })
        $("#captcha_submit").on("click", function () {
            if($(".wybrane").length==3)
            {
                console.log("gotowe")
                var czy = false
                $(".wybrane").each(function (i) {                    
                    if (!$(".wybrane").hasClass("inny")) {
                        czy = true;
                    }
                    else {
                        Ui.methods.showAlert("Już Cię namierzamy robocie!!!")
                        czy = false;
                    }

                    
                })
                if (czy) {
                    Ui.methods.showAlert("Faktycznie możesz być człowiekiem")
                    logowanie.init()
                }
            }
        })
        $("#ekran_captcha").css("display", "block")
    },
    select: 0,
    kolej:[0,1,2,3],
    zestawy:[
        ["czolg1.png", "czolg2.png", "czolg3.png", "czolg4.png"],
        ["samolot1.png", "samolot2.png", "samolot3.png", "samolot4.png"],
        ["pistol1.jpg", "pistol2.jpg", "pistol3.jpg", "pistol4.jpg"],
    ],
    opisy: ["Zaznacz czołgi bo są fajne:", "Zaznacz to co latać może:", "Zaznacz broń krótką:"],

    dodaj_zestaw:function(obraz1,obraz2,obraz3,obraz_zly4,opis){
        var tab= [obraz1,obraz2,obraz3,obraz_zly4]
        Captcha.zestawy.push(tab)
        Captcha.opisy(opis)
    },
    tasuj: function () {   
        main.mixArray(Captcha.kolej)
        console.log(Captcha.kolej)
    },
    create: function () {
        Captcha.tasuj()
        var kontener = $("#captcha_obrazy")
        var zestaw = main.rand(0, Captcha.zestawy.length - 1)
        $("#captcha_pytanie").html(Captcha.opisy[zestaw])
        console.log(zestaw)
        kontener.html("")
        for(var i = 0;i<4;i++)
        {
            var img = $("<img>")
            kontener.append(img)
            img.addClass("captcha-obraz")
            img.attr("src", "/gfx/cpatcha/" + Captcha.zestawy[zestaw][Captcha.kolej[i]])
            if(Captcha.kolej[i]==3)
                img.addClass("inny")
        }
        $(".captcha-obraz").on("click", function () {
            console.log($(this))
            if ($(this).css('filter') != "sepia(100%)") {
                if (Captcha.select == 3) {
                    $(".captcha-obraz").css('filter', "sepia(0%)")
                    $(".captcha-obraz").css('-webkit-filter', "sepia(0%)")
                    Captcha.select = 0;
                    $(".captcha-obraz").removeClass("wybrane")
                }
                $(this).css('filter', "sepia(100%)")
                $(this).css('-webkit-filter', "sepia(100%)")
                $(this).addClass("wybrane")
                Captcha.select++
            }

        })
    },
    skukces: function () {
        console.log("przejdzy do logowania")
    }
}

var logowanie = {
    user: "",
    init: function () {
        $("#ekran_logowanie").addClass("animuj-logowanie")
        $("#ekran_captcha").removeClass("animuj-captcha")
        $("#dalej").on("click", function () {
            if (logowanie.zapora($("#login").val()) && logowanie.zapora($("#haslo").val()))
            {
                console.log("przesyłam dane...")
                var obj = {
                    action: "scan",
                    login: $("#login").val(),
                    haslo: $("#haslo").val()
                }
                Database.methods.userLogIn(obj)
                 .done(function (response) {
                      logowanie.alert(response)// na razie alert
                 })
                 .fail(function (response) {
                     Ui.methods.showAlert(response.responseText)
                 })
            }
        })
        
    },
    zapora: function (text) {
        console.log(text)
        if (text != "") {
            if (text.indexOf("javascript") != -1 || text.indexOf("<") != -1 || text.indexOf(">") != -1 || text.indexOf("'") != -1 || text.indexOf('"') != -1) {
                console.log("ostzregam!")
                Ui.methods.showAlert("Oj!!! wprowadzono nie dozwolone znaki - takie jak: < > \" , ' ")
                return false;
            }
            else
                return true;
        }
        else
            Ui.methods.showAlert("Ups... puste pola")
    },
    alert: function (text) {
        switch(text){
            case "login":
                {
                    logowanie.user = $("#login").val();
                    Ui.init();
                    Zegar.init();                    
                    $("#ekran_logowanie").removeClass("animuj-logowanie")
                    Ui.methods.showAlert("Zalogowano pomyślnie na:"+ logowanie.user)
                    $("#login").val("")
                    $("#haslo").val("")
                    setTimeout(function () { $("#get_dane").click() }, 1000)
                    if(logowanie.user!="admin")
                        $("#btn_baza").css("display", "none")
                }
                break;
            case "register":
                {
                    Ui.methods.showAlert("Zarejestrowano nowego użytkownika")
                }
                break;
            case "error":
                {
                    Ui.methods.showAlert("Złe hasło")
                }
                break;
        }
    }
}