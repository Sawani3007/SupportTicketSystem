console.log("tickets.js is connected");

const API_URL = "https://localhost:7226/api/tickets";
const pageSize = 11;

let currentPage = 1;
let tickets = [];
let editingTicketId = null;

const loadingMessage = document.getElementById("loadingMessage");
const errorMessage = document.getElementById("errorMessage");
const ticketTableBody = document.getElementById("ticketTableBody");
const searchInput = document.getElementById("searchInput");
const statusFilter = document.getElementById("statusFilter");
const priorityFilter = document.getElementById("priorityFilter");
const ticketForm = document.getElementById("ticketForm");

const submitButton = document.getElementById("submitButton");
const cancelEditButton = document.getElementById("cancelEditButton");
const formTitle = document.getElementById("formTitle");

const previousButton = document.getElementById("previousBtn");
const nextButton = document.getElementById("nextBtn");
const pageInfo = document.getElementById("pageInfo");


async function getTickets() {

    loadingMessage.textContent = "Loading tickets...";
    errorMessage.textContent = "";

    try {

        const params = new URLSearchParams();

        params.append("page", currentPage);
        params.append("pageSize", pageSize);

        const search = searchInput.value.trim();

        if (search !== "") {
            params.append("search", search);
        }

        if (statusFilter.value !== "") {
            params.append("status", statusFilter.value);
        }

        if (priorityFilter.value !== "") {
            params.append("priority", priorityFilter.value);
        }

        const response = await fetch(`${API_URL}?${params.toString()}`);

        if (!response.ok) {
            throw new Error(`Failed to fetch tickets. Status: ${response.status}`);
        }

        const result = await response.json();

        console.log("API Response:", result);

        tickets = Array.isArray(result.data) ? result.data : [];

        renderTickets(tickets);
        updatePagination(result);

        loadingMessage.textContent = "";

    } catch (error) {

        console.error(error);

        loadingMessage.textContent = "";
        errorMessage.textContent =
            "Unable to load tickets. Please check whether the API is running.";
    }
}


function renderTickets(ticketList) {

    ticketTableBody.innerHTML = "";

    if (ticketList.length === 0) {

        ticketTableBody.innerHTML = `
            <tr>
                <td colspan="8">No tickets found.</td>
            </tr>
        `;

        return;
    }

    ticketList.forEach(ticket => {

        const ticketId = ticket.id ?? ticket.ticketId;

        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${ticketId}</td>
            <td>${ticket.title}</td>
            <td>${ticket.description}</td>
            <td>${getPriorityName(ticket.priority)}</td>
            <td>${getStatusName(ticket.status)}</td>
            <td>${ticket.customerId}</td>
            <td>${ticket.agentId ?? "Not Assigned"}</td>
            <td>
                <button onclick="editTicket(${ticketId})">Edit</button>
                <button onclick="deleteTicket(${ticketId})">Delete</button>
            </td>
        `;

        ticketTableBody.appendChild(row);
    });
}


function getPriorityName(priority) {

    if (priority === 0 || priority === "Low") {
        return "Low";
    }

    if (priority === 1 || priority === "Medium") {
        return "Medium";
    }

    if (priority === 2 || priority === "High") {
        return "High";
    }

    return priority;
}


function getStatusName(status) {

    if (status === 0 || status === "Open") {
        return "Open";
    }

    if (status === 1 || status === "InProgress") {
        return "In Progress";
    }

    if (status === 2 || status === "Resolved") {
        return "Resolved";
    }

    if (status === 3 || status === "Closed") {
        return "Closed";
    }

    return status;
}


function editTicket(ticketId) {

    const ticket = tickets.find(
        ticket => (ticket.id ?? ticket.ticketId) === ticketId
    );

    if (!ticket) {
        errorMessage.textContent = "Ticket not found.";
        return;
    }

    editingTicketId = ticketId;

    document.getElementById("title").value = ticket.title;
    document.getElementById("description").value = ticket.description;
    document.getElementById("priority").value = ticket.priority;
    document.getElementById("status").value = ticket.status;
    document.getElementById("customerId").value = ticket.customerId;
    document.getElementById("agentId").value = ticket.agentId ?? "";

    formTitle.textContent = "Update Ticket";
    submitButton.textContent = "Update Ticket";
    cancelEditButton.style.display = "inline-block";

    ticketForm.scrollIntoView({
        behavior: "smooth"
    });
}
ticketForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    errorMessage.textContent = "";

    const ticket = {
        title: document.getElementById("title").value.trim(),
        description: document.getElementById("description").value.trim(),
        priority: Number(document.getElementById("priority").value),
        status: Number(document.getElementById("status").value),
        customerId: Number(document.getElementById("customerId").value),
        agentId: document.getElementById("agentId").value === ""
            ? null
            : Number(document.getElementById("agentId").value)
    };
    console.log("Ticket data:", ticket);
    try {
        let response;
        if (editingTicketId !== null) {
            response = await fetch(`${API_URL}/${editingTicketId}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(ticket)
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(
                    `Failed to update ticket. Status: ${response.status}. ${errorText}`
                );
            }

            alert("Ticket updated successfully!");

        } else {

            response = await fetch(API_URL, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(ticket)
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(
                    `Failed to add ticket. Status: ${response.status}. ${errorText}`
                );
            }
            alert("Ticket added successfully!");
        }
        ticketForm.reset();
        editingTicketId = null;
        formTitle.textContent = "Add Ticket";
        submitButton.textContent = "Add Ticket";
        cancelEditButton.style.display = "none";
        getTickets();
    } catch (error) {
        console.error(error);
        errorMessage.textContent = error.message;
    }
});
async function deleteTicket(ticketId) {
    const confirmDelete = confirm(
        "Are you sure you want to delete this ticket?"
    );
    if (!confirmDelete) {
        return;
    }
    try {
        const response = await fetch(`${API_URL}/${ticketId}`, {
            method: "DELETE"
        });
        if (!response.ok) {
            const errorText = await response.text();

            throw new Error(
                `Failed to delete ticket. Status: ${response.status}. ${errorText}`
            );
        }
        alert("Ticket deleted successfully!");
        if (tickets.length === 1 && currentPage > 1) {
            currentPage--;
        }
        getTickets();
    } catch (error) {
        console.error(error);
        errorMessage.textContent = error.message;
    }
}
searchInput.addEventListener("input", function () {
    currentPage = 1;
    getTickets();
});
statusFilter.addEventListener("change", function () {
    currentPage = 1;
    getTickets();
});
priorityFilter.addEventListener("change", function () {
    currentPage = 1;
    getTickets();
});
previousButton.addEventListener("click", function () {

    if (currentPage > 1) {
        currentPage--;
        getTickets();
    }

});
nextButton.addEventListener("click", function () {
    currentPage++;
    getTickets();

});
function updatePagination(result) {

    const totalCount = result.totalCount ?? 0;
    const returnedPageSize = result.pageSize ?? pageSize;

    const totalPages = Math.ceil(
        totalCount / returnedPageSize
    );

    if (totalPages > 0) {
        pageInfo.textContent =
            `Page ${result.page} of ${totalPages}`;
    } else {
        pageInfo.textContent = "Page 1 of 1";
    }

    previousButton.disabled = currentPage <= 1;
    nextButton.disabled =
        currentPage >= totalPages || totalPages === 0;
}
cancelEditButton.addEventListener("click", function () {
    ticketForm.reset();
    editingTicketId = null;
    formTitle.textContent = "Add Ticket";
    submitButton.textContent = "Add Ticket";
    cancelEditButton.style.display = "none";

    errorMessage.textContent = "";
});
getTickets();