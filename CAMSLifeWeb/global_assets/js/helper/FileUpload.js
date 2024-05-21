  

function UploadSummerNoteImage(file, id) {

    var formData = new FormData();
    formData.append("file", file);

    $.ajax({
        url: '/FileUpload/UploadSummerNoteImageAsync',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        enctype: 'multipart/form-data',
        success: function (response) {
            //var imgNode = document.createElement('img');
            //imgNode.src = response;
            //$('#' + id).summernote('insertNode', imgNode);
            $('#' + id).summernote('insertImage', response);
        }
    });

}

function UploadImage(file) {
    var formData = new FormData();
    formData.append("file", file);
    var url = "";
    $.ajax({
        url: '/FileUpload/UploadSummerNoteImageAsync',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        enctype: 'multipart/form-data',
        success: function (response) {
            url = response;
            //var imgNode = document.createElement('img');
            //imgNode.src = response;
            //$('#' + id).summernote('insertNode', imgNode);
            //$('#' + id).summernote('insertImage', response);
        }

    });
    return url;

}