let data = '';
document.addEventListener('DOMContentLoaded', async function () {
    data = await getProductById();
    

    const render = () => {
        const { productName, price, description, picture, quantity } = data;

        document.getElementById('image-product').innerHTML = ` <img src="${picture}" id="image-crop">`
        document.getElementById('productNameUp').value = productName;
        document.getElementById('descriptionUp').value = description;
        document.getElementById('quantityUp').value = quantity;
        document.getElementById('priceUp').value = price;
    }
    render();
})

async function getProductById() {
    const queryString = location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id');

    const response = await fetch(`https://handmade.somee.com/api/v1/products/${id}`)
    return await response.json();
}

// Xử lý cập nhật thông tin sản phẩm
const btnUpdateInfor = document.getElementById('updateInfor');
btnUpdateInfor.addEventListener('click', async function () {
    data = await getProductById();
    const { productId} = data;
    const productName = document.getElementById('productNameUp').value;
    const description = document.getElementById('descriptionUp').value;
    const quantity = document.getElementById('quantityUp').value;
    const price = document.getElementById('priceUp').value;

    const productData = {
        productId: productId,
        productName: productName,
        description: description,
        quantity: parseInt(quantity),
        price: parseFloat(price)
    };
    console.log(productData)
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
    data = await getProductById();
    const { productId} = data;
    const productImage = document.getElementById('productImageUp').files[0];

    if (!productImage) {
        alert('Vui lòng chọn một hình ảnh để cập nhật.');
        return;
    }

    const formData = new FormData();
    formData.append('file', productImage);

    try {
        const response = await fetch(`https://www.handmade.somee.com/api/v1/firebase/upload?uploadType=Product&id=${productId}`, {
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

// Xử lý sự kiện click nút xóa sản phẩm
const btnDelete = document.getElementById('btnDelete');
btnDelete.addEventListener('click', async function () {
    data = await getProductById();
    const { productId} = data;

    // Gửi yêu cầu xóa sản phẩm
    try {
        const response = await fetch(`https://www.handmade.somee.com/api/v1/products/${productId}`, {
            method: 'DELETE',
            headers: {
                'accept': '*/*'
            }
        });

        // Kiểm tra nếu trạng thái trả về từ 200-300
        if (response.status >= 200 && response.status < 300) {
            alert(`Xóa sản phẩm thành công`);
            window.location.href = '../page_seller/homepage_seller.html'
        } else {
            alert(`Xóa sản phẩm thất bại, vui lòng thử lại`);
        }
    } catch (error) {
        alert(`Có lỗi xảy ra khi xóa sản phẩm. Vui lòng thử lại.`);
    }
});