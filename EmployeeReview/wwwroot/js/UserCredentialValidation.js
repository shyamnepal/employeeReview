$(document).ready(function () {
    $('#form').validate({
        rules: {
            UserName: {
                required: true
            },
            passwordHash: {
                required: true
            },
            confirmPassword: {
                required: true,
                equalTo: "#passwordHash"
            }
        },
        messages: {
            UserName: {
                required: "Please enter a user name"
            },
            passwordHash: {
                required: "Please enter a password"
            },
            confirmPassword: {
                required: "Please confirm your password",
                equalTo: "Passwords do not match"
            }
        },
        errorElement: "span"


    });
});