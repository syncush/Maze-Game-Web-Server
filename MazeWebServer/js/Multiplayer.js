jQuery(function ($) {
    var enemyPosotionX;
    var enemyPosotionY;
    var positionX;
    var positionY;
    var mazeJSON;
    var myImg;
    var exitImg;
    myImg = document.getElementById("playerImage");
    exitImg = document.getElementById("endImage");
    $("#gamesList").click(function () {
        $.ajax({
            type: "Post",
            url: "/api/ListMulti/List",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var list = JSON.parse(data);
                var cmbBox = $("#gamesList");
                cmbBox.find('option').remove();
                var i = 0;
                for (i = 0; i < list.length; i++) {
                    cmbBox.append('<option value=' + i + '>' + list[i] + '</option>');
                }
            },
            error: function (xhr, textStatus, errorThrown) {

                return false;
            }
        });
    });


    //sign to server as the document is ready
    var messagesHub = $.connection.MultiPlayerHub;
    $.connection.hub.start().done(function () {
        messagesHub.server.connect("Hi!");
    })

    //get notification from server when the other moved
    messagesHub.client.GetOtherMove = function (move) {

        //todo re-draw other player position
    };
    ////get notification from server when the other won
    messagesHub.client.NotifyOtherWin = function () {

    };


    

    //send requst to start gmae to server
    $("#startButton").on("click", function (e) {
        var mazeData = {
            Name: $("#playForm input[name = 'Name']").val(),
            Rows: $("#playForm input[name= 'Rows']").val(),
            Cols: $("#playForm input[name= 'Cols']").val()
        };
        e.preventDefault();
        messagesHub.server.startMulti(mazeData);
    });

    $("#joinButton").on("click", function (e) {
        e.preventDefault();
        var joinedMazeName = $('#gamesList option:selected').text();
        messagesHub.server.JoinMulti(data);
    });

    //get the maze from server after the second player connected
    messagesHub.client.GetMaze = function GetMaze(data) {
        //todo stop display waiting gif
        alert("got a second player");
        $("#mazePlace").drawMaze(data);
    };

    $("body").on("keyup", function (e) {
        var flag = 0;
        var direction;
        switch (e.which) {
            //key left
        case 37:
            flag = 1;
            direction = "left";
            break;
        //key up
        case 38:
            direction = "up";
            flag = 1;
            break;
        //key right
        case 39:
            direction = "right";
            flag = 1;
        //key down
        case 40:
            direction = "down";
            flag = 1;
            break;
        default:
            break;
        }
        if (flag === 1) {
            messagesHub.server.getMoveFromPlayer(direction);
        }
        if ((positionX === endX) && (positionY === endY)) {
            messagesHub.server.notifyMyWin();
            alert("You won !");
        }
    });
});