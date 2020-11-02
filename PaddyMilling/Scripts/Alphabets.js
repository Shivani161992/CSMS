//Call only end of the form or Div
//<%--Allow Only Alphabets using class="alphaOnly" --%>
//<script type="text/javascript" lang="javascript" src="Scripts/Alphabets.js"></script>


$(".alphaOnly").on("input", function () {
    var regexp = /[^a-zA-Z ]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});

$(".alphaNumeric").on("input", function () {
    var regexp = /[^a-zA-Z0-9-/& ]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});

$(".alphaNumericWithoutSpace").on("input", function () {
    var regexp = /[^a-zA-Z0-9-/&]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});

$(".alphaNumericWithSpecial").on("input", function () {
    var regexp = /[^a-zA-Z0-9& ]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});


