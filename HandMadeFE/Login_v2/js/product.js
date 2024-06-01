document.addEventListener('DOMContentLoaded',async function(){
    const data = await getProductById();
    console.log(data);
    
    const render= ()=>{
        const {productName,price,description}=data;

        document.getElementById('title').innerHTML=`<p>${productName}</p>`;
        document.getElementById('price').innerHTML=formatCurrency(price);
        // document.getElementById('des-content').innerHTML=`<p>${description}</p>`;
    }
    render();
})

function formatCurrency(price){
    return new Intl.NumberFormat('vi-VN',{
        style: 'currency',
        currency: 'VND'
    }).format(price);
}

async function getProductById(){
    const queryString=location.search;
    const urlParams= new URLSearchParams(queryString);
    const id=urlParams.get('id');

    const response= await fetch(`https://handmade.somee.com/api/v1/products/${id}`)
    return await response.json();
}
