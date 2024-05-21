


const BlockUIHelper = {

blockUI:function BlockUI(id) {
    var block = $('#' + id);
    $(block).block({
        message: '<i class="icon-spinner4 spinner"></i>',
        overlayCSS: {
            backgroundColor: '#fff',
            opacity: 0.8,
            cursor: 'wait'
        },
        css: {
            border: 0,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
},

unblockUI:function UnblockUI(id) {
    var block = $('#' + id);
    $(block).unblock();
}
}