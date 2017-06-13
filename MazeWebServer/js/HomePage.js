jQuery(function ($) {
    var isLogged = false;
    function MoveForm(form) {
        $("#palapa").load(form);
    }
    $(function () {
        var isUser = sessionStorage.getItem('user');
        if (isUser != undefined && isUser == null) {
            isLogged = true;
            $("#login").innerHTML = isUser;
            $("#regist").innerHTML = "Logout";
        }
    });
    $("#regist").on("click", function () {
        if (!isLogged) {
            MoveForm("Register.html");
        } else {
            isLogged = false;
            $("#regist").innerHTML = "Register";
            $("#login").innerHTML = "Login";
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