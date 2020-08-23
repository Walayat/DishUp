var cnt = 0;
$(document).ready(function () {
    $('.number').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
    ResetFooter();
});

function Calculate() {
    var totalCost = 0;
    $("#tblItem tbody tr td[id=lblCost1]").each(function () {
        totalCost += Number($(this).html());
    });

    $("#tblItem tbody tr").each(function () {
        if (totalCost > 0) {
            var itemcost = $(this).find("#lblCost1").html();
            var CostPerCentage = (Number(itemcost) / totalCost) * 100;
            $(this).find("#lblCostPer1").text(CostPerCentage.toFixed(2));
        }
        else {
            $(this).find("#lblCostPer1").text("0.00");
        }
    });

    $("#lblRecipeCost").html(parseFloat(totalCost).toFixed(2));
    $("#hdnRecipeCost").val(parseFloat(totalCost).toFixed(2));

    $("#lblGrossProfit").html((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));
    $("#hdnGrossProfit").val((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));

    $("#lblGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
    $("#hdnGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));

    $("#lblFoodCostPer").html(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
    $("#hdnFoodCostPer").Validate(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
}

function AddItems(flag) {
    if (Validate()) {
        if (flag == 0) {
            cnt = cnt + 1;
            var ItemTr = '<tr id="' + cnt + '">';
            ItemTr += '<td id="txtItemName1">' + $("#txtItemName").val() + '</td>';
            ItemTr += '<td id="txtQty1" align="right">' + $("#txtQty").val() + '</td>';
            ItemTr += '<td id="drpPortionedby1"><span id="lblPortionedby1">' + $("#drpPortionedby option:selected").text() + '</span><input type="hidden" id="hdnPortionedby1" value="' + $("#drpPortionedby option:selected").attr("unitid") + '"/></td>';
            ItemTr += '<td id="txtWgtPkg1" align="right">' + $("#txtWgtPkg").val() + '</td>';
            ItemTr += '<td id="drpPurchaseUnit1"><span id="lblPurchaseUnit1">' + $("#drpPurchaseUnit option:selected").text() + '</span><input type="hidden" id="hdnPurchaseUnit1" value="' + $("#drpPurchaseUnit option:selected").attr("unitid") + '"/></td>';
            ItemTr += '<td id="lblTotalPortion1" align="right">' + $("#lblTotalPortion").html() + '</td>';
            ItemTr += '<td id="txtPurchasePrice1" align="right">' + $("#txtPurchasePrice").val() + '</td>';
            ItemTr += '<td id="txtYield1" align="right">' + $("#txtYield").val() + '</td>';
            ItemTr += '<td id="lblYieldPortion1" align="right">' + $("#lblYieldPortion").html() + '</td>';
            ItemTr += '<td id="lblUnitCost1" align="right">' + $("#lblUnitCost").html() + '</td>';
            ItemTr += '<td id="lblCost1" align="right">' + $("#lblCost").html() + '</td>';
            ItemTr += '<td id="lblCostPer1" align="right"></td>';
            ItemTr += '<td>';
            ItemTr += '<a id="lnkEdit" href="javascript:void(0);" style="float: left;" onclick="EditItem(' + cnt + ')"><span class="glyphicon glyphicon-edit uk-text-primary" style="padding:5px" aria-hidden="true"></a>';
            ItemTr += ' <a href="javascript:void(0);" onclick="RemoveItem(' + cnt + ');"><span class="glyphicon glyphicon-remove-sign uk-text-danger" style="padding:5px" aria-hidden="true"></a>';
            ItemTr += '</td>';
            ItemTr += '</tr>';
            $("#tblItem tr:last").before(ItemTr);

            var totalCost = 0;
            $("#tblItem tbody tr td[id=lblCost1]").each(function () {
                totalCost += Number($(this).html());
            });

            $("#tblItem tbody tr").each(function () {
                if (totalCost > 0) {
                    var itemcost = $(this).find("#lblCost1").html();
                    var CostPerCentage = (Number(itemcost) / totalCost) * 100;
                    $(this).find("#lblCostPer1").text(CostPerCentage.toFixed(2));
                }
                else {
                    $(this).find("#lblCostPer1").text("0.00");
                }
            });

            $("#lblRecipeCost").html(parseFloat(totalCost).toFixed(2));
            $("#hdnRecipeCost").val(parseFloat(totalCost).toFixed(2));

            $("#lblGrossProfit").html((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));
            $("#hdnGrossProfit").val((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));

            $("#lblGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
            $("#hdnGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));

            $("#lblFoodCostPer").html(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
            $("#hdnFoodCostPer").val(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));

        }
        else {
            $("#tblItem tbody tr").each(function () {
                var id = $(this).attr("id");
                if (id == $("#hdnUpdateId").val()) {
                    $(this).find("#txtItemName1").html($("#txtItemName").val());
                    $(this).find("#txtQty1").html($("#txtQty").val());

                    $(this).find("#lblPortionedby1").html($("#drpPortionedby option:selected").text());

                    $(this).find("#hdnPortionedby1").val($("#drpPortionedby  option:selected").attr("unitid"));

                    $(this).find("#txtWgtPkg1").html($("#txtWgtPkg").val());

                    $(this).find("#lblPurchaseUnit1").html($("#drpPurchaseUnit option:selected").text());
                    $(this).find("#hdnPurchaseUnit1").val($("#drpPurchaseUnit  option:selected").attr("unitid"));

                    $(this).find("#lblTotalPortion1").html($("#lblTotalPortion").html());
                    $(this).find("#txtPurchasePrice1").html($("#txtPurchasePrice").val());
                    $(this).find("#txtYield1").html($("#txtYield").val());
                    $(this).find("#lblYieldPortion1").html($("#lblYieldPortion").html());
                    $(this).find("#lblUnitCost1").html($("#lblUnitCost").html());
                    $(this).find("#lblCost1").html($("#lblCost").html());

                    var totalCost = 0;
                    $("#tblItem tbody tr td[id=lblCost1]").each(function () {
                        totalCost += Number($(this).html());
                    });

                    $("#tblItem tbody tr").each(function () {
                        var itemcost = $(this).find("#lblCost1").html();
                        var CostPerCentage = (Number(itemcost) / totalCost) * 100;
                        $(this).find("#lblCostPer1").text(CostPerCentage.toFixed(2));
                    });

                    $("#lblRecipeCost").html(parseFloat(totalCost).toFixed(2));
                    $("#hdnRecipeCost").val(parseFloat(totalCost).toFixed(2));

                    $("#lblGrossProfit").html((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));
                    $("#hdnGrossProfit").val((parseFloat($("#txtPrice").val()) - parseFloat(totalCost)).toFixed(2));

                    $("#lblGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
                    $("#hdnGrossProfitPer").html(parseFloat(($("#hdnGrossProfit").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));

                    $("#lblFoodCostPer").html(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));
                    $("#hdnFoodCostPer").Validate(parseFloat(($("#hdnRecipeCost").val() / parseFloat($("#txtPrice").val())) * 100).toFixed(2));

                    $("#lnkUpdate").css("display", "none");
                    $("#lnkCancel").css("display", "none");
                    $("#lnkAdd").css("display", "block");
                }
            });
        }
        ResetFooter();
    }
}

function ResetFooter() {
    $("#txtItemName").val("");
    $("#txtQty").val("0.00");
    $("#txtWgtPkg").val("0.00");
    $("#txtPurchasePrice").val("0.00");
    $("#txtYield").val("100");
    $("#lblTotalPortion").html("0.00");
    $("#lblYieldPortion").html("0.00");
    $("#lblUnitCost").html("0.00");
    $("#lblCost").html("0.00");
    //$("#lblCostPer").html("0.00");

    $('select').prop('selectedIndex', 0);
    $("#lnkUpdate").css("display", "none");
    $("#lnkCancel").css("display", "none");
    $("#lnkAdd").css("display", "block");

}

function Validate() {
    if ($("#txtPrice").val() == "") {
        alert("Please Fill Price!!");
        $("#txtPrice").focus();
        return false;
    }
    else if ($("#txtItemName").val() == "") {
        alert("Please Fill Item Name!!");
        $("#txtItemName").focus();
        return false;
    }
    else if ($("#txtQty").val() == "") {
        alert("Please Fill Quantity!!");
        $("#txtQty").focus();
        return false;
    }
    else if ($("#drpPortionedby").val() == "0") {
        alert("Please Select Portioned By!!");
        $("#drpPortionedby").focus();
        return false;
    }
    else if ($("#txtWgtPkg").val() == "") {
        alert("Please Fill Wgt/Pkg!!");
        $("#txtWgtPkg").focus();
        return false;
    }
    else if ($("#drpPurchaseUnit").val() == "0") {
        alert("Please Select Purchase Unit!!");
        $("#drpPurchaseUnit").focus();
        return false;
    }
    else if ($("#txtPurchasePrice").val() == "") {
        alert("Please Fill Purchase Price!!");
        $("#txtPurchasePrice").focus();
        return false;
    }
    else if ($("#txtYield").val() == "") {
        alert("Please Fill Yield!!");
        $("#txtYield").focus();
        return false;
    }
    else {
        return true;
    }
}

function RemoveItem(cnt) {
    if (confirm("Sure you want to delete this item?")) {
        $("#tblItem tbody tr").each(function () {
            var id = $(this).attr("id");
            if (id == cnt) {
                $(this).remove();
                return false;
            }
        });
    }
}

function EditItem(cnt) {
    $("#tblItem tbody tr").each(function () {
        var id = $(this).attr("id");
        if (id == cnt) {
            var PortionedUnitId = $(this).find("#hdnPortionedby1").val();
            var PurchaseUnitId = $(this).find("#hdnPurchaseUnit1").val();
            $("#hdnUpdateId").val(id);
            $("#txtItemName").val($(this).find("#txtItemName1").html());
            $("#txtQty").val($(this).find("#txtQty1").html());

            $('#drpPortionedby option').each(function () {
                var $this = $(this);
                if ($this.attr("unitid") == PortionedUnitId) {
                    // $('#drpPortionedby').val($this.val());
                    $this.prop('selected', 'selected');
                    return false;
                }
            });

            //$("#drpPortionedby ").val($(this).find("#hdnPortionedby1").val());
            //$("#drpPortionedby option[unitid='" + PortionedUnitId + "']").attr("selected", "selected");

            $("#txtWgtPkg").val($(this).find("#txtWgtPkg1").html());

            $('#drpPurchaseUnit option').each(function () {
                var $this = $(this);
                if ($this.attr("unitid") == PurchaseUnitId) {
                    //$('#drpPurchaseUnit').val($this.val());
                    $this.prop('selected', 'selected');
                    return false;
                }
            });
            //$("#drpPurchaseUnit").val($(this).find("#hdnPurchaseUnit1").val());
            //$("#drpPurchaseUnit option[unitid='" + PurchaseUnitId + "']").attr("selected", "selected");

            $("#lblTotalPortion").html($(this).find("#lblTotalPortion1").html());
            $("#txtTotalPortion").val($(this).find("#lblTotalPortion1").html());

            $("#txtPurchasePrice").val($(this).find("#txtPurchasePrice1").html());
            $("#txtYield").val($(this).find("#txtYield1").html());

            $("#lblYieldPortion").html($(this).find("#lblYieldPortion1").html());
            $("#txtYieldPortion").val($(this).find("#lblYieldPortion1").html());

            $("#lblUnitCost").html($(this).find("#lblUnitCost1").html());
            $("#txtUnitCost").val($(this).find("#lblUnitCost1").html());

            $("#lblCost").html($(this).find("#lblCost1").html());
            //$("#lblCostPer").html($(this).find("#lblCostPer1").html());

            $("#lnkUpdate").css("display", "block");
            $("#lnkCancel").css("display", "block");
            $("#lnkAdd").css("display", "none");
        }
    });
}

function CalculateTotalPortion() {
    if ($("#drpPurchaseUnit option:selected").index() != 0 && $("#drpPortionedby option:selected").index() != 0 && $("#txtWgtPkg").val() != "") {
        var rhs = ConvertUnits('drpPurchaseUnit', 'drpPortionedby');
        var TotalPortion = parseFloat(parseFloat($("#txtWgtPkg").val()) * rhs).toFixed(2);
        $("#lblTotalPortion").html(TotalPortion);
        $("#txtTotalPortion").val(TotalPortion);
        $("#txtTotalPortion").trigger("onchange");
    }
}

function CalculateYieldPortion() {
    if ($("#txtYield").val() != "") {
        var YieldPortion = parseFloat((parseFloat($("#lblTotalPortion").html()) * parseFloat($("#txtYield").val())) / 100).toFixed(2);
        $("#lblYieldPortion").html(YieldPortion);
        $("#txtYieldPortion").val(YieldPortion);
        $('#txtYieldPortion').trigger("onchange");
    }
}

function CalculateUnitCost() {
    var UnitCost = "0.00";
    if ($("#lblYieldPortion").html() != "0.00")
        UnitCost = parseFloat(parseFloat($("#txtPurchasePrice").val()) / parseFloat($("#lblYieldPortion").html())).toFixed(2);
    $("#lblUnitCost").html(UnitCost);
    $("#txtUnitCost").val(UnitCost);
    $("#txtUnitCost").trigger("onchange");
}

function CalculateCost() {
    alert('ÁSD');
    if ($("#txtQty").val() != "") {
        var Cost = parseFloat(parseFloat($("#txtQty").val()) * parseFloat($("#lblUnitCost").html())).toFixed(2);
        $("#lblCost").html(Cost);
    }
}

function ResetPage() {
    if (confirm("Are you sure you want to clear all fields and start over?")) {
        //window.location.href = "/RecipeCalculator/RecipeCalculator";
        //$('#dvPrt').load('/RecipeCalculator/PrtRecipeCalculator');
        $.ajax({
            url: "/RecipeCalculator/PrtRecipeCalculator",
            cache: false,
            data: null,
            beforeSend: function () {
                $("#loading_image").show();
            },
            success: function (data) {
                $('#dvPrt').html(data);
                ResetFooter();
                $("#loading_image").hide();
            },
            error: function () {
            }
        });
    }
}

function onlyNos(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err.Description);
    }
}

