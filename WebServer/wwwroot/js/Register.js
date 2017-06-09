jQuery(function ($) {
    $(function () {
        $("#myNavbar").load("HomePage.html");
    });

    $("#registForm").on("submit", function (e) {
        e.preventDefault();
        var name = $("#input_1").val();
        var email = $("#input_2").val();
        var password = $.sha1($("#input_11").val());
        var verifyPassword = $.sha1($("#input_12").val());
        if (password === verifyPassword) {
            var mazeData = {
                Name: name,
                Email: email,
                Password: password
            };
            $.ajax({
                type: "POST",
                url: "/api/User",
                data: mazeData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    alert("Welcome! " + name);

                },
                error: function(xhr, textStatus, errorThrown) {

                    alert("Failed to sign-in, please try again * sad face * !");
                }
            });
        } else {
            alert("Bad arguments, password does not match!");
        }

    });
});