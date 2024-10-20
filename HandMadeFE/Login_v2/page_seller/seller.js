document.addEventListener('DOMContentLoaded', async function () {
    const account = JSON.parse(sessionStorage.getItem("account"));
    const sellerId = account.id - 1;
    
    const productsData = await getProducts();
    console.log(productsData);

    const products = document.getElementById('product-list');
    let html = '';

    // Lọc các sản phẩm có sellerId bằng với sellerId của account
    const filteredProducts = productsData.filter(x => x.sellerId === sellerId);
    
    // Tạo HTML cho từng sản phẩm sau khi lọc
    filteredProducts.forEach(x => {
        html += `
        <div class="product-items">
            <img src="${x.picture}" class="product-image"></img>
            <div class="infor">
                <a href="../page_seller/update_product.html?id=${x.productId}">${x.productName}</a>
                <p> Mô tả: ${x.description}</p>
                <p> Số lượng: ${x.quantity}</p>
                <p> Giá bán: ${x.price}</p>
            </div>
        </div>
        `;
    }); 
    products.innerHTML = html;

});



async function getProducts() {
    const response = await fetch('https://handmade.somee.com/api/v1/products?status=Stocking')
    return await response.json();
}



document.getElementById('addProduct').addEventListener('click', function() {
    window.location.href = '../page_seller/add_product.html';
});
