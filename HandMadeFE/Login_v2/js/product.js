let data = '';
document.addEventListener('DOMContentLoaded', async function () {
    data = await getProductById();
    console.log(data);

    const render = () => {
        const { productName, price, sellerName, picture, quantity } = data;

        document.getElementById('image-product').innerHTML = ` <img src="${picture}" id="image-crop">`
        document.getElementById('title').innerHTML = `<p>${productName}</p>`;
        document.getElementById('price').innerHTML = formatCurrency(price);
        document.getElementById('sell').innerHTML = `<p>Người bán:</p><span>${sellerName}</span>`
        // document.getElementById('des-content').innerHTML=`<p>${description}</p>`;
        document.getElementById('quantity').innerHTML = `<p>Số lượng:</p><span>${quantity}</span>`
    }
    render();
})

function formatCurrency(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}

async function getProductById() {
    const queryString = location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id');

    const response = await fetch(`https://handmade.somee.com/api/v1/products/${id}`)
    return await response.json();
}

const addToCart = document.getElementById('addToCard');
addToCart.addEventListener('click', () => {
   // sessionStorage.clear();
    AddProductToCart();
});


function AddProductToCart() {
    const cart = JSON.parse(sessionStorage.getItem('cart')) || { products: [] };

    const productIndex = cart.products.findIndex(item => item.productId === data.productId);

    if (productIndex !== -1) {
        alert('Sản phẩm đã có trong giỏ hàng');
    } else {
        alert('Thêm sản phẩm vào giỏ hàng thành công');
        cart.products.push(data);
        sessionStorage.setItem('cart', JSON.stringify(cart));
    }
}
