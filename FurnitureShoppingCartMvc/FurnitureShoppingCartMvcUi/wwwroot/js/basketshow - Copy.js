// Отримання елементів корзини з локального сховища
let basket = {
    selectedItems: []
};

let localBasketStr = localStorage.getItem('basket');
if (localBasketStr != null) {
    basket = JSON.parse(localBasketStr);
    updateTotalQty();
}

// Відображення товарів у корзині
function displayBasketItems() {
    let basketContainer = document.querySelector('.basket-products');
    basketContainer.innerHTML = ''; // Очищення контейнера перед вставкою нових елементів

    basket.selectedItems.forEach(item => {
        let productHTML = `
            <div class="basket-product">
                <img src="native_lamp.png" alt="img">
                <div class="productB-info">
                    <p id="name">Native hyative chair</p>
                    <p id="size">Size: 100*100</p>
                    <p id="price">$4,399.00</p>
                </div>
                <div class="productB-actions">
                    <a href="#"><i class="fa-solid fa-xmark" style="color: rgb(71, 71, 71);"></i></a>
                    <div class="counter">
                        <span id="decrement"><i class="fa-solid fa-minus"></i></span>
                        <span id="count">${item.qty}</span>
                        <span id="increment"><i class="fa-solid fa-plus"></i></span>
                    </div>
                </div>
            </div>
        `;
        basketContainer.insertAdjacentHTML('beforeend', productHTML); // Додавання HTML-рядка до контейнера
    });
}

displayBasketItems(); // Виклик функції для відображення товарів у корзині
