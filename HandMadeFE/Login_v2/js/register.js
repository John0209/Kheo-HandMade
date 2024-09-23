

(function ($) {
    "use strict";


    /*==================================================================
    [ Focus input ]*/
    $('.input100').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })
    })


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit', function () {
        var check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }

        return check;
    });


    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if ($(input).val().trim() == '') {
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }

    /*==================================================================
    [ Show pass ]*/
    var showPass = 0;
    $('.btn-show-pass').on('click', function () {
        if (showPass == 0) {
            $(this).next('input').attr('type', 'text');
            $(this).find('i').removeClass('zmdi-eye');
            $(this).find('i').addClass('zmdi-eye-off');
            showPass = 1;
        }
        else {
            $(this).next('input').attr('type', 'password');
            $(this).find('i').addClass('zmdi-eye');
            $(this).find('i').removeClass('zmdi-eye-off');
            showPass = 0;
        }

    });

    async function Register() {
        try {

            const data = {
                email: document.getElementById('email').value,
                password: document.getElementById('pass').value,
                name: document.getElementById('name').value,
                birthDate: document.getElementById('birthday').value,
                phoneNumber: document.getElementById('phone').value,
            };
            console.log(JSON.stringify(data));

            //Call API  
            const response = await fetch('https://www.handmade.somee.com/api/v1/accounts/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: document.getElementById('email').value,
                    password: document.getElementById('pass').value,
                    name: document.getElementById('name').value,
                    birthDate: document.getElementById('birthday').value,
                    phoneNumber: document.getElementById('phone').value,
                })
            });

            const status = response.status;
            if (status >= 200 && status < 300) {
                const form = document.getElementById('verify-form');
                form.style.display = 'block';
            } else if (status == 409) {
                alert('Email đã được đăng ký');
            } else {
                alert('Đăng ký tài khoản thất bại');
            }



        } catch (error) {
            alert(error)
        }
    }

    async function VerifyAccount() {
        try {
            const response = await fetch('https://www.handmade.somee.com/api/v1/accounts/verify', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: document.getElementById('email').value,
                    code: document.getElementById('code').value
                })
            });
            const status = response.status;

            if (status >= 200 && status < 300) {
                alert('Xác thực tài khoản thành công');
                window.location.href = '../Login_v2/login.html';
            } else {
                alert('Mã xác thực sai, vui lòng nhập lại');
            }

        } catch (error) {
            alert(error)
        }
    }
    // gắn event cho button login

    document.getElementById('btnRegister').addEventListener('click', async function (e) {
        e.preventDefault();
        Register();
    }, true);

    document.getElementById('btnVerify').addEventListener('click', async function (e) {
        e.preventDefault();
        VerifyAccount();
    }, true);


})(jQuery);



