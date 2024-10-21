document.addEventListener("DOMContentLoaded", function () {
    // Lấy account.Id từ session
    const account = JSON.parse(sessionStorage.getItem("account"));
    const sellerId = account.id
    // Call API để lấy danh sách đơn hàng
    const apiUrl = `https://www.handmade.somee.com/api/v1/dashboard/seller/orders/${sellerId - 1}`;
    
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            if (Array.isArray(data)) {
                renderOrdersTable(data);
            } else {
                console.error("Invalid data format.");
            }
        })
        .catch(error => console.error('Error fetching orders:', error));
});

// Function để render bảng hiển thị đơn hàng
function renderOrdersTable(orders) {
    const ordersDiv = document.getElementById("orders");

    const table = document.createElement("table");
    table.className = "orders-table";

    // Tạo tiêu đề bảng
    const thead = document.createElement("thead");
    thead.innerHTML = `
        <tr>
            <th>Product Name</th>
            <th>Quantity</th>
            <th>Total</th>
            <th>Order Date</th>
            <th>Status</th>
        </tr>
    `;
    table.appendChild(thead);

    // Tạo thân bảng từ dữ liệu đơn hàng
    const tbody = document.createElement("tbody");
    orders.forEach(order => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${order.productName}</td>
            <td>${order.quantity}</td>
            <td>${order.total.toLocaleString()}</td>
            <td>${order.orderDate}</td>
            <td>${order.status}</td>
        `;
        tbody.appendChild(row);
    });
    table.appendChild(tbody);
    ordersDiv.appendChild(table);
}
