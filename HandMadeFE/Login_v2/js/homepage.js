

document.addEventListener('DOMContentLoaded', async function () {
    const productsData = await getProducts();
    console.log(productsData);
    const products = document.getElementById('product-list');
    let html = '';
   
    productsData.forEach(x=>{
        html += `
    <div class="product-items">
        <img src="../images/candle.jpg" class="product-image"/>
        <p>${x.categoryName}</p>
        <h2>${x.productName}</h2>
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
    const response = await fetch('https://localhost:44326/api/v1/products?status=Stocking')
    return await response.json();
}




