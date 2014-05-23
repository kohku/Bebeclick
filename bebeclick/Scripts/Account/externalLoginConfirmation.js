// Use jQuery UI date picker for any input[type='date'] fields, if the browser doesn't natively support it
$(document).ready(function () {
    var datefields = $("input[type='date']");

    if (!Modernizr.inputtypes.date) {
        datefields
            .datepicker()
            .each(function () {
                this.type = "text";
            });
    }
});