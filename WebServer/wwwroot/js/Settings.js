jQuery(function($) {
    $(function() {
        $("#myNavbar").load("HomePage.html");
        $("#Rows").val(localStorage.getItem("Rowz"));
        $("#Cols").val(localStorage.getItem("Colsz"));
        var algoVal = "BFS";
        if (localStorage.getItem("AlgoSelected") == "1") {
            algoVal = "DFS";
        }
        $("#AlgoSelector").val(algoVal);
    });
    
    $("#settForm").on("submit", function(e) {
        e.preventDefault();
        var valueRows = $("#Rows").val();
        var valueCols = $("#Cols").val();
        var algoSelected = $(this).find('option:selected').text();
        if (algoSelected == "BFS") {
            algoSelected = 0;
        } else {
            algoSelected = 1;
        }
        localStorage.setItem("Rowz", valueRows);
        localStorage.setItem("Colsz", valueCols);
        localStorage.setItem("AlgoSelected", algoSelected);
    });

});