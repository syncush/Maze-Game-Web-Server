jQuery(function ($) {
    var isLogged = false;
    function MoveForm(form) {
        $("#palapa").load(form);
    }
    $(function () {
        var isUser = sessionStorage.getItem('user');
        if (isUser != undefined && isUser == null) {
            //TODO sett attr
        }
    });
    $("#regist").on("click", function () {
        if (!isLogged) {
            MoveForm("Register.html");
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
            MoveForm("MultiPlayer.html");
    });
    $("#Singleplayer").on("click", function () {
            MoveForm("SinglePlayer.html");
    });
    $("#HomePage").on("click", function () {
            MoveForm("index.html");
    });
    $("#palapa").load("mainPage.html");

});