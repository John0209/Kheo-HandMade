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
        `;
    });

    products.innerHTML = html;

    // Thêm vào thẻ <div id="update-product">
    const updateProduct = document.getElementById('update-product');
    let selectHTML = '<select name="products" id="sltProduct">'; // Bỏ thuộc tính multiple

    // Tạo option cho thẻ select
    filteredProducts.forEach(x => {
        selectHTML += `<option value="${x.productId}">${x.productName}</option>`;
    });
    
    selectHTML += '</select>';
    
    // Thêm các nút và thẻ select vào div
    updateProduct.innerHTML = `
        ${selectHTML}
        <button id="btnUpdate">Cập nhật thông tin sản phẩm</button>
        <button id="btnDelete">Xóa sản phẩm</button>
    `;

    // Xử lý sự kiện click nút xóa sản phẩm
    const btnDelete = document.getElementById('btnDelete');
    btnDelete.addEventListener('click', async function () {
        const sltProduct = document.getElementById('sltProduct');
        const selectedProduct = sltProduct.value; // Chỉ lấy giá trị của sản phẩm được chọn

        // Kiểm tra nếu không có sản phẩm được chọn
        if (!selectedProduct) {
            alert("Vui lòng chọn một sản phẩm để xóa.");
            return;
        }

        // Gửi yêu cầu xóa sản phẩm
        try {
            const response = await fetch(`https://www.handmade.somee.com/api/v1/products/${selectedProduct}`, {
                method: 'DELETE',
                headers: {
                    'accept': '*/*'
                }
            });

            // Kiểm tra nếu trạng thái trả về từ 200-300
            if (response.status >= 200 && response.status < 300) {
                alert(`Xóa sản phẩm có ID ${selectedProduct} thành công`);
            } else {
                alert(`Xóa sản phẩm có ID ${selectedProduct} thất bại, vui lòng thử lại`);
            }
        } catch (error) {
            alert(`Có lỗi xảy ra khi xóa sản phẩm có ID ${selectedProduct}. Vui lòng thử lại.`);
        }
    });

    // Xử lý sự kiện click nút cập nhật sản phẩm
    const btnUpdate = document.getElementById('btnUpdate');
    btnUpdate.addEventListener('click', function () {
        // Thêm các trường nhập liệu để cập nhật sản phẩm
        updateProduct.innerHTML += `
            <input type="text" placeholder="Tên sản phẩm" id="productNameUp">
            <input type="text" placeholder="Mô tả" id="descriptionUp">
            <input type="text" placeholder="Số lượng" id="quantityUp">
            <input type="text" placeholder="Giá" id="priceUp">
            <button id="updateInfor">Cập nhật thông tin</button>

            <!-- Thẻ để chọn file -->
            <input type="file" id="productImageUp">
            <button id="updateImage">Cập nhật hình ảnh</button>
        `;

        // Xử lý cập nhật thông tin sản phẩm
        const btnUpdateInfor = document.getElementById('updateInfor');
        btnUpdateInfor.addEventListener('click', async function () {
            const selectedProduct = document.getElementById('sltProduct').value;
            const productName = document.getElementById('productNameUp').value;
            const description = document.getElementById('descriptionUp').value;
            const quantity = document.getElementById('quantityUp').value;
            const price = document.getElementById('priceUp').value;

            const productData = {
                productId: selectedProduct,
                productName: productName,
                description: description,
                quantity: parseInt(quantity),
                price: parseFloat(price)
            };

            try {
                const response = await fetch('https://www.handmade.somee.com/api/v1/products', {
                    method: 'PUT',
                    headers: {
                        'accept': '*/*',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(productData)
                });

                if (response.status >= 200 && response.status < 300) {
                    alert('Cập nhật thông tin sản phẩm thành công');
                } else {
                    alert('Cập nhật thông tin sản phẩm thất bại, vui lòng thử lại');
                }
            } catch (error) {
                alert('Có lỗi xảy ra khi cập nhật thông tin sản phẩm. Vui lòng thử lại.');
            }
        });

        // Xử lý cập nhật hình ảnh sản phẩm
        const btnUpdateImage = document.getElementById('updateImage');
        btnUpdateImage.addEventListener('click', async function () {
            const selectedProduct = document.getElementById('sltProduct').value;
            const productImage = document.getElementById('productImageUp').files[0];

            if (!productImage) {
                alert('Vui lòng chọn một hình ảnh để cập nhật.');
                return;
            }

            const formData = new FormData();
            formData.append('file', productImage);

            try {
                const response = await fetch(`https://www.handmade.somee.com/api/v1/firebase/upload?uploadType=Product&id=${selectedProduct}`, {
                    method: 'PATCH',
                    headers: {
                        'accept': '*/*'
                    },
                    body: formData
                });

                if (response.status >= 200 && response.status < 300) {
                    alert('Cập nhật hình ảnh thành công');
                } else {
                    alert('Cập nhật hình ảnh thất bại, vui lòng thử lại');
                }
            } catch (error) {
                alert('Có lỗi xảy ra khi cập nhật hình ảnh. Vui lòng thử lại.');
            }
        });
    });
});




async function getProducts() {
    const response = await fetch('https://handmade.somee.com/api/v1/products?status=Stocking')
    return await response.json();
}




document.getElementById("addProductBtn").addEventListener("click", async function () {
    // Lấy dữ liệu từ các ô input
    const productName = document.getElementById("productName").value;
    const description = document.getElementById("description").value;
    const quantity = document.getElementById("quantity").value;
    const price = document.getElementById("price").value;
    const productImage = document.getElementById("productImage").files[0];

    // Lấy sellerId từ session, giả sử lưu trong sessionStorage
    const account = JSON.parse(sessionStorage.getItem("account"));
    const sellerId = account.id - 1;

    // Tạo đối tượng dữ liệu sản phẩm
    const productData = {
        sellerId: sellerId,
        categoryId: 1, // category mặc định là 1
        productName: productName,
        description: description,
        quantity: parseInt(quantity),
        price: parseFloat(price)
    };
    console.log(productData)
    try {
        // Gửi yêu cầu POST để thêm sản phẩm
        const postResponse = await fetch('https://www.handmade.somee.com/api/v1/products', {
            method: 'POST',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productData)
        });
        const status = postResponse.status;
        if (status >= 200 && status < 300) {

            const postData = await postResponse.json();
            const orderId = postData.orderId; // Lấy orderId từ kết quả trả về

            // Gửi yêu cầu PATCH để upload hình ảnh
            const formData = new FormData();
            formData.append("file", productImage); // Lấy file từ input file

            const patchResponse = await fetch(`https://www.handmade.somee.com/api/v1/firebase/upload?uploadType=Product&id=${orderId}`, {
                method: 'PATCH',
                headers: {
                    'accept': '*/*'
                },
                body: formData
            });

            if (!patchResponse.ok) {
                throw new Error('Lỗi khi upload hình ảnh');
            }

            const patchData = await patchResponse.json();
            console.log('Thêm sản phẩm và upload hình ảnh thành công:', patchData);
            alert('Thêm sản phẩm thành công! Vui lòng load lại trang để cập nhật.')
        }
        else {
            alert('thêm sản phẩm không thành công, vui lòng thử lại')
        }
    } catch (error) {
        console.error('Lỗi:', error);
    }
});
