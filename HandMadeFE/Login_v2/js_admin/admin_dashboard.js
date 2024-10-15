document.addEventListener('DOMContentLoaded', function () {
    const btnMenu = document.querySelectorAll('button[name="btnMenu"]');
    btnMenu.forEach(x => {
        x.className = 'btnMenu';
    })
    var btnOrder = document.getElementById('btnNotify');
    btnOrder.className = 'sidebar-select';
})

document.getElementById('btnLogout').addEventListener('click', function () {
    window.location.href = '../login.html';
})
document.getElementById('btnOrder').addEventListener('click', function () {
    window.location.href = '../page_admin/admin_orders.html';
})
document.getElementById('btnNotify').addEventListener('click', function () {
    window.location.href = '../page_admin/admin_dashboard.html';
})

const btnSelect = document.querySelectorAll('button[name="btnSelect"]');
btnSelect.forEach(btn => {
    btn.addEventListener('click', async e => {
        changeBorderStatus(btn);
    });
});

const border_Status = document.querySelectorAll('div[name="border_Status"]');
const ORDER_STATUS_BTN = {
    ALL: 'btnAll',
    PROCESS: 'btnProcess',
    CONFIRM: 'btnConfirm',
    DELIVERY: 'btnDelivery',
    SUCCESS: 'btnSuccess',
    FAILED: 'btnFail',
}
function changeBorderStatus(btn) {
    
    border_Status.forEach(x => {
        x.className = '';
    });
    var line = '';

    switch (btn.id) {
        case ORDER_STATUS_BTN.PROCESS:
            line = document.getElementById('process');
            break;
        case ORDER_STATUS_BTN.CONFIRM:
            line = document.getElementById('confirm');
            break;
        case ORDER_STATUS_BTN.DELIVERY:
            line = document.getElementById('delivery');
            break;
        case ORDER_STATUS_BTN.SUCCESS:
            line = document.getElementById('success');
            break;
        case ORDER_STATUS_BTN.FAILED:
            line = document.getElementById('fail');
            break;
    }
    line.className = 'border_Order';
}

document.addEventListener("DOMContentLoaded", function() {
    // Call API để lấy thông tin đơn hàng
    fetch('https://www.handmade.somee.com/api/v1/dashboard', {
      method: 'GET',
      headers: {
        'accept': 'application/json'
      }
    })
    .then(response => response.json())
    .then(data => {
      // Đặt tổng số đơn hàng
      document.querySelector('#total_number h4').textContent = data.orderTotal || 0;
      document.querySelector('#totalIncome').textContent = 'Tổng thu nhập: ' + data.moneyTotal || 0;

      // Biến để lưu số lượng từng trạng thái
      let successCount = 0, failCount = 0, deliveringCount = 0, confirmingCount = 0, processingCount = 0;

      // Xử lý từng đơn hàng và gán giá trị tương ứng cho các trạng thái
      data.orders.forEach(order => {
        switch (order.status) {
          case 'Success':
            successCount = order.orderStatusTotal;
            break;
          case 'Failed':
            failCount = order.orderStatusTotal;
            break;
          case 'Delivering':
            deliveringCount = order.orderStatusTotal;
            break;
          case 'Confirming':
            confirmingCount = order.orderStatusTotal;
            break;
          case 'Processing':
            processingCount = order.orderStatusTotal;
            break;
        }
      });

      // Gán giá trị vào các thẻ h4
      document.querySelector('#success_number h4').textContent = successCount;
      document.querySelector('#fail_number h4').textContent = failCount;
      document.querySelector('#delivery_number h4').textContent = deliveringCount;
      document.querySelector('#confirm_number h4').textContent = confirmingCount;
      document.querySelector('#process_number h4').textContent = processingCount;
    })
    .catch(error => {
      console.error('Error fetching order data:', error);
    });

    // Call API để lấy thông tin sản phẩm
    fetch('https://handmade.somee.com/api/v1/products?status=Stocking', {
      method: 'GET',
      headers: {
        'accept': 'application/json'
      }
    })
    .then(response => response.json())
    .then(products => {
      const table = document.createElement('table');
      const thead = `
        <thead>
          <tr>
            <th>Product Name</th>
            <th>Quantity</th>
            <th>Price (VND)</th>
            <th>Seller Name</th>
          </tr>
        </thead>
      `;
      table.innerHTML = thead;

      const tbody = document.createElement('tbody');

      // Vẽ các dòng dữ liệu vào bảng
      products.forEach(product => {
        const row = document.createElement('tr');
        row.innerHTML = `
          <td>${product.productName}</td>
          <td>${product.quantity}</td>
          <td>${product.price}</td>
          <td>${product.sellerName}</td>
        `;
        tbody.appendChild(row);
      });

      table.appendChild(tbody);

      // Thêm bảng vào phần dưới của thẻ h5
      document.querySelector('.title_product').appendChild(table);
    })
    .catch(error => {
      console.error('Error fetching product data:', error);
    });
  });