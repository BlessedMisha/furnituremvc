document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("checkout").addEventListener("click", function () {
        let firstName = document.querySelector("#firstName").value;
        let lastName = document.querySelector("#lastName").value;
        let email = document.querySelector("#email").value;
        let phone = document.querySelector("#phone").value;
        let address = document.querySelector("#address").value;

        let orderData = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            phone: phone,
            address: address,
            totalPrice: parseFloat(document.getElementById("total-price").textContent.replace("$", "")),
            catalogItemId: parseInt(document.querySelector(".basket-product").getAttribute("data-catalogitemid")),
            itemName: document.querySelector(".name").textContent,
            itemPrice: parseFloat(document.querySelector(".price").textContent.replace("$", "")),
            quantity: parseInt(document.querySelector(".count").textContent)
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
                // Додайте будь-які дії після успішного замовлення, наприклад, очищення кошика
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error.message);
            });
    });
});
