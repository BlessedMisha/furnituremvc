document.addEventListener("DOMContentLoaded", function () {
    let basket = {
        selectedItems: []
    };

    let localBasketStr = localStorage.getItem('basket');
    if (localBasketStr != null) {
        basket = JSON.parse(localBasketStr);
        updateTotalQty();
    }

    let buttons = document.getElementsByClassName('btnAddToBasket');
    for (let button of buttons) {
        button.addEventListener('click', onBtnAddToBasketClick);
    }

    function onBtnAddToBasketClick() {
        let productRow = this.closest('.product-text-row');
        if (!productRow) {
            console.error("Product row not found");
            return;
        }

        let nameElement = productRow.querySelector('.product-name');
        let priceElement = productRow.querySelector('.product-price');
        let catalogId = productRow.getAttribute('data-catalogItemId');

        if (nameElement && priceElement && catalogId) {
            let name = nameElement.textContent.trim() || 'Unknown';
            let price = priceElement.textContent.trim() || 'Unknown';

            console.log("Adding item to basket:", { catalogId, name, price });

            let item = basket.selectedItems.find(i => i.itemId == catalogId);
            if (item == null) {
                item = {
                    itemId: catalogId,
                    name: name,
                    price: price,
                    qty: 1
                };
                basket.selectedItems.push(item);
            } else {
                item.qty++;
            }
            localStorage.setItem("basket", JSON.stringify(basket));
            console.log("Basket after adding item:", basket);
            updateTotalQty();
        } else {
            console.error("Error: Missing element information or catalogId");
        }
    }

    function updateTotalQty() {
        if (basket.selectedItems == null) {
            return;
        }
        let totalQty = basket.selectedItems.map(i => i.qty).reduce((total, qty) => total + qty, 0);
        document.querySelector('.bag-number').innerHTML = totalQty;
    }
});