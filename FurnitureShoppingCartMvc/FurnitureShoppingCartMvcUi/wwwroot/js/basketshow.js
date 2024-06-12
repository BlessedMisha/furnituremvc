document.addEventListener("DOMContentLoaded", function () {
    let basket = {
        selectedItems: []
    };

    let localBasketStr = localStorage.getItem('basket');
    if (localBasketStr != null) {
        basket = JSON.parse(localBasketStr);
        console.log("Basket loaded from local storage:", basket);
        updateTotalQty();
        updateTotalPrice(); // Додано для обрахунку загальної ціни при завантаженні сторінки
    }

    displayBasketItems();

    function displayBasketItems() {
        let basketContainer = document.querySelector('.order-info');
        if (!basketContainer) {
            console.error('Basket container not found');
            return;
        }
        basketContainer.innerHTML = '';

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
                        <p class="size">Size: ${item.size}</p>
                        <p class="price">$${item.price}</p>
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
            updateTotalPrice(); // Оновлюємо загальну ціну після збільшення кількості
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
            updateTotalPrice(); // Оновлюємо загальну ціну після зменшення кількості
        }
    }

    function removeItem() {
        let catalogId = this.closest('.basket-product').getAttribute('data-catalogItemId');
        basket.selectedItems = basket.selectedItems.filter(i => i.itemId != catalogId);
        localStorage.setItem("basket", JSON.stringify(basket));
        displayBasketItems();
        updateTotalQty();
        updateTotalPrice(); // Оновлюємо загальну ціну після видалення товару
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
    function updateTotalPrice() {
        if (basket.selectedItems == null) {
            return;
        }
        let totalPrice = basket.selectedItems.map(i => parseFloat(i.price) * i.qty).reduce((total, price) => total + price, 0);
        let totalPriceElement = document.getElementById('total-price');
        if (totalPriceElement) {
            totalPriceElement.textContent = `$${new Intl.NumberFormat('en-US', { minimumFractionDigits: 2 }).format(totalPrice)}`;
        } else {
            console.error('Total price element not found');
        }
    }

});