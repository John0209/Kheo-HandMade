
document.addEventListener("DOMContentLoaded", async function () {
    ORDER_STATUS = 'All';
    if (account.email === 'admin@gmail.com') {
        await AdminLoadOrders();
    } else {
        await LoadOrders();
    }
});
let account = JSON.parse(sessionStorage.getItem('account'));
let html = '';
let ORDER_STATUS = ''
const COLOR_STATUS = {
    Processing: {
        color: '#988e8ee1',
        name: 'Đợi thanh toán'
    },
    Confirming: {
        color: '#1A9CB7',
        name: 'Đợi xác nhận'
    },
    Delivering: {
        color: '#FF8A00',
        name: 'Đang giao'
    },
    Success: {
        color: '#6DCE63',
        name: 'Thành công'
    },
    Failed: {
        color: '#F11616',
        name: 'Thất bại'
    }
};
var color_status = '';
var name_status = '';

async function LoadOrders() {
    let product = '';
    let orders = document.getElementById('orders');
    const data = await getOrdersAPI();

    if (data.length === 0) {
        html = `<div id="no-order">
                        <h5>Tiếc quá, bạn chưa có đơn hàng nào</h5>
                       <img src="../images/cry.png" alt="">
                    </div>`;
    } else {
        html='';
        data.forEach(x => {
            x.orderDetails.forEach(z => {

                const status = x.status;
                color_status = COLOR_STATUS[status].color;
                name_status = COLOR_STATUS[status].name;

                product += `
                 <div id="order-product">
                                            <img src="${z.productImage}" alt="">
                                            <div>
                                                <p>Tên sản phẩm</p>
                                                <a href="../pages/orderDetail.html?id=${x.id}">${z.productName}</a>
                                            </div>
                                        </div>`
            })

            html += `
             <div id="order-infor">
                            <div id="order-code">
                                <div style="display: flex;">
                                    <i class="fa-solid fa-box"></i>
                                    <p>Mã đơn hàng: </p>
                                    <p id="code">${x.orderCode}</p>
                                </div>
                                <div id="status" style="background-color: ${color_status}">
                                    <span>${name_status}</span>
                                </div>
                            </div>
                            <hr id="order-hr">
                            <div id="order-details">
                                <div class="row">
                                    <div class="col-sm-5" id="order-product-infor">
                                        ${product}
                                    </div>
    
                                    <div class="col-sm-1">
                                        <div id="col-hr">
                                        </div>
                                    </div>
    
                                    <div class="col-sm-6" id="order-total-infor">
                                        <div id="order-product">
                                            <div>
                                                <p>Tổng số sản phẩm</p>
                                                <span>${x.quantityTotal}</span>
                                            </div>
                                            <div id="price">
                                                <p>Tổng cộng</p>
                                                <span>${formatCurrency(x.priceTotal)}</span>
                                            </div>
                                        </div>
                                    </div>
    
                                </div>
                            </div>
                        </div>
                `
            product = '';
        });
    }

    orders.innerHTML = html;
}
async function AdminLoadOrders() {
    var admin_orders = document.getElementById('admin_orders');
    var total_order = document.getElementById('total_order');
    const data = await adminGetOrdersAPI();
    total_order.innerHTML = ` <p>Tổng Số Đơn Hàng</p>
                            <span>${data.totalOrder}</span>`;
    if (data.orders.length === 0) {
        html = `<div id="no-order">
                        <h5>Chưa có đơn hàng nào ở trạng thái này</h5>
                       <img src="../images/cry.png" alt="">
                    </div>`;
    } else {
        html = '';
        data.orders.forEach(x => {
            const status = x.status;
            color_status = COLOR_STATUS[status].color;
            name_status = COLOR_STATUS[status].name;

            html += `<div id="order_information">
                        <h5>${x.customerName}</h5>
                            <p>${x.orderCode}</p>
                            <h4>${formatCurrency(x.total)}</h4>
                            <span style="color: ${color_status};">${name_status}</span>
                            <div id="viewDetail">
                            <a href="../page_admin/admin_orderDetail.html"> Xem chi tiết</a>
                            </div>
                             </div>`
        });
    }
    admin_orders.innerHTML = html;
}

function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}

async function getOrdersAPI() {
    var response = '';
    switch (ORDER_STATUS) {
        case 'All':
            response = await fetch(`https://handmade.somee.com/api/v1/orders?customerId=${account.customerId}`);
            break;
        default:
            response = await fetch(`https://handmade.somee.com/api/v1/orders?customerId=${account.customerId}&status=${ORDER_STATUS}`);
            break;
    }
    return await response.json();
}
async function adminGetOrdersAPI() {
    var response = '';
    switch (ORDER_STATUS) {
        case 'All':
            response = await fetch(`https://handmade.somee.com/api/v1/orders/admin?status=Processing`);
            break;
        default:
            response = await fetch(`https://handmade.somee.com/api/v1/orders/admin?status=${ORDER_STATUS}`);
            break;
    }
    return await response.json();
}
const btnSelect = document.querySelectorAll('button[name="btnSelect"]');
btnSelect.forEach(btn => {
    btn.addEventListener('click', async e => {
        changeBtnColor(btn);
        getOrderStatus(btn);
        if (account.email === 'admin@gmail.com') {
            changeLineStatus(btn);
            await AdminLoadOrders();
        } else {
            await LoadOrders();
        }
    });
});

function getOrderStatus(btn) {
    switch (btn.id) {
        case ORDER_STATUS_BTN.ALL:
            ORDER_STATUS = 'All';
            break;
        case ORDER_STATUS_BTN.PROCESS:
            ORDER_STATUS = 'Processing';
            break;
        case ORDER_STATUS_BTN.CONFIRM:
            ORDER_STATUS = 'Confirming';
            break;
        case ORDER_STATUS_BTN.DELIVERY:
            ORDER_STATUS = 'Delivering';
            break;
        case ORDER_STATUS_BTN.SUCCESS:
            ORDER_STATUS = 'Success';
            break;
        case ORDER_STATUS_BTN.FAILED:
            ORDER_STATUS = 'Failed';
            break;
    }
}

const lineStatus = document.querySelectorAll('div[name="lineStatus"]');

function changeBtnColor(btn) {
    btnSelect.forEach(x => {
        x.style.color = "#212121cb";
        x.style.fontWeight = "initial";
    });
    btn.style.color = "#1A9CB7";
    btn.style.fontWeight = "bold";
}

const ORDER_STATUS_BTN = {
    ALL: 'btnAll',
    PROCESS: 'btnProcess',
    CONFIRM: 'btnConfirm',
    DELIVERY: 'btnDelivery',
    SUCCESS: 'btnSuccess',
    FAILED: 'btnFail',
}
function changeLineStatus(btn) {
    lineStatus.forEach(x => {
        x.className = '';
    });
    var line = '';

    switch (btn.id) {
        case ORDER_STATUS_BTN.PROCESS:
            line = document.getElementById('process');
            line.className = 'lineProcess';
            break;
        case ORDER_STATUS_BTN.CONFIRM:
            line = document.getElementById('confirm');
            line.className = 'lineConfirm';
            break;
        case ORDER_STATUS_BTN.DELIVERY:
            line = document.getElementById('delivery');
            line.className = 'lineDelivery';
            break;
        case ORDER_STATUS_BTN.SUCCESS:
            line = document.getElementById('success');
            line.className = 'lineSuccess';
            break;
        case ORDER_STATUS_BTN.FAILED:
            line = document.getElementById('fail');
            line.className = 'lineFail';
            break;
    }
}

