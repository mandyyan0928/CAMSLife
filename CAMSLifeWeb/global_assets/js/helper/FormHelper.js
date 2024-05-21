
const FormHelper = {
    addData: function (name, value,form) {
        let jsonObj = [];

        var res = $('.' + className).map(function () {
            var $inputs = $(this).find('input');
            var arrInputs = $inputs.serializeArray();
            console.log(arrInputs);


            item = {}
            $.each(arrInputs, function (k, v) {
                item[v.name] = v.value;
            });
            jsonObj.push(item);
        }).get();


        return jsonObj;

    }
}
 