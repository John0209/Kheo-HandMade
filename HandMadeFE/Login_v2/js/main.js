
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


    async function checkLogin() {
        try {
            //Call API
            const response = await fetch('https://www.handmade.somee.com/api/v1/accounts/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: document.getElementById('email').value,
                    password: document.getElementById('pass').value
                })
            });
            //Nhận result from api
            const status = response.status;
            if (status >= 200 && status < 300) {
                const data = await response.json();
                sessionStorage.setItem('account', JSON.stringify(data));
                // Chuyển page theo role
                switch(data.role){
                    case "Seller":
                        window.location.href = '../Login_v2/page_seller/homepage_seller.html'
                    break;
                    case "Admin":
                        window.location.href = '../Login_v2/page_admin/admin_dashboard.html'
                    break;
                    case "Customer":
                        window.location.href = '../Login_v2/pages/homepage.html';
                    break;
                }

            } else {
                alert('Nhập sai email, mật khẩu hoặc tài khoản chưa được xác thực');
            }
        } catch (error) {
            console.log(error)
        }
    }

    // gắn event cho button login

    document.getElementById('btnLogin').addEventListener('click', async function (e) {

        e.preventDefault();

        await checkLogin();

    }, true);

})(jQuery);



document.getElementById('btnForgot').onclick = function() {
    document.getElementById('popup').style.display = 'block';
};

document.getElementsByClassName('close')[0].onclick = function() {
    document.getElementById('popup').style.display = 'none';
};

window.onclick = function(event) {
    if (event.target == document.getElementById('popup')) {
        document.getElementById('popup').style.display = 'none';
    }
};

document.getElementById('sendPassword').onclick = function() {
    const email = document.getElementById('emailPopup').value;
    alert(email)
    fetch('https://www.handmade.somee.com/api/v1/accounts/recover?email='+ email, {
        method: 'POST'
    })
    .then(response => {
        if (response.status >= 200 && response.status < 300) {
            alert('Đã gửi mật khẩu mới tới email của bạn.');
        } else {
            alert('Email chưa được đăng ký hoặc đã xảy ra lỗi, vui lòng thử lại.');
        }
    })
    .catch(error => {
        alert('Đã xảy ra lỗi, vui lòng thử lại.');
    });
};