jQuery(function ($)  {
    var maze = [[0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0],
                [0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0],
                [0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0],
                [0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0],
                [0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0],
                [0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0]];
    var currX = 0;
    var currY = 0;
    var mazeRows = maze.length;
    var mazeCols = maze[0].length;

    var endXaxis = 4;
    var endYaxis = 0;
    
    
    var cannnn = document.getElementById("mazeCanvas");
    var canvasFX = cannnn.getContext("2d");
    canvasFX.fillStyle = "#FFFFFF";

    var playerImg = document.getElementById("playerImage");
    var endGameImg = document.getElementById("endImage");

    $("#myNavbar").load("HomePage.html");
    $("#playForm").on("submit",function( e ) {
		e.preventDefault();
        var canv = document.getElementById('mazeCanvas');
        canv.width = 50 * mazeCols;
        canv.height = 50 * mazeRows;
        $("#mazeCanvas").drawMaze(maze);
        canvasFX.drawImage(playerImg, currY * 50, currX * 50, 50, 50);
        canvasFX.drawImage(endGameImg, endYaxis * 50, endXaxis * 50, 50, 50);
    });

    $("body").on("keydown", function (e) {
        canvasFX.fillStyle = "#FFFFFF";
        // Handle the arrow keys
        switch (e.which) {
            case 37:
                if (currX >= 0 && currX < mazeRows && currY - 1 < mazeCols && currY >= 0 && maze[currX][currY - 1] == 0) {
                    canvasFX.fillRect(50 * currY, 50 * currX, 50, 50);
                    currY = currY - 1;
                    
                }
                break;
            case 38:
                if (currX - 1 >= 0 && currX - 1 < mazeRows && currY < mazeCols && currY >= 0 && maze[currX - 1][currY] == 0) {
                    canvasFX.fillRect(50 * currY, 50 * currX, 50, 50);
                    currX = currX - 1;
                    
                }
                break;
            case 39:
                if (currX >= 0 && currX < mazeRows && currY + 1 < mazeCols && currY + 1 >= 0 && maze[currX][currY + 1] == 0) {
                    canvasFX.fillRect(50 * currY, 50 * currX, 50, 50);
                    currY = currY + 1;
                    
                }
                break;
            case 40:
                if (currX + 1 >= 0 && currX + 1 < mazeRows && currY < mazeCols && currY >= 0 && maze[currX + 1][currY] == 0) {
                    canvasFX.fillRect(50 * currY, 50 * currX, 50, 50);
                    currX = currX + 1;
                    
                }
                break;
        }
        canvasFX.drawImage(playerImg, currY * 50, currX * 50, 50, 50);
        if (currX == endXaxis && currY == endYaxis) {
            alert("You won your mother fucker nigger.");
        }
    });
    function OnValueInitStartUp() {
        $('input[name="Cols"]').val(localStorage.getItem('Colsz'));
        $('input[name="Rows"]').val(localStorage.getItem('Rowz'));
        $("#AlgoritmComboBox").val((localStorage.getItem('AlgoSelected')));
    }

});