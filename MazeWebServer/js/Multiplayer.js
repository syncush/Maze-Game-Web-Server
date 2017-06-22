jQuery(function ($) {
    var enemyPositionX;
    var enemyPositionY;
    var positionX;
    var positionY;
    var endXaxis;
    var endYaxis;
    var rows;
    var cols;
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
    var canvasFX = document.getElementById("mazeCanvas");
    var mult = $.connection.multiplayerHub;

   

    function moveEnemy(boardName, direction) {
        var myCan = document.getElementById(boardName);
        var myCanFX = myCan.getContext("2d");
        if (isAbleToMove === true) {
            myCanFX.fillStyle = "#FFFFFF";
            // Handle the arrow keys
            switch (direction) {
                case "left":
                    k = enemyPositionX * rows + (enemyPositionY - 1);
                    myCanFX.fillRect(cellWidth * enemyPositionY, cellHeight * enemyPositionX, cellWidth, cellHeight);
                    enemyPositionY = enemyPositionY - 1;
                    break;
                case "up":
                    k = (enemyPositionX - 1) * rows + (enemyPositionY);
                    myCanFX.fillRect(cellWidth * enemyPositionY, cellHeight * enemyPositionX, cellWidth, cellHeight);
                    enemyPositionX = enemyPositionX - 1;
                    break;
                case "right":
                    k = enemyPositionX * rows + (enemyPositionY + 1);
                    myCanFX.fillRect(cellWidth * enemyPositionY, cellHeight * enemyPositionX, cellWidth, cellHeight);
                    enemyPositionY = enemyPositionY + 1;
                    break;
                case "down":
                    k = (enemyPositionX + 1) * rows + (enemyPositionY);
                    myCanFX.fillRect(cellWidth * enemyPositionY, cellHeight * enemyPositionX, cellWidth, cellHeight);
                    enemyPositionX = enemyPositionX + 1;
                    break;
            }
        }
        myCanFX.drawImage(myImg, enemyPositionY * cellWidth, enemyPositionX * cellHeight, cellWidth, cellHeight);
        myCanFX.drawImage(exitImg, endYaxis * cellWidth, endXaxis * cellHeight, cellWidth, cellHeight);
    }

    mult.client.gotMessage = function (direction) {
        moveEnemy("enemyCanvas", direction);
    };

    mult.client.close = function (direction) {
        alert("You Lost!");
        isAbleToMove = false;
        var pieData2 = {
            User: $("#login").html()
        };
        $.ajax({
            type: "Post",
            url: "/api/User/UpdateLose",
            data: JSON.stringify(pieData2),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
            },
            error: function (xhr, textStatus, errorThrown) {

                alert("Failed to update lose , please contact an admin.");
            }
        });
        //$("#palapa").load("index.html");
    };

    mult.client.gameStarted = function (data) {
        mazeJSON = data;
        isAbleToMove = true;
        positionX = data["Start"]["Row"];
        positionY = data["Start"]["Col"];
        enemyPositionX = positionX;
        enemyPositionY = positionY;
        endXaxis = data["End"]["Row"];
        endYaxis = data["End"]["Col"];
        maze = data["Maze"];
        var canv = document.getElementById('mazeCanvas');
        rows = data["Rows"];
        cols = data["Cols"];
        canv.width = 50 * rows;
        canv.height = 50 * cols;
        cellWidth = canv.width / cols;
        cellHeight = canv.height / rows;
        var enemyCanvas = document.getElementById('enemyCanvas');
        $("#mazeCanvas").drawMaze("mazeCanvas", data, exitImg, endXaxis, endYaxis, myImg, positionX, positionY);
        canv = document.getElementById('enemyCanvas');
        canv.width = 50 * rows;
        canv.height = 50 * cols;
        cellWidth = canv.width / cols;
        cellHeight = canv.height / rows;
        $("#enemyCanvas").drawMaze("enemyCanvas", data, exitImg, endXaxis, endYaxis, myImg, positionX, positionY);
    };

    $.connection.hub.start().done(function () {
        function movePlayer(boardName, direction, shouldNotify) {
            var myCan = document.getElementById(boardName);
            var myCanFX = myCan.getContext("2d");
            if (isAbleToMove === true) {
                myCanFX.fillStyle = "#FFFFFF";
                // Handle the arrow keys
                switch (direction) {
                    case "left":
                        k = positionX * rows + (positionY - 1);
                        if (positionX >= 0 && positionX < rows && positionY - 1 < cols && positionY >= 0 && maze[k] == 0) {
                            myCanFX.fillRect(cellWidth * positionY, cellHeight * positionX, cellWidth, cellHeight);
                            positionY = positionY - 1;
                        }
                        break;
                    case "up":
                        k = (positionX - 1) * rows + (positionY);

                        if (positionX - 1 >= 0 &&
                            positionX - 1 < rows &&
                            positionY < cols &&
                            positionY >= 0 &&
                            maze[k] == 0) {

                            myCanFX.fillRect(cellWidth * positionY, cellHeight * positionX, cellWidth, cellHeight);
                            positionX = positionX - 1;


                        }
                        break;
                    case "right":
                        k = positionX * rows + (positionY + 1);
                        if (positionX >= 0 &&
                            positionX < rows &&
                            positionY + 1 < cols &&
                            positionY + 1 >= 0 &&
                            maze[k] == 0) {
                            myCanFX.fillRect(cellWidth * positionY, cellHeight * positionX, cellWidth, cellHeight);
                            positionY = positionY + 1;


                        }
                        break;
                    case "down":
                        k = (positionX + 1) * rows + (positionY);

                        if (positionX + 1 >= 0 &&
                            positionX + 1 < rows &&
                            positionY < cols &&
                            positionY >= 0 &&
                            maze[k] == 0) {

                            myCanFX.fillRect(cellWidth * positionY, cellHeight * positionX, cellWidth, cellHeight);
                            positionX = positionX + 1;

                        }
                        break;
                }
                myCanFX.drawImage(myImg, positionY * cellWidth, positionX * cellHeight, cellWidth, cellHeight);
                myCanFX.drawImage(exitImg, endYaxis * cellWidth, endXaxis * cellHeight, cellWidth, cellHeight);
                if (positionX == endXaxis && positionY == endYaxis) {
                    alert("You won!");
                    isAbleToMove = false;
                    mult.server.moveAction(direction);
                    var pieData = {
                        User: $("#login").html()
                    };
                    $.ajax({
                        type: "Post",
                        url: "/api/User/UpdateWin",
                        data: JSON.stringify(pieData),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                        },
                        error: function (xhr, textStatus, errorThrown) {

                            alert("Failed to update win , please contact an admin.");
                        }
                    });
                    setTimeout(function () { mult.server.close($("#login").html()); }, 400);
                }
                else {
                    shouldNotify = true;
                    mult.server.moveAction(direction);
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

                    alert("Failed to load games , sorry !");
                }
            });
        });



        //send requst to start gmae to server
        $("#playForm").on("submit", function (e) {
            e.preventDefault();
            var mazeData = {
                Name: $("#playForm input[name = 'Name']").val(),
                Rows: $("#playForm input[name= 'Rows']").val(),
                Cols: $("#playForm input[name= 'Cols']").val()
            };
            mult.server.start(mazeData.Name, mazeData.Rows, mazeData.Cols);
        });
        $("#joinButt").on("click", function (e) {
            e.preventDefault();
            var joinedMazeName = $('#gamesList option:selected').text();
            mult.server.join(joinedMazeName);
        });


        $("body").on("keyup", function (e) {
            switch (e.which) {
                case 37:
                    movePlayer("mazeCanvas", "left", true);

                    break;
                case 38:
                    movePlayer("mazeCanvas", "up", true);

                    break;
                case 39:
                    movePlayer("mazeCanvas", "right", true);

                    break;
                case 40:
                    movePlayer("mazeCanvas", "down", true);

                    break;
            }
        });


    });
    
});
