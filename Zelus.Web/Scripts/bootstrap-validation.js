//// override jquery validate plugin defaults
//$.validator.setDefaults({
//    highlight: function (element) {
//        $(element).closest('.form-group').addClass('has-error');
//    },
//    unhighlight: function (element) {
//        $(element).closest('.form-group').removeClass('has-error');
//    },
//    ignore: '',
//    //errorElement: 'span',
//    errorClass: 'help-block',
//    errorPlacement: function (error, element) {
//        if (element.parents('.input-group').length) {
//            error.insertAfter(element.parents('.input-group')[0]);
//        } else {
//            error.insertAfter(element);
//        }
//    }
//});

//$(function () {
//    addValidationClasses();
//});

//function addValidationClasses() {
//    $("span.field-validation-valid, span.field-validation-error").addClass('help-block');
//    $("div.form-group").has("span.field-validation-error").addClass('has-error');
//    $("div.validation-summary-errors").has("li:visible").addClass("alert alert-block alert-danger");

//    $("form").removeData("validator");
//    $("form").removeData("unobtrusiveValidation");
//    $.validator.unobtrusive.parse("form");
//}

$.validator.setDefaults({
    //showErrors: function (errorMap, errorList) {
    //    if (errorList.length > 0) {
    //        var element = errorList[0].element;
    //        if ($(element).parent().hasClass("k-dropdown"))
    //            $(element).data("kendoDropDownList").focus();
    //        else if ($(element).parent().hasClass("k-numeric-wrap"))
    //            $(element).data("kendoNumericTextBox").focus();
    //        else
    //            element.focus();
    //    }

    //    this.defaultShowErrors();
    //},
    ignore: '',
    errorElement: 'span',
    errorClass: 'help-block',
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorPlacement: function (error, element) {
        if (element.parents('.k-widget').length) {
            error.insertAfter(element.parents('.k-widget')[0]);
        } else {
            error.insertAfter(element);
        }
    }
});