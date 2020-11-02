//Call only end of the form or Div

//<%--Allow Only Numeric using class="Number" --%>
//<script type="text/javascript" lang="javascript" src="Scripts/Number.js"></script>


$(".Number").on("input", function () {
    var regexp = /[^0-9]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});