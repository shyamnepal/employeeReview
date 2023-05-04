$(document).ready(function () {
    $('#form').validate({
        rules: {
            PermanentAddress: {
                required: true
            },
            PermanentCountry: {
                required: true
            },
            PermanentState: {
                required: true
            },
            TemporaryAddress: {
                required: true
            },
            TemporaryCountry: {
                required: true
            },
            TemporaryState: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            ContactNumber: {
                required: true,
                
            },
        },
        messages: {
            PermanentAddress: {
                required: "Please enter your permanent address."
            },
            PermanentCountry: {
                required: "Please enter your permanent country."
            },
            PermanentState: {
                required: "Please enter your permanent state."
            },
            TemporaryAddress: {
                required: "Please enter your temporary address."
            },
            TemporaryCountry: {
                required: "Please enter your temporary country."
            },
            TemporaryState: {
                required: "Please enter your temporary state."
            },
            Email: {
                required: "Please enter your email.",
                email: "Please enter a valid email address."
            },
            ContactNumber: {
                required: "Please enter your contact number"
                
            }

        },
        submitHandler: function (form) {

            form.submit();
        },

          errorElement: "span",
        

       
        
    });
});