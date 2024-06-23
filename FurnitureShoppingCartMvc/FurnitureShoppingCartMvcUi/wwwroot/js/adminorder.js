// adminorder.js

document.addEventListener("DOMContentLoaded", function () {
    const prevPageLink = document.getElementById("prevPage");
    const nextPageLink = document.getElementById("nextPage");

    if (prevPageLink && nextPageLink) {
        prevPageLink.addEventListener("click", function (e) {
            e.preventDefault();
            let currentPage = parseInt(document.querySelector(".scrolling span").textContent);
            if (currentPage > 1) {
                currentPage--;
                fetchPage(currentPage);
            }
        });

        nextPageLink.addEventListener("click", function (e) {
            e.preventDefault();
            let currentPage = parseInt(document.querySelector(".scrolling span").textContent);
            currentPage++;
            fetchPage(currentPage);
        });
    }

    function fetchPage(page) {
        fetch(`/Admin/Orders?page=${page}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                return response.json();
            })
            .then(data => {
                updateTable(data);
                document.querySelector(".scrolling span").textContent = page;
                history.pushState(null, null, `/Admin/Orders?page=${page}`);
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });
    }

    function updateTable(data) {
        const ordersTable = document.querySelector("#orders-table tbody");
        ordersTable.innerHTML = "";

        data.forEach(order => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${order.id}</td>
                <td>${order.firstName}</td>
                <td>${order.lastName}</td>
                <td>${order.email}</td>
                <td>${order.phone}</td>
                <td>${order.address}</td>
                <td>${order.orderDate}</td>
                <td>
                    <ul>
                        ${order.orderItems.map(item => `<li>${item.itemName} - Quantity: ${item.quantity}</li>`).join("")}
                    </ul>
                </td>
                <td>${order.totalPrice}</td>
                <td>${order.isPaid}</td>
                <td>
                    <button class="btn-edit" data-order-id="${order.id}">Edit</button>
                    <button class="btn-delete" data-order-id="${order.id}">Delete</button>
                </td>
            `;
            ordersTable.appendChild(row);
        });

        attachEventListeners();
    }

    function attachEventListeners() {
        const editButtons = document.querySelectorAll(".btn-edit");
        const deleteButtons = document.querySelectorAll(".btn-delete");

        editButtons.forEach(button => {
            button.addEventListener("click", function (e) {
                e.preventDefault();
                const orderId = this.getAttribute("data-order-id");
                openEditModal(orderId);
            });
        });

        deleteButtons.forEach(button => {
            button.addEventListener("click", function (e) {
                e.preventDefault();
                const orderId = this.getAttribute("data-order-id");
                deleteOrder(orderId);
            });
        });

        const closeBtn = document.querySelector(".close");
        if (closeBtn) {
            closeBtn.addEventListener("click", function () {
                closeModal();
            });
        }

        const editForm = document.getElementById("editForm");
        if (editForm) {
            editForm.addEventListener("submit", function (e) {
                e.preventDefault();
                const formData = new FormData(editForm);
                saveEditedOrder(formData);
            });
        }
    }

    function openEditModal(orderId) {
        fetch(`/Admin/GetOrder/${orderId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                return response.json();
            })
            .then(order => {
                populateEditForm(order);
                document.getElementById("editModal").style.display = "block";
            })
            .catch(error => {
                console.error("Error fetching order details:", error);
            });
    }

    function populateEditForm(order) {
        document.getElementById("editOrderId").value = order.id;
        document.getElementById("editFirstName").value = order.firstName;
        document.getElementById("editLastName").value = order.lastName;
        document.getElementById("editEmail").value = order.email;
        document.getElementById("editPhone").value = order.phone;
        document.getElementById("editAddress").value = order.address;
        document.getElementById("editOrderDate").value = order.orderDate;
        document.getElementById("editTotalPrice").value = order.totalPrice;
        document.getElementById("editIsPaid").checked = order.isPaid;
    }

    function closeModal() {
        document.getElementById("editModal").style.display = "none";
    }

    function saveEditedOrder(formData) {
        fetch(`/Admin/EditOrder`, {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Id: formData.get("Id"),
                FirstName: formData.get("FirstName"),
                LastName: formData.get("LastName"),
                Email: formData.get("Email"),
                Phone: formData.get("Phone"),
                Address: formData.get("Address"),
                OrderDate: formData.get("OrderDate"),
                TotalPrice: formData.get("TotalPrice"),
                IsPaid: formData.get("IsPaid") === "on"
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                closeModal();
                fetchPage(parseInt(document.querySelector(".scrolling span").textContent));
            })
            .catch(error => {
                console.error("Error saving order:", error);
            });
    }

    function deleteOrder(orderId) {
        if (confirm("Are you sure you want to delete this order?")) {
            fetch(`/Admin/DeleteOrder/${orderId}`, {
                method: "DELETE"
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Network response was not ok");
                    }
                    fetchPage(parseInt(document.querySelector(".scrolling span").textContent));
                })
                .catch(error => {
                    console.error("Error deleting order:", error);
                });
        }
    }

    attachEventListeners();
});
