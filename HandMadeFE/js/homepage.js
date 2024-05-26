

document.addEventListener('DOMContentLoaded', async function () {
    const productsData = await getProducts();
    console.log(productsData);
    const products = document.getElementById('product-list');
    let html = '';
   
    productsData.forEach(x=>{
        html += `
    <div class="product-items">
        <img src="../images/candle.jpg"/ class="product-image">
        <p>${x.categoryName}</p>
        <h2>${x.productName}</h2>
        <p>${x.price}</p>
    </div>
    `
    })

    products.innerHTML=html;
})

async function getProducts() {
    const response = await fetch('https://localhost:44326/api/v1/products?status=Stocking')
    return await response.json();
}




