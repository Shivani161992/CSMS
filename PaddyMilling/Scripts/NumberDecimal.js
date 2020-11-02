
// JQUERY ".Class" SELECTOR.
//$(document).ready(function () {

//    $('.NumberDecimal').keypress(function (event) {

//        return isNumber(event, this)

//    });

//});
//// THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
//function isNumber(evt, element) {

//    var charCode = (evt.which) ? evt.which : event.keyCode

//    if (
//        //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
//        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
//        (charCode < 48 || charCode > 57))
//        return false;

//    return true;

//}




//Call only end of the form or Div
//<%--Allow Only Numeric & Decimal using class="NumberDecimal" --%>
//<script type="text/javascript" lang="javascript" src="Scripts/NumberDecimal.js"></script>

$(".NumberDecimal").on("input", function () {
    var regexp = /[^0-9.]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});


