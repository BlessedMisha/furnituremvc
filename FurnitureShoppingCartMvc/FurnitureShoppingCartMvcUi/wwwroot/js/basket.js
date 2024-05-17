// TODO :add JS classes
let basket = {
    selectedItems : []
};

let localBasketStr = localStorage.getItem('basket');
basket = JSON.parse(localBasketStr);
updateTotalQty();

let buttons = document.getElementsByClassName('btnAddToBasket');
for(let button of buttons)
    button.addEventListener('click', onBtnAddToBasketClick);

function onBtnAddToBasketClick() {

    let catalogId = this.closest('.product-text-row')
        .getAttribute('data-catalogItemId');

    let item = basket.selectedItems.find(i => i.itemId == catalogId);
    if (item == null) {
        item = {
            itemId: catalogId,
            qty: 1
        };
        basket.selectedItems.push(item);
    } else {
        item.qty++;
    }

    localStorage.setItem("basket", JSON.stringify(basket));
    updateTotalQty();
}

function updateTotalQty() {
    let totalQty = basket.selectedItems.map(i => i.qty).reduce((total, qty) => total + qty, 0);
    document.getElementsByClassName('bag-number')[0].innerHTML = totalQty;
}