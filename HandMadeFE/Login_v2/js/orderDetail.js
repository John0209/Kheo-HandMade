const COLOR_STATUS = {
    Processing: {
        color: '#988e8ee1',
        name: 'Đợi Thanh Toán'
    },
    Confirming: {
        color: '#1A9CB7',
        name: 'Đợi Xác Nhận'
    },
    Delivering: {
        color: '#FF8A00',
        name: 'Đang Giao Hàng'
    },
    Success: {
        color: '#6DCE63',
        name: 'Giao Thành Công'
    },
    Failed: {
        color: '#F11616',
        name: 'Đơn Đã Hủy'
    }
};
const PAYMENT_TYPE = {
    Momo: {
        image: '../images/momo.png',
        title: 'Thanh toán bằng ví Momo'
    },
    Cash: {
        image: '../images/cash.png',
        title: 'Thanh toán khi nhận hàng'
    }
}
var status_color = '';
var status_name = '';
var image_type = '';
var title_type = '';

let account = JSON.parse(sessionStorage.getItem('account'));
let html = '';
let product = '';
let product_name = '';
let product_status_div = '';


async function LoadOrders(data, order) {
    data.orderDetails.forEach(x => {
        product += ` <div id="order-product">
                            <img src="${x.productImage}" alt="">
                            <div>
                                <p>Tên sản phẩm</p>
                                <a >${x.productName}</a>
                            </div>
                            <div>
                                <p>Số lượng đã mua</p>
                                <span>${x.orderQuantity}</span>
                            </div>
                            <div id="price">
                                <p>Giá sản phẩm</p>
                                <span>${formatCurrency(x.productPrice)}</span>
                            </div>
    
                        </div>`
        product_name += (x.productName + ". ");
    });

    getBtnProduct(data);

    getColorAndPayment(data);

    html = ` <h3>Chi tiết đơn hàng</h3>

                <div id="order-infor">
                    <div id="order-code">
                        <div style="display: flex;">
                            <i class="fa-solid fa-box"></i>
                            <p>Mã đơn hàng: </p>
                            <p id="code">${data.orderCode}</p>
                        </div>
                        <div id="status" style="background-color: ${status_color}">
                            <span>${status_name}</span>
                        </div>
                    </div>
                    <hr id="order-hr">
                    ${product}
                </div>

                <div class="order-background" id="order-payment">
                    <div style="display: flex;">
                        <p>Ngày đặt hàng: </p>
                        <span>${data.orderDate}</span>
                    </div>
                    <div id="payment">
                        <img src="${image_type}">
                        <p>${title_type}</p>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6">
                        <div class="order-background" id="order-address">
                            <h5>Địa chỉ giao hàng</h5>
                            <div id="address-contact">
                                <p>Anh Vũ</p>
                                <span>0397528860</span>
                            </div>
                            <div id="address-infor">
                                <div id="home">NHÀ RIÊNG</div>
                                <p>số 16 đường 4-4A, Xã Xuân Thới Thượng, Huyện Hóc Môn, Hồ Chí Minh</p>
                            </div>
                            <h5>Hình thức giao hàng</h5>
                            <div id="ship-fee">
                                <div style="display: flex;">
                                    <i class="fa-solid fa-circle-check"></i>
                                    <div>
                                        <p>10.000 đ</p>
                                        <p>Giao hàng tiêu chuẩn</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="order-background" id="order-address">
                            <h5>Thông tin đơn hàng</h5>
                            <div class="order-detail">
                                <p>Tên sản phẩm</p>
                                <span>${product_name}</span>
                            </div>
                            <div class="order-detail">
                                <p>Tổng tiền </p>
                                <span>${formatCurrency(data.priceTotal - 10000)}</span>
                            </div>
                            <div class="order-detail">
                                <p>Tổng số sản phẩm</p>
                                <span>x${data.quantityTotal}</span>
                            </div>
                            <div class="order-detail">
                                <p>Phí vận chuyển</p>
                                <span>${formatCurrency(10000)}</span>
                            </div>
                            <hr>
                            <div class="order-detail">
                                <p>Tổng cộng (đã cộng trừ tất cả phí)</p>
                                <h4>${formatCurrency(data.priceTotal)}</h4>
                            </div>
                            ${product_status_div}
                        </div>
                    </div>
                </div>`
    order.innerHTML = html;
}

