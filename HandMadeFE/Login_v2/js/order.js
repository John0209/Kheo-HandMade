const btnSelect = document.querySelectorAll('button[name="btnSelect"]');
btnSelect.forEach(btn => {
    btn.addEventListener('click', e => {
        changeBtnColor(btn);
    });
});

function changeBtnColor(btn) {
    btnSelect.forEach(x=>{
        x.style.color = "#212121cb";
        x.style.fontWeight = "initial";
    });
    btn.style.color = "#F57224";
    btn.style.fontWeight = "bold";
}