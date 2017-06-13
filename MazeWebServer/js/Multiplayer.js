jQuery(function ($) {
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
});