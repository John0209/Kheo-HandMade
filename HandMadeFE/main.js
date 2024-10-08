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
    const email = document.getElementById('email').value;
    fetch('https://www.handmade.somee.com/api/v1/accounts/recover', {
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
