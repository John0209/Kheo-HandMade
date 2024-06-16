const cartBtn= document.getElementById('btnCart');
cartBtn.addEventListener('click',function(){
    window.location.href='../pages/cart.html';
});

const select = document.getElementById('account-dropdown');
select.addEventListener('change',redirectToPage);
function redirectToPage(e){
    if(e.target.value === 'order'){
        location.href="../pages/order.html";
    }
    if(e.target.value === 'logout'){
        location.href="../login.html";
    }
}

document.addEventListener('DOMContentLoaded',LoadCustomerName);
function LoadCustomerName(){
    const account=JSON.parse(sessionStorage.getItem('account'));
    const option_name=select.options[0];
    option_name.textContent=account.name;

}
