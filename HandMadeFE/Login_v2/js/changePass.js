document.getElementById('btnLogin').addEventListener('click', function (event) {
    // Ngăn chặn form submit mặc định
    event.preventDefault();

    // Lấy giá trị mật khẩu mới và xác nhận mật khẩu
    var newPassword = document.getElementById('newPass').value;
    var confirmPassword = document.getElementById('confirmPass').value;

    // Kiểm tra xem mật khẩu mới và xác nhận mật khẩu có khớp nhau không
    if (newPassword !== confirmPassword) {
        alert('Mật khẩu không trùng khớp!');
        return;
    }

    // Lấy email từ sessionStorage
    var account = sessionStorage.getItem('account');
    if (!account) {
        alert('Không tìm thấy thông tin tài khoản.');
        return;
    }
    // Chuyển đổi dữ liệu account từ JSON
    var user = JSON.parse(account);

    // Tạo đối tượng dữ liệu để gửi
    var data = {
        userId: user.userId,
        password: newPassword
    };

    // Gửi yêu cầu API thay đổi mật khẩu
    fetch('https://www.handmade.somee.com/api/v1/accounts/change-pass', {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => response.json())
    .then(result => {
        if (result.success) {
            alert('Đổi mật khẩu thành công!');
        } else {
            alert('Đổi mật khẩu thất bại: ' + result.message);
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Có lỗi xảy ra khi đổi mật khẩu.');
    });
});
