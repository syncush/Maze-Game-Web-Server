jQuery(function($) {
    $(function() {
        $("#Rows").val(localStorage.getItem("Rowz"));
        $("#Cols").val(localStorage.getItem("Colsz"));
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
        //localStorage.setItem("AlgoSelected", imgData);
    });

});