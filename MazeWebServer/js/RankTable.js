jQuery(function ($) {
    $('#back').css('background-image', 'url(' + "/images/dogorank.png" + ')');
    $.ajax({
        type: "Post",
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
                var obj = data[i];
                row = table.insertRow(-1);
                rank = row.insertCell(0);
                usernameCell = row.insertCell(1);
                winsCell = row.insertCell(2);
                loseCell = row.insertCell(3);
                usernameCell.innerHTML = obj["Username"];
                winsCell.innerHTML = obj["Wins"];
                loseCell.innerHTML = obj["Loses"];
                rank.innerHTML = i + 1;
                i++;
            }
        },
        Error: function (data) {
            alert("fail in loading table");
        }
    });
});
