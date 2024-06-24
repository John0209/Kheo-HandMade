const btnMenu=document.querySelectorAll('button[name="btnMenu"]')
btnMenu.forEach(btn =>{
    btn.addEventListener('click',async e =>{
        changeBtnStyle(btn);
    })
})
function changeBtnStyle(btn){
    btnMenu.forEach(x=>{
        x.className='btnMenu';
    })
    btn.className='sidebar-select';
}