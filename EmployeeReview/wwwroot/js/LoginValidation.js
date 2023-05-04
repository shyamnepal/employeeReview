
$(document).ready(function () {
    $('#LoginForm').validate({
        rules: {
            UserName: {
                required: true,
                minlength: 2
            },
            password: {
                required: true,
                minlength: 6
            },
        },
            
            messages: {
                UserName: {
                required: 'Please enter your user name',
                minlength: 'First name must be at least 2 characters long'
            },

                password: {
                required: 'Please enter your password',
                minlength: ' Password must be at least 6 characters long'
            },
           
        },

        errorElement: "span",

    });
});
