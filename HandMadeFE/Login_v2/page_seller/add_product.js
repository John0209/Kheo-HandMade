


document.getElementById("btnAdd").addEventListener("click", async function () {
    // Lấy dữ liệu từ các ô input
    const productName = document.getElementById("productNameUp").value;
    const description = document.getElementById("descriptionUp").value;
    const quantity = document.getElementById("quantityUp").value;
    const price = document.getElementById("priceUp").value;
    const productImage = document.getElementById("productImageUp").files[0];

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