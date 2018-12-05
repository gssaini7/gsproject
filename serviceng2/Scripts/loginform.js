(function ($) {
    "use strict";

    // Options for Message
    //----------------------------------------------
    var options = {
        'btn-loading': '<i class="fa fa-spinner fa-pulse"></i>',
        'btn-success': '<i class="fa fa-check"></i>',
        'btn-error': '<i class="fa fa-remove"></i>',
        'msg-success': 'All Good! Redirecting...',
        'msg-success-fp': 'An email to reset password is sent. Please check your email.',

        'msg-error': 'Wrong login credentials!',
        'useAJAX': true,
    };

    // Login Form
    //----------------------------------------------
    // Validation
    $("#login-form").validate({
        rules: {
            lg_username: "required",
            lg_password: "required",
        },
        errorClass: "form-invalid"
    });

    // Form Submission
    $("#login-form").submit(function () {
        remove_loading($(this));
        if (options['useAJAX'] == true) {
            // Dummy AJAX request (Replace this with your AJAX code)
            // If you don't want to use AJAX, remove this
            //dummy_submit_form($(this));
            loginuser($(this));

            // Cancel the normal submission.
            // If you don't want to use AJAX, remove this
            return false;
        }
    });


    // Resetpassword Form
    //----------------------------------------------
    // Validation
    $("#resetpassword-form").validate({
        rules: {
            rp_password: "required",
            rp_confirmpassword: "required",
        },
        errorClass: "form-invalid"
    });

    // Form Submission
    $("#resetpassword-form").submit(function () {
        remove_loading($(this));
        if (options['useAJAX'] == true) {
            // Dummy AJAX request (Replace this with your AJAX code)
            // If you don't want to use AJAX, remove this
            //dummy_submit_form($(this));
            resetpassword($(this));

            // Cancel the normal submission.
            // If you don't want to use AJAX, remove this
            return false;
        }
    });

    // Register Form
    //----------------------------------------------
    // Validation
    $("#register-form").validate({
        rules: {
            reg_username: "required",
            reg_password: {
                required: true,
                minlength: 5
            },
            reg_password_confirm: {
                required: true,
                minlength: 5,
                equalTo: "#register-form [name=reg_password]"
            },
            reg_email: {
                required: true,
                email: true
            },
            reg_agree: "required",
        },
        errorClass: "form-invalid",
        errorPlacement: function (label, element) {
            if (element.attr("type") === "checkbox" || element.attr("type") === "radio") {
                element.parent().append(label); // this would append the label after all your checkboxes/labels (so the error-label will be the last element in <div class="controls"> )
            }
            else {
                label.insertAfter(element); // standard behaviour
            }
        }
    });

    // Form Submission
    $("#register-form").submit(function () {
        remove_loading($(this));

        if (options['useAJAX'] == true) {
            // Dummy AJAX request (Replace this with your AJAX code)
            // If you don't want to use AJAX, remove this
            dummy_submit_form($(this));

            // Cancel the normal submission.
            // If you don't want to use AJAX, remove this
            return false;
        }
    });

    // Forgot Password Form
    //----------------------------------------------
    // Validation
    $("#forgot-password-form").validate({
        rules: {
            fp_email: "required",
            fp_uid: "required",

        },
        errorClass: "form-invalid"
    });

    // Form Submission
    $("#forgot-password-form").submit(function () {
        remove_loading($(this));

        if (options['useAJAX'] == true) {
            // Dummy AJAX request (Replace this with your AJAX code)
            // If you don't want to use AJAX, remove this
            //dummy_submit_form($(this));
            forgetpassword($(this));
            // Cancel the normal submission.
            // If you don't want to use AJAX, remove this
            return false;
        }
    });

    // Loading
    //----------------------------------------------
    function remove_loading($form) {
        $form.find('[type=submit]').removeClass('error success');
        $form.find('.login-form-main-message').removeClass('show error success').html('');
    }

    function form_loading($form) {
        $form.find('[type=submit]').addClass('clicked').html(options['btn-loading']);
    }

    function form_success($form) {
        $form.find('[type=submit]').addClass('success').html(options['btn-success']);
        $form.find('.login-form-main-message').addClass('show success').html(options['msg-success']);
    }

    function form_success_forgetpaswword($form) {
        $form.find('[type=submit]').addClass('success').html(options['btn-success']);
        $form.find('.login-form-main-message').addClass('show success').html(options['msg-success-fp']);
    }

    function form_failed($form) {
        $form.find('[type=submit]').addClass('error').html(options['btn-error']);
        $form.find('.login-form-main-message').addClass('show error').html(options['msg-error']);
    }

    function form_failed($form, errormsg) {
      
        $form.find('[type=submit]').addClass('error').html(options['btn-error']);
        $form.find('.login-form-main-message').addClass('show error').html(errormsg);
    }

    // Dummy Submit Form (Remove this)
    //----------------------------------------------
    // This is just a dummy form submission. You should use your AJAX function or remove this function if you are not using AJAX.
    function dummy_submit_form($form) {
        if ($form.valid()) {
            form_loading($form);
            
            setTimeout(function () {
                form_success($form);
            }, 2000);
        }
    }

    function loginuser($form) {
       
        if ($form.valid()) {
            form_loading($form);
            //console.log($form.find('#lg_username').val());
            //var loginData = {
            //    grant_type: 'password',
            //    username: $form.find('#lg_username').val(),
            //    password: $form.find('#lg_password').val()
            //};
            var loginData = {
               // grant_type: 'password',
                Mobile: $form.find('#lg_username').val(),
                password: $form.find('#lg_password').val(),
                dbcode: $form.find('#lg_uid').val()

            };
            $.ajax({
                type: 'POST',
                //url: '/Token',
                //url: '/api/account/WebLogin',
                url: '/api/webdetail/WebLogin',
                
                data: loginData
            }).done(function (data) {
                console.log(data);
                
                //self.user(data.userName);
                //// Cache the access token in session storage.  
                //sessionStorage.setItem('userName', data.userName);
                //sessionStorage.setItem('auth_token', data.access_token);
                localStorage.setItem('auth_token', data.access_token);
                localStorage.setItem('dbcodeid', data.dbcodeid);



                //var tkn = sessionStorage.getItem(tokenKey);
                //$("#tknKey").val(tkn);
                window.location.href='/manager';
            }).fail(function (error) {
                //console.log(error.responseJSON);
                
                var errormsg = modelstateerror(error);

                form_failed($form, errormsg);
                
                //form_failed($form, error.responseJSON.error_description)
            });

            //setTimeout(function () {
            //    form_success($form);
            //}, 2000);
        }
    }
    function forgetpassword($form) {

        if ($form.valid()) {
            form_loading($form);
           
            var forgetdata = {
                Email: $form.find('#fp_email').val(),
                dbcode: $form.find('#fp_uid').val()
            };
            $.ajax({
                type: 'POST',
                url: '/api/admin/GetUserByEmail',
                data: forgetdata
            }).done(function (data) {
                form_success_forgetpaswword($form);
            }).fail(function (error) {
                var errormsg = modelstateerror(error);


                form_failed($form, errormsg);
            });
        }
    }

    function resetpassword($form) {

        if ($form.valid()) {
            form_loading($form);
          
            var resetpassworddata = {
                NewPassword: $form.find('#rp_password').val(),
                ConfirmPassword: $form.find('#rp_confirmpassword').val(),
                Useremail: getUrlVars()["user"],
                Uid: getUrlVars()["uid"],
                Uprcode: getUrlVars()["uprcode"],
                tokencode: getUrlVars()["token"],

            };
            $.ajax({
                type: 'POST',
                url: '/api/admin/ResetPassword',
                data: resetpassworddata
            }).done(function (data) {
                form_success($form);
                window.location.href = '/home/account';

            }).fail(function (error) {
                console.log(error);
                var errormsg = modelstateerror(error);
                form_failed($form, errormsg);
            });
        }
    }

    function modelstateerror(error) {
        var errormsg = error.responseJSON.Message;
        if (error.responseJSON.ModelState!=null) {
            var modelState = error.responseJSON.ModelState;
            for (var key in modelState) {
                if (modelState.hasOwnProperty(key)) {
                    errormsg = (errormsg == "" ? "" : errormsg + "<br/>") + modelState[key];
                    //errors.push(modelState[key]);//list of error messages in an array
                    //errormsg += " ;";
                }
            }
        }
        return errormsg;
    }

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

})(jQuery);