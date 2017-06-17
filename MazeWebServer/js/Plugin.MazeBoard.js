(function ($) {
    $.fn.drawMaze = function (boardName, maze, exitImg, exitX, exitY, initPosImg, initX,initY) {
        var myCanvas = document.getElementById(boardName);
        var context = mazeCanvas.getContext("2d");
        var rows = maze.Rows;
        var cols = maze.Cols;
        var cellWidth = myCanvas.width / cols;
        var cellHeight = myCanvas.height / rows;
        var k;
        for (var i = 0; i < rows; i++) {
            for (var j = 0; j < cols; j++) {
                k = i * rows + j;
                if (maze.Maze[k] == 1) {
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                }
            }
        }
        context.drawImage(exitImg, exitY * cellWidth, exitX * cellHeight, cellWidth, cellHeight);
        context.drawImage(initPosImg, initY * cellWidth, initX * cellHeight, cellWidth, cellHeight);
    };
}(jQuery));