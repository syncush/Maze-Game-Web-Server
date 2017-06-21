jQuery(function ($) {
    $("#registForm").on("submit", function (e) {
        e.preventDefault();
        var name = $("#input_1").val();
        var email = $("#input_2").val();
        var password = $.sha1($("#input_11").val());
        var verifyPassword = $.sha1($("#input_12").val());
        if (password === verifyPassword) {
            var userLoginJson = {
                UserName: name,
                Email: email,
                Password: password
            };
            $.ajax({
                type: "Post",
                url: "/api/User/Register",
                data: JSON.stringify(userLoginJson),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    alert("Welcome! " + name + "\n Page will now redirect!");
                    sessionStorage.setItem('user', userLoginJson.UserName);
                    $("#palapa").load("mainPage.html");

                },
                error: function(xhr, textStatus, errorThrown) {
                    alert("Failed to sign-in, please try again * sad face * !");
                }
            });
        } else {
            alert("Bad arguments, password does not match!");
        }

    });
    $("#back").addClass('abale');
});