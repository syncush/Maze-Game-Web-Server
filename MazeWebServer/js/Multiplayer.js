jQuery(function ($) {
    var enemyPosotionX;
    var enemyPosotionY;
    var positionX;
    var positionY;
    var endXaxis;
    var endYaxis;
    var mazeJSON = null;
    var myImg = null;
    var exitImg = null;
    var isAbleToMove = false;
    var cellHeight = 0;
    var cellWidth = 0;
    var k = -1;
    var maze;
    myImg = document.getElementById("playerImage");
    exitImg = document.getElementById("endImage");
    var canvasFX = getElementById("mazeCanvas");
    $.connection.hub.start().done(function () {
        var mult = $.connection.multiplayerHub;

        function movePlayer(boardName, posX, poxY, direction) {
            var myCan = getElementById(boardName);
            var myCanFX = myCan.getContext("2d");
            if (isAbleToMove === true) {
                myCanFX.fillStyle = "#FFFFFF";
                // Handle the arrow keys
                switch (direction) {
                    case "left":
                        k = posX * rows + (positionY - 1);
                        if (posX >= 0 && posX < rows && positionY - 1 < cols && positionY >= 0 && maze[k] == 0) {
                            myCanFX.fillRect(cellWidth * positionY, cellHeight * posX, cellWidth, cellHeight);
                            positionY = positionY - 1;


                        }
                        break;
                    case "up":
                        k = (posX - 1) * rows + (positionY);

                        if (posX - 1 >= 0 &&
                            posX - 1 < rows &&
                            positionY < cols &&
                            positionY >= 0 &&
                            maze[k] == 0) {

                            myCanFX.fillRect(cellWidth * positionY, cellHeight * posX, cellWidth, cellHeight);
                            posX = posX - 1;


                        }
                        break;
                    case "right":
                        k = posX * rows + (positionY + 1);
                        if (posX >= 0 &&
                            posX < rows &&
                            positionY + 1 < cols &&
                            positionY + 1 >= 0 &&
                            maze[k] == 0) {
                            myCanFX.fillRect(cellWidth * positionY, cellHeight * posX, cellWidth, cellHeight);
                            positionY = positionY + 1;


                        }
                        break;
                    case "down":
                        k = (posX + 1) * rows + (positionY);

                        if (posX + 1 >= 0 &&
                            posX + 1 < rows &&
                            positionY < cols &&
                            positionY >= 0 &&
                            maze[k] == 0) {

                            myCanFX.fillRect(cellWidth * positionY, cellHeight * posX, cellWidth, cellHeight);
                            posX = posX + 1;

                        }
                        break;
                }
                myCanFX.drawImage(myImg, positionY * cellWidth, posX * cellHeight, cellWidth, cellHeight);
                myCanFX.drawImage(exitImg, endYaxis * cellWidth, endXaxis * cellHeight, cellWidth, cellHeight);
                if (posX == endXaxis && positionY == endYaxis) {
                    alert("You won!");
                    isAbleToMove = false;
                }
            }
        }
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
        //send requst to start gmae to server
        $("#startButton").on("click", function (e) {
            var mazeData = {
                Name: $("#playForm input[name = 'Name']").val(),
                Rows: $("#playForm input[name= 'Rows']").val(),
                Cols: $("#playForm input[name= 'Cols']").val()
            };
            e.preventDefault();
        });
        $("#joinButton").on("click", function (e) {
            e.preventDefault();
            var joinedMazeName = $('#gamesList option:selected').text();
            //TODO ASK HUB TO JOIN GAME
        });
        //get the maze from server after the second player connected
        mult.client.GameStarted = function gameStarted(data) {
            mazeJSON = data;
            positionX = data["Start"]["Row"];
            positionY = data["Start"]["Col"];
            enemyPosotionX = positionX;
            enemyPosotionY = positionY;
            endXaxis = data["End"]["Row"];
            endYaxis = data["End"]["Col"];
            maze = data["Maze"];
            var canv = document.getElementById('mazeCanvas');
            canv.width = 50 * rows;
            canv.height = 50 * cols;
            cellWidth = canv.width / cols;
            cellHeight = canv.height / rows;
            $("#mazeCanvas").drawMaze("mazeCanvas", data, exitImg, endXaxis, endYaxis, myImg, positionX, positionY);
            $("#enemyCanvas").drawMaze("enemyCanvas", data, exitImg, endXaxis, endYaxis, myImg, positionX, positionY);
        };
        $("body").on("keyup", function (e) {
            switch (e.which) {
                case 37:
                    movePlayer("mazeCanvas", positionX, positionY, "left");
                    break;
                case 38:
                    movePlayer("mazeCanvas", positionX, positionY, "up");
                    break;
                case 39:
                    movePlayer("mazeCanvas", positionX, positionY, "up");
                    break;
                case 40:
                    movePlayer("mazeCanvas", positionX, positionY, "down");
                    break;
            }
            mult.server.MoveAction("down");
        });
    });
});
