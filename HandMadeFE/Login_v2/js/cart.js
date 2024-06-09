document.addEventListener('DOMContentLoaded', () => {
    LoadCart();
});

let html = '';
function LoadCart() {
    const cart = JSON.parse(sessionStorage.getItem('cart'));
    console.log(cart)
    cart.products.map(x => {
        html += `
            <div class="row" id="product">
                <div class="col-sm-2">
                    <div id="select-one">
                        <input type="checkbox" id="box-${x.productId}" class="box">
                            <img src="${x.picture}">
                    </div>
                </div>

                <div class="col-sm-6">
                        <p>${x.productName}</p>
                </div>

                <div class="col-sm-2">
                    <div id="price">
                            <input type="hidden" id="price-${x.productId}" value="${x.price}"/>
                            <span> ${formatCurrency(x.price)}</span>
                            <button id="remove-${x.productId}" class="remove">
                                <i class="fa-solid fa-trash"></i>
                            </button>
                    </div>
                </div>

                <div class="col-sm-2">
                    <div id="add-to-card">
                            <button id="minus-${x.productId}" class="minus">-</button>
                            <input type="text" min="1" max="20" value="1" id="quantity-${x.productId}" class="qty"/>
                            <button id="plus-${x.productId}" class="plus">+</button>
                    </div>
                </div>

            </div>
        `
    });
    document.getElementById('loadCard').innerHTML = html;

}

function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}

const QuantityAction = {
    PLUS: 'plus',
    MINUS: 'minus'
};

setTimeout(() => {
    const minusBtn = document.querySelectorAll('.minus');
    minusBtn.forEach(btn => {
        btn.addEventListener('click', handleClickMinus)
    });

    const plusBtn = document.querySelectorAll('.plus');
    plusBtn.forEach(x => {
        x.addEventListener('click', handleClickPlus)
    });

    const checkBox = document.querySelectorAll('.box');
    checkBox.forEach(x => {
        x.addEventListener('click', handleBox)
    })

    const removeBtn = document.querySelectorAll('.remove');
    removeBtn.forEach(x => {
        x.addEventListener('click', handleRemove)
    })
}, 100);

function handleClickMinus(e) {
    const btnId = e.target.id;
    const parts = btnId.split('-');
    const id = parts[1];

    const minus = document.getElementById(btnId);
    const qty = document.getElementById(`quantity-${id}`);
    const box = document.getElementById(`box-${id}`);
    const pri = document.getElementById(`price-${id}`);

    minus.addEventListener('click', handleQuantity(QuantityAction.MINUS, qty), calculateQuantityWhenChecked(box, qty, pri, QuantityAction.MINUS));
}

function handleClickPlus(e) {
    const btnId = e.target.id;
    const parts = btnId.split('-');
    const id = parts[1];

    const plus = document.getElementById(btnId);
    const qty = document.getElementById(`quantity-${id}`);
    const box = document.getElementById(`box-${id}`);
    const pri = document.getElementById(`price-${id}`);

    // plus.addEventListener('click',()=>{
    //     handleQuantity(QuantityAction.PLUS, qty);
    //    // calculateQuantity(box,qty,pri,QuantityAction.PLUS);
    // });
    plus.addEventListener('click', handleQuantity(QuantityAction.PLUS, qty), calculateQuantityWhenChecked(box, qty, pri, QuantityAction.PLUS))
}

function handleQuantity(type, quantity) {
    let currentValue = Number(quantity.value);

    if (type === QuantityAction.PLUS) {
        currentValue++;
    } else {
        currentValue--;
    }

    if (currentValue <= 0) {
        currentValue = 1;
    }
    return quantity.value = currentValue;
}

let price_cart = document.getElementById('price');
let quantity_cart = document.getElementById('quantity');
let total_cart = document.getElementById('total');
let totalPrice = 0;
let totalQuantity = 0;

function handleBox(e) {
    const boxId = e.target.id;
    const parts = boxId.split('-');
    const id = parts[1];

    const box = document.getElementById(boxId);
    const qty = document.getElementById(`quantity-${id}`);
    const pri = document.getElementById(`price-${id}`);

    calculateMoney(box, qty, pri);
}

let qtyPrevious=0;
function calculateQuantityWhenChecked(box, qty, pri, type) {
    var price = Number(pri.value);
    var quantity = Number(qty.value);
    var ship = 0;

    if (box.checked && type === QuantityAction.PLUS) {
        qtyPrevious=quantity;
        totalQuantity += 1;
        totalPrice += price;
        ship = 10000;
    }
    if (box.checked && type === QuantityAction.MINUS) {
        if(quantity===1 && qtyPrevious===1 ){
            return
        }
        qtyPrevious=quantity;
        totalQuantity -= 1;
        totalPrice -= price;
        ship = 10000;
    }

    updateDOM(totalQuantity, totalPrice, ship)
}

function calculateMoney(box, qty, pri) {
    var price = Number(pri.value);
    var quantity = Number(qty.value);
    var ship = 10000;

    if (box.checked) {
        totalQuantity += quantity;
        totalPrice += (price * quantity);
    } else {
        totalQuantity -= quantity;
        totalPrice -= (price * quantity);
        ship = 0;
    }

    updateDOM(totalQuantity, totalPrice, ship);
}

function updateDOM(totalQuantity, totalPrice, ship) {
    quantity_cart.innerText = totalQuantity;
    price_cart.innerText =formatCurrency(totalPrice);
    totalPrice += ship;
    total_cart.innerText = formatCurrency(totalPrice);
}
 function handleRemove(e) {
    const btnId = e.target.closest('.remove').id;
    const parts = btnId.split('-');
    const number = Number(parts[1]);

    if (number > -1) {
        const cart = JSON.parse(sessionStorage.getItem('cart'));
        var newCart = {
            products: cart.products.filter(x => x.productId !== number)
        };

        sessionStorage.setItem('cart', JSON.stringify(newCart));
        location.reload();
    } else {
        alert('Thao tác quá nhanh, xin làm lại')
    }

}


