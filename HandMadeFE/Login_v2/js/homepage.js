

document.addEventListener('DOMContentLoaded', async function () {
    const productsData = await getProducts();
    console.log(productsData);
    const products = document.getElementById('product-list');
    let html = '';
   
    productsData.forEach(x=>{
        html += `
    <div class="product-items">
        <img src="${x.picture}" class="product-image"/>
        <p>${x.categoryName}</p>
        <a href="../pages/product.html?id=${x.productId}">${x.productName}</a>
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




