const query = new URLSearchParams(location.search);
const data = decodeURIComponent(query.get('data'));
let cartProducts = JSON.parse(data);

let productsHTML = document.getElementById('products');
let quantityHTML = document.getElementById('quantity');
let priceHTML = document.getElementById('price');
let totalHTML = document.getElementById('total');
let html = '';

document.addEventListener('DOMContentLoaded', LoadProducts);

function LoadProducts() {
    var quantity = 0;
    var price = 0;
    var total = 0;
    cartProducts.map(x => {
        html += `<div id="product-item">
                    <img src="${x.picture}">
                        <p>${x.productName}</p>
                        <span>${formatCurrency(x.price)}</span>
                        <p>Số lượng: ${x.quantity}</p></div> `;
        quantity += x.quantity;
        price += (x.price * x.quantity);
    });
    productsHTML.innerHTML = html;
    updateDOM(quantity, price, total);
}
function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}
function updateDOM(quantity, price, total) {
    quantityHTML.innerText = quantity;
    priceHTML.innerText = formatCurrency(price);
    total = price + 10000;
    totalHTML.innerText = formatCurrency(total);
}

const radios = document.querySelectorAll('input[name="pay-selected"]');
radios.forEach(radio => {
    radio.addEventListener('click', () => {
        const parentDiv = radio.parentElement.parentElement;

        document.querySelectorAll('#momo').forEach(x => {
            x.style.border = '1px solid #DADADA';
        })

        parentDiv.style.border = "2px solid #F57224";
    });
});