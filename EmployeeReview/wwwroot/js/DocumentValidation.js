$(document).ready(function () {
    $('#form').validate({
        rules: {
            CitizenshipNumber: {
                required: true
            },
            CitizenshipIssuePlace: {
                required: true
            },
            AcademicQualification: {
                required: true
            },
            SelectDocType: {
                required: true
            },
          
        },
        messages: {
            CitizenshipNumber: {
                required: 'Please enter your citizenship number.'
            },
            CitizenshipIssuePlace: {
                required: 'Please enter the issue place of your citizenship.'
            },
            AcademicQualification: {
                required: 'Please enter your academic qualification.'
            },
            SelectDocType: {
                required: 'Please select a document type.'
            },
          
        },
        submitHandler: function (form) {

            form.submit();
        },

        errorElement: "span"
    });
});