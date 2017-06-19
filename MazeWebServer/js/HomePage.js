jQuery(function ($) {
    var isLogged = false;
    var refreshIntervalId = null;
    var data = null;

    function MoveForm(form) {
        $("#palapa").load(form);
    }
    refreshIntervalId = setInterval(function () {
        if (data == null) {
            data = sessionStorage.getItem('user');
            if (data != null) {
                isLogged = true;
                clearInterval(refreshIntervalId);
                $("#regist").html("Logout");
                $("#login").html(sessionStorage.getItem('user'));
            } else {
                isLogged = false;
                $("#regist").html("Register");
                $("#login").html("Login");
            }
        }

    }, 1000);
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