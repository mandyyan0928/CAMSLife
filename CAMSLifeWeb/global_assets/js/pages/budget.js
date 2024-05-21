var grandtotalace = 0;

// for income simulator
function IncomeMonthChange(col) {
    if (col == 3) {
        $("#tblincome").find('.quarter1').show();
        $("#tblincome").find('.quarter2').hide();
        $("#tblincome").find('.quarter3').hide();
        $("#tblincome").find('.quarter4').hide();
    }
    else if (col == 6) {
        $("#tblincome").find('.quarter1').show();
        $("#tblincome").find('.quarter2').show();
        $("#tblincome").find('.quarter3').hide();
        $("#tblincome").find('.quarter4').hide();
    }
    else if (col == 9) {
        $("#tblincome").find('.quarter1').show();
        $("#tblincome").find('.quarter2').show();
        $("#tblincome").find('.quarter3').show();
        $("#tblincome").find('.quarter4').hide();
    }
    else {
        $("#tblincome").find('.quarter1').show();
        $("#tblincome").find('.quarter2').show();
        $("#tblincome").find('.quarter3').show();
        $("#tblincome").find('.quarter4').show();
    }
    CalIncome();
}

function CalIncome() {
    let my = Intl.NumberFormat('en-MY');
    let a = $("#price").val();
    var acesum = 0;
    var acetotalsum = 0;
    var cases = 0;
    var month = $('#incomeMonth').val();
    $("#tblincome").find('td .inputproducer').each(function (i) {
        // num = id="xxx{1}";
        var num = i + 1;
        if (i < month) {
            cases = cases + parseFloat($(this).val());

            //total price = personalproducer * product price
            totalprice = parseFloat(a) * parseFloat($(this).val());
            $("#incomePrice" + num).text(my.format(totalprice.toFixed(2)));

            //total commission = totalprice * commission percent
            totalcommission = parseFloat(totalprice) * parseFloat($("#commPercent-1").val() / 100);
            $("#incomecomm" + num).text(my.format(totalcommission.toFixed(2)));

            //ace sum = total price * 12;
            ace = totalprice * 12;
            $("#incomeace" + num).text("RM " + my.format(ace.toFixed(2)));
            acesum += ace;
            acetotalsum += ace;
            if (num % 3 == 0) {
                $("#sumquarter" + num).text("RM " + my.format(acesum.toFixed(2)));
                acesum = 0;
            }
        }
    });
    $("#totalcase").text(cases);
    $(".commCase").text(cases);
    $(".commPrice").text(a);
    $("#acesumtotal").text("RM " + my.format(acetotalsum.toFixed(2)));
    grandtotalace = acetotalsum;
    CalComm();
    CalPropotion();
}

function CalComm() {
    let my = Intl.NumberFormat('en-MY');
    let a = $("#price").val();
    var acccom = 0;
    $("#tblcommission").find('tr').each(function (i) {
        var num = i + 1;
        ttlcom = parseFloat($("#commCount-" + num).val()) * parseFloat($("#commCase-" + num).text()) * parseFloat(a) * parseFloat($("#commPercent-" + num).val() / 100);
        $("#ttlCom-" + num).text("RM " + my.format(ttlcom.toFixed(2)));
        acccom += ttlcom;
        $("#accCom-" + num).text("RM " + my.format(acccom.toFixed(2)));

    });
}





function UpdateIncome() {
    swalInit.fire({
        title: 'Updated!',
        text: 'Budget updated succesfully',
        icon: 'success'
    });

}