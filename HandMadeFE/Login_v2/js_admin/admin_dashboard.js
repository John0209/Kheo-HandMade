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

const btnSelect = document.querySelectorAll('button[name="btnSelect"]');
btnSelect.forEach(btn => {
    btn.addEventListener('click', async e => {
        changeBorderStatus(btn);
    });
});

const border_Status = document.querySelectorAll('div[name="border_Status"]');
const ORDER_STATUS_BTN = {
    ALL: 'btnAll',
    PROCESS: 'btnProcess',
    CONFIRM: 'btnConfirm',
    DELIVERY: 'btnDelivery',
    SUCCESS: 'btnSuccess',
    FAILED: 'btnFail',
}
function changeBorderStatus(btn) {
    
    border_Status.forEach(x => {
        x.className = '';
    });
    var line = '';

    switch (btn.id) {
        case ORDER_STATUS_BTN.PROCESS:
            line = document.getElementById('process');
            break;
        case ORDER_STATUS_BTN.CONFIRM:
            line = document.getElementById('confirm');
            break;
        case ORDER_STATUS_BTN.DELIVERY:
            line = document.getElementById('delivery');
            break;
        case ORDER_STATUS_BTN.SUCCESS:
            line = document.getElementById('success');
            break;
        case ORDER_STATUS_BTN.FAILED:
            line = document.getElementById('fail');
            break;
    }
    line.className = 'border_Order';
}