jQuery(function ($) {
    var isLogged = false;
    function MoveForm(form) {
        $("#palapa").load(form);
    }
    
    $(function () {
        var isUser = sessionStorage.getItem('user');
        if (isUser == null) {
            //TODO sett attr
        }
    });
    $("#regist").on("click", function () {
        if (!isLogged) {
            MoveForm("Register.html");
        } else {
            sessionStorage.clear();
            isLogged = false;
            data = null;
            MoveForm("index.html");
        }
    });
    $("#login").on("click", function () {
        if (!isLogged) {
            MoveForm("Login.html");
        }
    });
    var refreshIntervalId = setInterval(function () {
        if (data != null) {
            data = sessionStorage.getItem('user');
            if (data != null) {
                break;
            } else {
                isLogged = false;
                sessionStorage.clear();
                $("#regist").html("Register");
                $("#login").html("Login");
            }
        }
        
    }, 1000);
    $("#settings").on("click", function () {
        MoveForm("Settings.html");
    });
    $("#Multiplayer").on("click", function () {
        if (isLogged) {
            MoveForm("MultiPlayer.html");
        } else {
            MoveForm("Login.html");
        }
            
    });
    $("#Singleplayer").on("click", function () {
            MoveForm("SinglePlayer.html");
    });
    $("#HomePage").on("click", function () {
            MoveForm("index.html");
    });
    $("#palapa").load("mainPage.html");
});