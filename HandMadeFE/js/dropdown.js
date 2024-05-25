const btn = document.querySelector('.btn');
const menu = document.querySelector('.menu-options');

btn.addEventListener('click', () => {
  menu.style.display = 'block';
});

document.addEventListener('click', e => {
  if(!menu.contains(e.target)) {
    menu.style.display = 'none'; 
  }
})