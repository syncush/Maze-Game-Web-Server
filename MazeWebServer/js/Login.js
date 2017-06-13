jQuery(function ($) {
    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
        var userName = $("#userNameInput").val();
        var password = $.sha1($("#passwordInput").val());
        var loginData = {
            UserName: userName,
            Password: password
        };
        $.ajax({
            type: "Post",
            url: "/api/User/Login",
            data: JSON.stringify(loginData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("Welcome back " + userName + "!");
                sessionStorage.setItem('user', userName);
                $(location).attr("href", "index.html");
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Check username or password!");
            }
        });
    });
    $('#back').css('background-image', 'url(' + "/images/doge.jpg" + ')');
});