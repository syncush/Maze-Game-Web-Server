jQuery(function ($)  {
    var currX = 0;
    var currY = 0;
    var maze;
    var endXaxis = 0;
    var endYaxis = 0;
    var rows;
    var cols;
    var yolo;
    var isAbleToMove = false;

    var cellWidth;
    var cellHeight;
    
    
    var cannnn = document.getElementById("mazeCanvas");
    var canvasFX = cannnn.getContext("2d");
    canvasFX.fillStyle = "#FFFFFF";

    var playerImg = document.getElementById("playerImage");
    var endGameImg = document.getElementById("endImage");

    $("#myNavbar").load("HomePage.html");

    $("#playForm").on("submit",function( e ) {
        e.preventDefault();
        var mazeData = {
            Name: $("#playForm input[name = 'Name']").val(),
            Rows: $("#playForm input[name= 'Rows']").val(),
            Cols: $("#playForm input[name= 'Cols']").val()
        };
        alert(mazeData.Name);
        $.ajax({
            type: "GET",
            url: "/api/GenerateMaze",
            data: mazeData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                currX = data["Start"]["Row"];
                currY = data["Start"]["Col"];
                endXaxis = data["End"]["Row"];
                endYaxis = data["End"]["Col"];
                rows = data.Rows;
                cols = data.Cols;
                maze = data.Maze;
                yolo = data;
                 var canv = document.getElementById('mazeCanvas');
                 canv.width = 50 * rows;
                 canv.height = 50 * cols;
                 cellWidth = canv.width / cols;
                 cellHeight = canv.height / rows;
                 $("#mazeCanvas").drawMaze(data, endGameImg, endXaxis, endYaxis, playerImg, currX, currY);
                 isAbleToMove = true;
                
            },
            error: function (xhr, textStatus, errorThrown) {

                return false;
            }
        });
    });
    $("#restartButt").on("click",
        function () {
            
            canvasFX.fillRect(cellWidth * currY, cellHeight * currX, cellWidth, cellHeight);
            currX = yolo["Start"]["Row"];
            currY = yolo["Start"]["Col"];
            canvasFX.drawImage(playerImg, currY * cellWidth, currX * cellHeight, cellWidth, cellHeight);
            isAbleToMove = true;
            canvasFX.drawImage(endGameImg, endYaxis * cellWidth, endXaxis * cellHeight, cellWidth, cellHeight);

        });
    $("body").on("keydown", function (e) {
        if (isAbleToMove === true) {
            var k;
            canvasFX.fillStyle = "#FFFFFF";
            // Handle the arrow keys
            switch (e.which) {
            case 37:
                k = currX * rows + (currY - 1);
                if (currX >= 0 && currX < rows && currY - 1 < cols && currY >= 0 && maze[k] == 0) {
                    canvasFX.fillRect(cellWidth * currY, cellHeight * currX, cellWidth, cellHeight);
                    currY = currY - 1;

                }
                break;
            case 38:
                k = (currX - 1) * rows + (currY);
                if (currX - 1 >= 0 && currX - 1 < rows && currY < cols && currY >= 0 && maze[k] == 0) {
                    canvasFX.fillRect(cellWidth * currY, cellHeight * currX, cellWidth, cellHeight);
                    currX = currX - 1;

                }
                break;
            case 39:
                k = currX * rows + (currY +  1);
                if (currX >= 0 && currX < rows && currY + 1 < cols && currY + 1 >= 0 && maze[k] == 0) {
                    canvasFX.fillRect(cellWidth * currY, cellHeight * currX, cellWidth, cellHeight);
                    currY = currY + 1;

                }
                break;
            case 40:
                k = (currX + 1) * rows + (currY);
                if (currX + 1 >= 0 && currX + 1 < rows && currY < cols && currY >= 0 && maze[k] == 0) {
                    canvasFX.fillRect(cellWidth * currY, cellHeight * currX, cellWidth, cellHeight);
                    currX = currX + 1;
                }
                break;
            }
            canvasFX.drawImage(playerImg, currY * cellWidth, currX * cellHeight, cellWidth, cellHeight);
            canvasFX.drawImage(endGameImg, endYaxis * cellWidth, endXaxis * cellHeight, cellWidth, cellHeight);
            if (currX == endXaxis && currY == endYaxis) {
                alert("You won your mother fucker nigger.");
                isAbleToMove = false;
            }
        }
    });
    function OnValueInitStartUp() {
        $('input[name="Cols"]').val(localStorage.getItem('Colsz'));
        $('input[name="Rows"]').val(localStorage.getItem('Rowz'));
        $("#AlgoritmComboBox").val((localStorage.getItem('AlgoSelected')));
    }

});