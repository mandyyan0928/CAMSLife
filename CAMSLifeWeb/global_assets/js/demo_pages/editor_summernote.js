/* ------------------------------------------------------------------------------
 *
 *  # Summernote editor
 *
 *  Demo JS code for editor_summernote.html page
 *
 * ---------------------------------------------------------------------------- */


// Setup module
// ------------------------------

var Summernote = function() {


    //
    // Setup module components
    //

    // Summernote
    var _componentSummernote = function() {
        if (!$().summernote) {
            console.warn('Warning - summernote.min.js is not loaded.');
            return;
        }

        // Basic examples
        // ------------------------------

        // Default initialization
        $('.summernote').summernote({
            callbacks: {
                onImageUpload: function (files) {
                    for (let i = 0; i < files.length; i++) {
                        var id = $(this).attr('id');
                        //FileUploadHelper.UploadSummerNoteImage
                        UploadSummerNoteImage(files[i], id); eImage
                    }
                }
            }
        });

        // Control editor height
        $('.summernote-height').summernote({
            height: 600,
            callbacks: {
                onImageUpload: function (files) {
                    for (let i = 0; i < files.length; i++) {
                        var id = $(this).attr('id');
                        //FileUploadHelper.UploadSummerNoteImage
                        UploadSummerNoteImage(files[i], id); eImage
                    }
                }
            }
        });

        // Air mode
        $('.summernote-airmode').summernote({
            airMode: true,
            callbacks: {
                onImageUpload: function (files) {
                    for (let i = 0; i < files.length; i++) {
                        var id = $(this).attr('id');
                        //FileUploadHelper.UploadSummerNoteImage
                        UploadSummerNoteImage(files[i], id); eImage
                    }
                }
            }
        });


        // Click to edit
        // ------------------------------

        // Edit
        $('#edit').on('click', function() {
            $('.click2edit').summernote({focus: true});
        })

        // Save
        $('#save').on('click', function() {
            var aHTML = $('.click2edit').summernote('code');
            $('.click2edit').summernote('destroy');
        });
    };


    //
    // Return objects assigned to module
    //

    return {
        init: function() {
            _componentSummernote();
        }
    }
}();


// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function() {
    Summernote.init();
});
