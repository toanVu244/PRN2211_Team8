// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#addCategoryForm').submit(function (event) {
        event.preventDefault();

        var formData = $(this).serialize(); // Serialize the form data

        $.ajax({
            type: 'POST',
            url: '/Dashboard/Categories/Index?handler=Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#addCategoryModal').modal('hide');
                    loadContent('/Dashboard/Categories/Index?handler=Partial');
                } else {
                    alert('Error adding category');
                }
            },
            error: function (error) {
                alert('Error adding category');
            }
        });
    });
});


