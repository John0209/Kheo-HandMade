document.addEventListener("DOMContentLoaded", async function () {
    ORDER_STATUS = 'All';
    await LoadOrders();
});

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
    let html = '';
    let product = '';
    let orders = document.getElementById('orders');
    const data = await getOrdersAPI();

    if(data.length===0){
        html=`<div id="no-order">
                        <h5>Tiếc quá, bạn chưa có đơn hàng nào</h5>
                       <img src="../images/cry.png" alt="">
                    </div>`;
    }else{
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
                product='';
        });
    }
   
    orders.innerHTML = html;
}

function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}

async function getOrdersAPI() {
    const account=JSON.parse(sessionStorage.getItem('account'));
    var response = '';
    switch (ORDER_STATUS) {
        case 'All':
            response = await fetch(`https://handmade.somee.com/api/v1/orders?customerId=${account.customerId}`);
            break;
        default:
            console.log(ORDER_STATUS)
            response = await fetch(`https://handmade.somee.com/api/v1/orders?customerId=${account.customerId}&status=${ORDER_STATUS}`);
            break;
    }
    return await response.json();
}

const btnSelect = document.querySelectorAll('button[name="btnSelect"]');
btnSelect.forEach(btn => {
    btn.addEventListener('click', async e => {
        changeBtnColor(btn);
        getOrderStatus(btn);
        await LoadOrders();
    });
});

function getOrderStatus(btn) {
    switch (btn.id) {
        case 'btnAll':
            ORDER_STATUS = 'All';
            break;
        case 'btnProcess':
            ORDER_STATUS = 'Processing';
            break;
        case 'btnConfirm':
            ORDER_STATUS = 'Confirming';
            break;
        case 'btnDelivery':
            ORDER_STATUS = 'Delivering';
            break;
        case 'btnSuccess':
            ORDER_STATUS = 'Success';
            break;
        case 'btnFail':
            ORDER_STATUS = 'Failed';
            break;
    }
}

function changeBtnColor(btn) {
    btnSelect.forEach(x => {
        x.style.color = "#212121cb";
        x.style.fontWeight = "initial";
    });
    btn.style.color = "#F57224";
    btn.style.fontWeight = "bold";
}