async function AdminLoadOrders(data, order) {
    data.orderDetails.forEach(x => {
        product += ` <div id="order-product">
                            <img src="${x.productImage}" alt="">
                            <div>
                                <p>Tên sản phẩm</p>
                                <a >${x.productName}</a>
                            </div>
                            <div>
                                <p>Số lượng đã mua</p>
                                <span>${x.orderQuantity}</span>
                            </div>
                            <div id="price">
                                <p>Giá sản phẩm</p>
                                <span>${formatCurrency(x.productPrice)}</span>
                            </div>
    
                        </div>`
        product_name += (x.productName + ". ");
    });

    getBtnProduct(data);

    getColorAndPayment(data);

    html = ` 
             <div id="title">
                        <div>
                            <h5>CHI TIẾT ĐƠN HÀNG</h5>
                            <div id="line"></div>
                        </div>
                        <i class="fa-solid fa-box-open"></i>
                    </div>

 <div class="order-customer background">
                        <div style="display: flex;">
                            <i class="fa-solid fa-user-tie"></i>
                            <p>Khách hàng: </p>
                            <span id="code">${data.customerName}</span>
                        </div>
                        <div id="status" style="background-color: ${status_color}">
                            <span>${status_name}</span>
                        </div>
                    </div>

                  <div class="order-information background">
                        <div id="order-code">
                            <div style="display: flex;">
                                <i class="fa-solid fa-box"></i>
                                <p>Mã đơn hàng: </p>
                                <p id="code">${data.orderCode}</p>
                            </div>
                        </div>

                        <hr id="order-hr">
                        
                         ${product}
                    </div>

                <div  class="order-background-admin background" id="order-payment">
                    <div style="display: flex;">
                        <p>Ngày đặt hàng: </p>
                        <span>${data.orderDate}</span>
                    </div>
                    <div id="payment">
                        <img src="${image_type}">
                        <p>${title_type}</p>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6">
                        <div class="order-address background" id="order-address">
                            <h5>Địa chỉ giao hàng</h5>
                            <div id="address-contact">
                                <p>Anh Vũ</p>
                                <span>0397528860</span>
                            </div>
                            <div id="address-infor">
                                <div id="home">NHÀ RIÊNG</div>
                                <p>số 16 đường 4-4A, Xã Xuân Thới Thượng, Huyện Hóc Môn, Hồ Chí Minh</p>
                            </div>
                            <h5>Hình thức giao hàng</h5>
                            <div id="ship-fee">
                                <div style="display: flex;">
                                    <i class="fa-solid fa-circle-check"></i>
                                    <div>
                                        <p>10.000 đ</p>
                                        <p>Giao hàng tiêu chuẩn</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="order-infor-detail background" id="order-address">
                            <h5>Thông tin đơn hàng</h5>
                            <div class="order-detail">
                                <p>Tên sản phẩm</p>
                                <span>${product_name}</span>
                            </div>
                            <div class="order-detail">
                                <p>Tổng tiền </p>
                                <span>${formatCurrency(data.priceTotal - 10000)}</span>
                            </div>
                            <div class="order-detail">
                                <p>Tổng số sản phẩm</p>
                                <span>x${data.quantityTotal}</span>
                            </div>
                            <div class="order-detail">
                                <p>Phí vận chuyển</p>
                                <span>${formatCurrency(10000)}</span>
                            </div>
                            <hr>
                            <div class="order-detail">
                                <p>Tổng cộng (đã cộng trừ tất cả phí)</p>
                                <h4>${formatCurrency(data.priceTotal)}</h4>
                            </div>

                            ${product_status_div}
                        </div>
                    </div>
                </div>`
    order.innerHTML = html;
}


document.addEventListener("DOMContentLoaded", async function () {

    var data = await LoadOrderDetails();
    var order = '';
    html = '';

    if (account.email === 'admin@gmail.com') {
        order = document.getElementById('order-details');
        await AdminLoadOrders(data, order);
    } else {
        order = document.getElementById('order');
        await LoadOrders(data, order);
    }
})

async function LoadOrderDetails() {
    const queryString = location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id');

    const response = await fetch(`https://handmade.somee.com/api/v1/orders/id?id=${id}`);
    return await response.json();
}
function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}
function getColorAndPayment(data) {
    status_color = COLOR_STATUS[data.status].color;
    status_name = COLOR_STATUS[data.status].name;
    title_type = PAYMENT_TYPE[data.paymentType].title;
    image_type = PAYMENT_TYPE[data.paymentType].image;
}

function getBtnProduct(data) {
    if (account.email === 'admin@gmail.com') {
        switch (data.status) {
            case 'Confirming':

                product_status_div += `  <div id="order-cancelBtn">
                            <button id="${data.id}-Delivering" class="btnDelivery btnEffect">VẬN CHUYỂN ĐƠN HÀNG</button>
                        </div>
            <div id="order-cancelBtn">
                            <button id="${data.id}-Failed" class="btnCancel btnEffect">HỦY ĐƠN HÀNG</button>
                        </div>`
                break;
            case 'Delivering':
                product_status_div += `  <div id="order-cancelBtn">
                                <button id="${data.id}-Success" class="btnSuccess btnEffect">GIAO THÀNH CÔNG</button>
                            </div>
                <div id="order-cancelBtn">
                                <button id="${data.id}-Failed" class="btnFailed btnEffect">GIAO THẤT BẠI</button>
                            </div>`
                break;
            default:
                product_status_div = '';
        }
    }
    else {
        switch (data.status) {
            case 'Confirming':
                product_status_div += ` 
            <div id="order-cancelBtn">
                            <button id="${data.id}-Failed" class="btnCancel btnEffect" >HỦY ĐƠN HÀNG</button>
                        </div>`
                break;
            case 'Processing':
                product_status_div += `  
                <div id="order-cancelBtn">
                                <button id="${data.id}-Payment" class="btnPayment btnEffect" >THANH TOÁN ĐƠN HÀNG</button>
                            </div>`
                break;
            default:
                product_status_div = '';
        }
    }
}

setTimeout(() => {
    const btnProduct = document.querySelectorAll('.btnEffect');
    btnProduct.forEach(x => {
        x.addEventListener('click', async () => {
            console.log(x.id)
            await callAPIChangeStatus(x.id)
        });
    })
}, 1000);

async function callAPIChangeStatus(btn) {
    const part = btn.split('-');
    const id = part[0];
    const status = part[1];
    var response = '';
    switch (status) {
        case 'Payment':
            response = await fetch(`https://handmade.somee.com/api/v1/orders/momo/${id}`, {
                method: 'POST'
            });
            const data = await response.json();
            if (response.status >= 200 && response.status < 300) {
                location.href = data.link;
                break;
            }
            break;
        default:
            response = await fetch(`https://handmade.somee.com/api/v1/orders?code=${id}&status=${status}`,{
                method:'PATCH'
            });
            alert('Yêu cầu đã được chấp nhận')
            location.reload();
    }

}