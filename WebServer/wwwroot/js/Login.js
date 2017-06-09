jQuery(function($) {
  
    $("#myNavBar").load("HomePage.html");

    $("#formLogin").on("submit", function (e) {
        e.preventDefault();
        var userName = $("#userNameInput").val();
        var password = $.sha1($("#passwordInput").val());
        var loginData = {
            UserName: userName,
            Password: password
        };
        $.ajax({
            type: "GET",
            url: "/api/User/Login",
            data: loginData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("Welcome back " + userName + "!");
                $(location).attr("href", "index.html");
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Check username or password!");
            }
        });
    });
});