//Call only end of the form or Div
//<script type="text/javascript" lang="javascript" src="Scripts/TruckNo.js"></script>


$(".TruckNumber").on("input", function () {
    var regexp = /[^a-zA-Z0-9-/]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});