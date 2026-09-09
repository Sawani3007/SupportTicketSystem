console.log("customers.js is connected");

const API_URL = "https://localhost:7226/api/customers";
const loadingMessage = document.getElementById("loadingMessage");
const errorMessage = document.getElementById("errorMessage");
const customerTableBody = document.getElementById("customerTableBody");
const searchInput = document.getElementById("searchInput");
const customerForm = document.getElementById("customerForm");

let customers = [];
async function getCustomers() {
    loadingMessage.textContent = "Loading customers...";
    errorMessage.textContent = "";
    try {
        const response = await fetch(API_URL);
        if (!response.ok) {
            throw new Error(
                `Failed to fetch customers. Status: ${response.status}`
            );
        }
        const result = await response.json();
        console.log("API Response:", result);
        if (Array.isArray(result)) {
            customers = result;
        }
        else if (Array.isArray(result.data)) {
            customers = result.data;
        }
        else {
            customers = [];
        }
        renderCustomers(customers);
        loadingMessage.textContent = "";
    }
    catch (error) {
        console.error(error);
        loadingMessage.textContent = "";
        errorMessage.textContent =
            "Unable to load customers. Please check whether the API is running.";
    }
}
function renderCustomers(customerList) {
    customerTableBody.innerHTML = "";
    if (customerList.length === 0) {
        customerTableBody.innerHTML = `
            <tr>
                <td colspan="4">
                    No customers found.
                </td>
            </tr>
        `;
        return;
    }
    customerList.forEach(customer => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${customer.id ?? customer.customerId}</td>
            <td>${customer.name}</td>
            <td>${customer.email}</td>
            <td>${customer.phone}</td>

        `;
        customerTableBody.appendChild(row);
    });

}
customerForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    errorMessage.textContent = "";
    const customer = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value
    };
    console.log("Sending customer:", customer);
    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: {

                "Content-Type": "application/json"
            },
            body: JSON.stringify(customer)

        });
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(
                `Failed to add customer: ${response.status} ${errorText}`
            );
        }
        const createdCustomer = await response.json();
        console.log("Created customer:", createdCustomer);
        alert("Customer added successfully!");
        customerForm.reset();
        getCustomers();

    }
    catch (error) {
        console.error(error);
        errorMessage.textContent =
            "Unable to add customer. Please check the API and entered data.";
    }
});
searchInput.addEventListener("input", function () {
    const searchText =
        searchInput.value.toLowerCase();
    const filteredCustomers = customers.filter(customer => {
        return (
            String(customer.id ?? customer.customerId)
                .toLowerCase()
                .includes(searchText)
            ||
            String(customer.name)
                .toLowerCase()
                .includes(searchText)
            ||
            String(customer.email)
                .toLowerCase()
                .includes(searchText)
            ||
            String(customer.phone)
                .toLowerCase()
                .includes(searchText)
        );
    });
    renderCustomers(filteredCustomers);

});
getCustomers();