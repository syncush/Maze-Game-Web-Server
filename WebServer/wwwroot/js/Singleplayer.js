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

    var c = document.getElementById("mazeCanvas");
    var canvasFX = c.getContext("2d");

    var playerImg = document.getElementById("playImg");
    var endGameImg = document.getElementById("endImage");

    $("#myNavbar").load("HomePage.html");
    $("#playForm").on("submit",function( e ) {
		e.preventDefault();
        var canv = document.getElementById('mazeCanvas');
        canv.width = 50 * mazeCols;
        canv.height = 50 * mazeRows;
        $("#mazeCanvas").drawMaze(maze);
    });

    $("body").on("keydown", function (e) {
        // Handle the arrow keys
        switch (e.which) {
            case 37:
                if (currX >= 0 && currX < mazeRows && currY - 1 < mazeCols && currY >= 0 && maze[currX][currY - 1] == 0) {
                    alert("left");
                    currY = currY - 1;
                }
                break;
            case 38:
                if (currX - 1 >= 0 && currX - 1 < mazeRows && currY < mazeCols && currY >= 0 && maze[currX - 1][currY] == 0) {
                    currX = currX - 1;
                    alert("up");
                }
                break;
            case 39:
                if (currX >= 0 && currX < mazeRows && currY + 1 < mazeCols && currY + 1 >= 0 && maze[currX][currY + 1] == 0) {
                    alert("right");
                    currY = currY + 1;
                }
                break;
            case 40:
                if (currX + 1 >= 0 && currX + 1 < mazeRows && currY < mazeCols && currY >= 0 && maze[currX + 1][currY] == 0) {
                    alert("down");
                    currX = currX + 1;
                }
                break;
        }
    });
    function OnValueInitStartUp() {
        $('input[name="Cols"]').val(localStorage.getItem('Colsz'));
        $('input[name="Rows"]').val(localStorage.getItem('Rowz'));
        $("#AlgoritmComboBox").val((localStorage.getItem('AlgoSelected')));
    }

});