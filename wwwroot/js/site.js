// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.imgIndex').hover
(
    function ()
    {
        $(this).animate({ 'zoom': 1.2 }, 700); //questa parte ingrandisce in 700 millisecondi
    },
    function ()
    {
        $(this).animate({ 'zoom': 1 }, 400);
    }
)