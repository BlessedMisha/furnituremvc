document.addEventListener("DOMContentLoaded", function () {
    let basket = {
        selectedItems: []
    };

    let localBasketStr = localStorage.getItem('basket');
    if (localBasketStr != null) {
        basket = JSON.parse(localBasketStr);
        console.log("Basket loaded from local storage:", basket);
        updateTotalQty();
    }

    displayBasketItems();

    function displayBasketItems() {
        let basketContainer = document.querySelector('.basket-products');
        if (!basketContainer) {
            console.error('Basket container not found');
            return;
        }
        basketContainer.innerHTML = ''; // Очищення контейнера перед вставкою нових елементів

        if (basket.selectedItems.length === 0) {
            console.log("Basket is empty");
        } else {
            basket.selectedItems.forEach(item => {
                console.log("Displaying item:", item);
                let productHTML = `
                <div class="basket-product" data-catalogItemId="${item.itemId}">
                    <img src="${item.imageUrl}" alt="img">
                    <div class="productB-info">
                        <p class="name">${item.name}</p>
                        <p class="size">Size: 100*100</p>
                        <p class="price">${item.price}</p>
                    </div>
                    <div class="productB-actions">
                        <a href="#" class="remove-item"><i class="fa-solid fa-xmark" style="color: rgb(71, 71, 71);"></i></a>
                        <div class="counter">
                            <span class="decrement"><i class="fa-solid fa-minus"></i></span>
                            <span class="count">${item.qty}</span>
                            <span class="increment"><i class="fa-solid fa-plus"></i></span>
                        </div>
                    </div>
                </div>
            `;
                basketContainer.insertAdjacentHTML('beforeend', productHTML);
            });

            // Додаємо обробники подій для кнопок increment, decrement та remove
            document.querySelectorAll('.increment').forEach(button => {
                button.addEventListener('click', incrementQty);
            });

            document.querySelectorAll('.decrement').forEach(button => {
                button.addEventListener('click', decrementQty);
            });

            document.querySelectorAll('.remove-item').forEach(button => {
                button.addEventListener('click', removeItem);
            });
        }
    }


    function incrementQty() {
        let catalogId = this.closest('.basket-product').getAttribute('data-catalogItemId');
        let item = basket.selectedItems.find(i => i.itemId == catalogId);
        if (item) {
            item.qty++;
            localStorage.setItem("basket", JSON.stringify(basket));
            displayBasketItems();
            updateTotalQty();
        }
    }

    function decrementQty() {
        let catalogId = this.closest('.basket-product').getAttribute('data-catalogItemId');
        let item = basket.selectedItems.find(i => i.itemId == catalogId);
        if (item && item.qty > 1) {
            item.qty--;
            localStorage.setItem("basket", JSON.stringify(basket));
            displayBasketItems();
            updateTotalQty();
        }
    }

    function removeItem() {
        let catalogId = this.closest('.basket-product').getAttribute('data-catalogItemId');
        basket.selectedItems = basket.selectedItems.filter(i => i.itemId != catalogId);
        localStorage.setItem("basket", JSON.stringify(basket));
        displayBasketItems();
        updateTotalQty();
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
