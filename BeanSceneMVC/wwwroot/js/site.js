// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleDarkMode() {
    const body = document.body;
    body.classList.toggle('hero');
    document.body.classList.toggle('dark-mode');
}