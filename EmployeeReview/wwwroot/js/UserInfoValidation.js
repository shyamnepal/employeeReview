
    $(document).ready(function () {
        $('#registerForm').validate({
            rules: {
                FirstName: {
                    required: true,
                    minlength: 2
                },
              
                LastName: {
                    required: true,
                    minlength: 2
                },
                DateOfBirth: {
                    required: true
                },
                Gender: {
                    required: true
                },
                MaritalStatus: {
                    required: true
                },
                Religion: {
                    required: true
                },
                BloodGroup:{
                    required: true
                },
            },
            messages: {
                FirstName: {
                    required: 'Please enter your first name',
                    minlength: 'First name must be at least 2 characters long'
                },
                
                LastName: {
                    required: 'Please enter your last name',
                    minlength: 'Last name must be at least 2 characters long'
                },
                DateOfBirth: {
                    required: 'Please enter your date of birth'
                },
                Gender: {
                    required: 'Please select your gender'
                },
                MaritalStatus: {
                    required: 'Please select your marital status'
                },
                Religion: {
                    required: 'Please Select your Religion'
                },
                BloodGroup: {
                    required: 'Please select your Blood Group'
                },
            },

            errorElement: "span",
           
        });
    });
