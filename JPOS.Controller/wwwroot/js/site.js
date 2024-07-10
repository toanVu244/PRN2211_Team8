// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadContent(url) {
    $('#content-area').html('<div class="loading">Loading...</div>');
    $('#content-area').load(url, function (response, status, xhr) {
        if (status == "error") {
            $('#content-area').html("<p>Sorry, there was an error loading the content.</p>");
        }
    });
}
