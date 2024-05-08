document.addEventListener("DOMContentLoaded", function () {
    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            console.log("Широта: " + latitude + ", Довгота: " + longitude);

            // Тут ви можете використовувати отримані координати для подальшого використання
            // Наприклад, відправити їх на сервер або використати для відображення місцезнаходження на карті
        }, function (error) {
            console.error("Помилка отримання місцезнаходження: " + error.message);
        });
    } else {
        console.log("Геолокація не підтримується в вашому браузері");
    }
});
