jQuery(function ($) {

    $("#myNavbar").load("HomePage.html");
    $.ajax({
        type: "GET",
        url: "/api/User/GetRankTable",
        success: function (data) {
            var table = document.getElementById("tblRank");
            var row;
            var usernameCell;
            var winsCell;
            var loseCell;
            var rank;
            var i = 0;
            for (var key in data) {
                row = table.insertRow(1);
                rank = row.insertCell(0);
                usernameCell = row.insertCell(1);
                winsCell = row.insertCell(2);
                loseCell = row.insertCell(3);
                usernameCell.innerHTML = data[i]["username"];
                winsCell.innerHTML = data[i]["wins"];
                loseCell.innerHTML = data[i]["losses"];
                rank.innerHTML = i + 1;
                i++;
            }

        },
        Error: function (data) {
            alert("fail in loading table");
        }
    });
});
