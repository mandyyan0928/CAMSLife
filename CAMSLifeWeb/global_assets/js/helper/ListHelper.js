
const ListHelper = {
    getList: function (className) {
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



const CheckBox = {
    getCheckedList: function (name) {
        var selected = [];


        $("input[name='" + name + "']:checked").each(function () {

            var text = $(this).next('label').text();
            const item = {};
            item[name] = $(this).val();
            selected.push(item);
        });

        return selected;

    },

    getCheckedIds: function (name) {
        var selected = [];


        $("input[name='" + name + "']:checked").each(function () {
            let id = parseInt($(this).val());
            selected.push(id);
        });

        return selected;

    },


}



$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

