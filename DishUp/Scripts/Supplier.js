

$(document).ready(function () {
    $('#isPackage').change(function () {
        $('#isPackageSupplie').toggle(this.checked);
    }).change(); //ensure visible state matches initially

        $('#isBox').change(function () {
        $('#isBoxSupplie').toggle(this.checked);
        }).change();

        var pageSize = 20;


        //Method which is to be called for populating options in dropdown //dynamically
        var myUrlCategory = $("#urlCategory").val();
        var myUrlUnitYield = $("#UrlUnitYield").val();
        var myUrlUnitPortion = $("#UrlUnitPortion").val();
        var myUrlPrepTime = $("#urlPrepTime").val();
        var myUrlCookTime = $("#urlCookTime").val();
        var myUrlShelfTime = $("#urlShelfTime").val();




        $("#ID_CATEGORY").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlCategory,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select Category --",
                minimumInputLength: 0,
                allowClear: true
            });

        $("#ID_MEDIDA_YIELD").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlUnitYield,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select --",
                minimumInputLength: 0,
                allowClear: true
            });

        $("#ID_MEDIDA_PORTION").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlUnitPortion,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select --",
                minimumInputLength: 0,
                allowClear: true
            });

        $("#ID_PREP_TIME").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlPrepTime,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select --",
                minimumInputLength: 0,
                allowClear: true
            });

        $("#ID_COOK_TIME").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlCookTime,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select --",
                minimumInputLength: 0,
                allowClear: true
            });

        $("#ID_SHELF_TIME").select2(
            {
                ajax: {
                    delay: 150,
                    url: myUrlShelfTime,
                    dataType: 'json',
                    data: function (params) {
                        params.page = params.page || 1;
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.Results,
                            pagination: {
                                more: (params.page * pageSize) < data.Total
                            }
                        };
                    }
                },
                placeholder: "-- Select --",
                minimumInputLength: 0,
                allowClear: true
            });



});
//function ClearSupplie() {
            //    //$('#ID_INSUMO').val('');
            //    $('#modalSupplier').modal('hide');

            //    $('#modalNewSupplie').modal('hide');
            //    var frm = document.getElementById('formAddSupplie');
            //    frm.reset();  // Reset all form data
            //    return false;
            //}
function AddIngredient(id) {
    $.ajax({
        type: "POST",
        url: "/Supplier/AddIngredient",  //form.action,
        data: { ids: id },
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
function AddSupplierToUser(id) {
    $.ajax({
        type: "POST",
        url: "/Supplier/AddSuplierToUser",  //form.action,
        data: { ids: id },
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
function ImportSuppliesFromSupplier(id) {
    $.ajax({
        type: "POST",
        url: "/Supplier/ImportSuppliesFromSupplier",  //form.action,
        data: { ids: id },
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
    $(function () {
        //ensure visible state matches initially
        $(".iconEdit").prepend("<span class=\"fa fa-edit\"></span>&nbsp;");
    $(".iconDelete").prepend("<span class=\"fa fa-trash\"></span> & nbsp; ");
});