const QuantityAction={
    PLUS:'plus',
    MINUS:'minus'
};


const minus= document.getElementById('minus');
const plus= document.getElementById('plus');
const quantity = document.getElementById('pro-quantity');

minus.addEventListener('click',()=>{
    handleQuantity(QuantityAction.MINUS);
});
plus.addEventListener('click',()=>{
    handleQuantity(QuantityAction.PLUS);
});


function handleQuantity(type){
    let currentValue=Number(quantity.value);

    if(type===QuantityAction.PLUS){
        currentValue++;
    }else{
        currentValue--;
    }

    quantity.value=currentValue;
}
