// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*function toggleLightMode() {
    const body = document.body;
    body.classList.toggle('hero');
    document.body.classList.toggle('light-mode');
}*/

// Function to apply the mode
function applyMode(isLightMode) {
    const body = document.body;
    if (isLightMode) {
        body.classList.remove('hero'); // Apply light mode (black text)
        body.classList.add('light-mode');
        
    } else {
        body.classList.remove('light-mode');
        body.classList.add('hero'); // Apply dark mode (white text)
    }
}

// Function to toggle light mode and save the state
function toggleLightMode() {
    const isLightMode = document.body.classList.contains('light-mode');
    localStorage.setItem('lightMode', JSON.stringify(!isLightMode));
    applyMode(!isLightMode);
}

// Apply mode as soon as the page is loaded
(function () {

    const savedMode = JSON.parse(localStorage.getItem('lightMode'));
    // Default to dark mode (white text) if no mode is set
    const isLightMode = savedMode !== null ? savedMode : false;
    applyMode(isLightMode);
})();