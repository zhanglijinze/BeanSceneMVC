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
    const button = document.querySelector(".toggle")
    if (isLightMode) {
        body.classList.remove('hero'); // Apply light mode (black text)
        body.classList.add('light-mode');
        button.classList.add('switch')
        button.textContent="Dark mode"
        
    } else {
        body.classList.remove('light-mode');
        body.classList.add('hero'); // Apply dark mode (white text)
        button.classList.remove('switch')
        button.textContent ="Light mode"
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
    const button = document.querySelector(".toggle")
    button.textContent="Dark mode"
    const savedMode = JSON.parse(localStorage.getItem('lightMode'));
    // Default to dark mode (white text) if no mode is set
    const isLightMode = savedMode !== null ? savedMode : false;
    applyMode(isLightMode);
})();

//date picker


   /* $(function() {
        $("#StartDatePicker").datepicker({
            dateFormat: "yy-mm-dd", // Adjust date format as needed
        });
    $("#EndDatePicker").datepicker({
        dateFormat: "yy-mm-dd" // Adjust date format as needed
        });
    });*/

$(function () {
    $("#StartDatePicker").datepicker({
        dateFormat: "yy-mm-dd", // Adjust date format as needed
        onClose: function (selectedDate) {
            // Set the minDate or maxDate for the EndDatePicker
            var maxEndDate = new Date(Date.parse(selectedDate));
            maxEndDate.setDate(maxEndDate.getDate() + 14); // Add 14 days
            $("#EndDatePicker").datepicker("option", "minDate", selectedDate);
            $("#EndDatePicker").datepicker("option", "maxDate", maxEndDate);

            // If only start date is selected, set end date to 14 days later
            if ($("#EndDatePicker").val() === '') {
                $("#EndDatePicker").datepicker("setDate", maxEndDate);
            }
        }
    });

    $("#EndDatePicker").datepicker({
        dateFormat: "yy-mm-dd", // Adjust date format as needed
        onClose: function (selectedDate) {
            // Set the maxDate for the StartDatePicker
            var minStartDate = new Date(Date.parse(selectedDate));
            minStartDate.setDate(minStartDate.getDate() - 14); // Subtract 14 days
            $("#StartDatePicker").datepicker("option", "maxDate", selectedDate);
            $("#StartDatePicker").datepicker("option", "minDate", minStartDate);

            // If only end date is selected, set start date to 14 days earlier
            if ($("#StartDatePicker").val() === '') {
                $("#StartDatePicker").datepicker("setDate", minStartDate);
            }
        }
    });
});

$(function () {
    $("#SelectedDatePicker").datepicker({
        dateFormat: "yy-mm-dd" // Adjust date format as needed
    });
});
