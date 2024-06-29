// const btnMenu=document.querySelectorAll('button[name="btnMenu"]')
// btnMenu.forEach(btn =>{
//     btn.addEventListener('click',async e =>{
//         changeBtnStyle(btn);
//     })
// })
// function changeBtnStyle(btn){
//     btnMenu.forEach(x=>{
//         x.className='btnMenu';
//     })
//      btn.className='sidebar-select';
// }
document.addEventListener('DOMContentLoaded', function () {
    const btnMenu = document.querySelectorAll('button[name="btnMenu"]');
    btnMenu.forEach(x => {
        x.className = 'btnMenu';
    })
    var btnOrder = document.getElementById('btnNotify');
    btnOrder.className = 'sidebar-select';
})

document.getElementById('btnLogout').addEventListener('click', function () {
    window.location.href = '../login.html';
})
document.getElementById('btnOrder').addEventListener('click', function () {
    window.location.href = '../page_admin/admin_orders.html';
})
document.getElementById('btnNotify').addEventListener('click', function () {
    window.location.href = '../page_admin/admin_dashboard.html';
})