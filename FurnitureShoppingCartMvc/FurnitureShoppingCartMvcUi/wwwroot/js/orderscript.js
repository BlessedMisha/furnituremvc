document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("checkout").addEventListener("click", function () {
        let firstName = document.querySelector("#firstName").value;
        let lastName = document.querySelector("#lastName").value;
        let email = document.querySelector("#email").value;
        let phone = document.querySelector("#phone").value;
        let address = document.querySelector("#address").value;

        let items = [];
        document.querySelectorAll(".basket-product").forEach(product => {
            let item = {
                catalogItemId: parseInt(product.getAttribute("data-catalogitemid")),
                itemName: product.querySelector(".name").textContent,
                itemPrice: parseFloat(product.querySelector(".price").textContent.replace("$", "")),
                quantity: parseInt(product.querySelector(".count").textContent)
            };
            items.push(item);
        });
        // Перевірка, чи корзина порожня
        if (items.length === 0) {
            alert("Your basket is empty. Please add items before checking out.");
            return;
        }
        // Перевірка, чи всі поля заповнені
        if (!firstName || !lastName || !email || !phone || !address) {
            alert("Please fill in all fields before checking out.");
            return;
        }
        // Перевірка, чи номер телефону складається лише з цифр
        if (!/^\d+$/.test(phone)) {
            alert("Phone number must contain only digits.");
            return;
        }
        let totalPrice = items.reduce((sum, item) => sum + (item.itemPrice * item.quantity), 0);

        let orderData = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            phone: phone,
            address: address,
            totalPrice: totalPrice,
            items: items // Відправляємо всі товари
        };

        fetch("/Order/CreateOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(orderData)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                return response.json();
            })
            .then(data => {
                console.log("Order created successfully with ID:", data.orderId);
                initiateLiqPay(data.data, data.signature);
                // Очистити кошик після успішного оформлення замовлення
                localStorage.removeItem("basket");
            })

            .catch(error => {
                console.error("There was a problem with your fetch operation:", error.message);
            });
    });

    function initiateLiqPay(data, signature) {
        let form = document.createElement("form");
        form.method = "POST";
        form.action = "https://www.liqpay.ua/api/3/checkout";
        form.acceptCharset = "utf-8";

        let inputData = document.createElement("input");
        inputData.type = "hidden";
        inputData.name = "data";
        inputData.value = data;
        form.appendChild(inputData);

        let inputSignature = document.createElement("input");
        inputSignature.type = "hidden";
        inputSignature.name = "signature";
        inputSignature.value = signature;
        form.appendChild(inputSignature);

        document.body.appendChild(form);
        form.submit();
    }
});
