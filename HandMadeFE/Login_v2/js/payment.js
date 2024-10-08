const query = new URLSearchParams(location.search);
const data = decodeURIComponent(query.get('data'));
let cartProducts = JSON.parse(data);

let productsHTML = document.getElementById('products');
let quantityHTML = document.getElementById('quantity');
let priceHTML = document.getElementById('price');
let totalHTML = document.getElementById('total');
let html = '';
let quantity = 0;
let price = 0;
let total = 0;
let products = [];


document.addEventListener('DOMContentLoaded', LoadProducts);

function LoadProducts() {
    cartProducts.map(x => {
        html += `<div id="product-item">
                    <img src="${x.picture}">
                        <p>${x.productName}</p>
                        <span>${formatCurrency(x.price)}</span>
                        <p>Số lượng: ${x.quantity}</p></div> `;

        quantity += x.quantity;
        price += (x.price * x.quantity);

        const product = {
            productId: x.productId,
            productQuantity: x.quantity,
            productPrice: x.price
        };

        products.push(product);
    });
    productsHTML.innerHTML = html;
    updateDOM(quantity, price);

    const account = JSON.parse(sessionStorage.getItem('account'));
    const customerNameElement = document.querySelector('.customerName');
    customerNameElement.textContent = account.name;
}
function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}
function updateDOM(quantity, price) {
    quantityHTML.innerText = quantity;
    priceHTML.innerText = formatCurrency(price);
    total = price + 10000;
    totalHTML.innerText = formatCurrency(total);
}
const PaymentType = {
    MOMO: 'momo-pay',
    CASH: 'cash-pay'
}
let type = '';

const radios = document.querySelectorAll('input[name="pay-selected"]');
radios.forEach(radio => {
    radio.addEventListener('click', () => {
        const parentDiv = radio.parentElement.parentElement;

        document.querySelectorAll('#momo').forEach(x => {
            x.style.border = '1px solid #DADADA';
        })

        parentDiv.style.border = "2px solid #F57224";
        if (radio.id === PaymentType.MOMO) {
            type = 'Momo';
        } else {
            type = 'Cash';
        }
    });
});

var orderBtn = document.getElementById('confirm');
orderBtn.addEventListener('click', createOrder);
async function createOrder() {
    if (type === '') return alert('Vui lòng chọn hình thức thanh toán');

    const account=JSON.parse(sessionStorage.getItem('account'));
    try {
        const response = await fetch(`https://www.handmade.somee.com/api/v1/orders?type=${type}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                customerId: account.id,
                quantity: quantity,
                total: total,
                products: products
            })
        });

        const data = await response.json();
        console.log(data);
        const status = response.status;
        if (status >= 200 && status < 300) {
            switch (type) {
                case 'Momo':
                    location.href = data.link;
                    break;
                case 'Cash':
                    location.href = "../pages/success.html";
                    break;
            }
        } else {
            alert('Thanh toán thật bại, vui lòng làm lại');
        }

    } catch (error) {
        alert(error)
    }
}
