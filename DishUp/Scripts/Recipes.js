function AddIngredient(id) {
    $.ajax({
        type: "POST",
        url: "/Recipes/AddIngredient",  //form.action,
        data: $('#formuSupplie').serialize();
        success: function (data) {
            if (data.success) {
                $("#divShow").removeClass();
                $("#divShow").css("display", "block");
                $("#divShow").addClass(data.CssClassName);
                $("#Title").text(data.Title);
                $("#dtMessage").text(data.detailMessage);
                // do what you want after update or add successfully
            } else {
                $("#divShow").css("display", "block");
                $("#divShow").addClass(data.CssClassName);
                $("#Title").text(data.Title);

                //$("#divShow").css("display", "none");
                // do what you want after failure
            }
        },

    });
    return false;
}