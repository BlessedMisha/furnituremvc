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
        let productRow = this.closest('.card');
        if (!productRow) {
            console.error("Product row not found");
            return;
        }

        let nameElement = productRow.querySelector('.product-name');
        let priceElement = productRow.querySelector('.product-price');
        let sizeElement = productRow.querySelector('.product-size');
        let imageElement = productRow.querySelector('img');
        let catalogId = productRow.getAttribute('data-catalogItemId');

        if (nameElement && priceElement && catalogId && imageElement && sizeElement) {
            let name = nameElement.textContent.trim() || 'Unknown';
            let price = priceElement.textContent.trim() || 'Unknown';
            let imageUrl = imageElement.getAttribute('src') || '';
            let size = sizeElement.textContent.trim() || 'unknown';

            console.log("Adding item to basket:", { catalogId, name, price, imageUrl, size });

            let item = basket.selectedItems.find(i => i.itemId == catalogId);
            if (item == null) {
                item = {
                    itemId: catalogId,
                    name: name,
                    price: price,
                    size: size,
                    qty: 1,
                    imageUrl: imageUrl
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
        let bagNumberElement = document.querySelector('.bag-number');
        if (bagNumberElement) {
            bagNumberElement.innerHTML = totalQty;
        } else {
            console.error('Bag number element not found');
        }
    }
});
