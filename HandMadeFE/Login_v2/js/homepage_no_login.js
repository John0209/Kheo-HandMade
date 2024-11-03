const select = document.getElementById('account-dropdown');
select.addEventListener('change',redirectToPage);
function redirectToPage(e){
    if(e.target.value === 'login'){
        location.href="login.html";
    }
}

document.addEventListener('DOMContentLoaded', async function () {
    const productsData = await getProducts();
    console.log(productsData);
    const products = document.getElementById('product-list');
    let html = '';
   
    productsData.forEach(x=>{
        html += `
    <div class="product-items">
        <img src="${x.picture}" class="product-image"></img>
        <a>${x.productName}</a>
        <div class="product-star">
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        <i class="fa-solid fa-star"></i>
        </div>
        <h2 style="color: #F57C20;">${x.price}đ</h2>
        <div class="product-bar"></div>
    </div>
    `
    })

    products.innerHTML=html;
})

async function getProducts() {
    const response = await fetch('https://handmade.somee.com/api/v1/products?status=Stocking')
    return await response.json();
}

document.getElementById('product-list').onclick = function() {
    alert("Vui lòng đăng nhập để bắt đầu đặt hàng!")
};