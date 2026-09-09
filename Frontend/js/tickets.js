console.log("tickets.js is connected");

const API_URL = "https://localhost:7226/api/tickets";
const loadingMessage = document.getElementById("loadingMessage");
const errorMessage = document.getElementById("errorMessage");
const ticketTableBody = document.getElementById("ticketTableBody");
const searchInput = document.getElementById("searchInput");
const ticketForm = document.getElementById("ticketForm");
let tickets = [];

async function getTickets() {

    loadingMessage.textContent = "Loading tickets...";
    errorMessage.textContent = "";

    try {
        const response = await fetch(API_URL);

        if (!response.ok) {
            throw new Error(
                `Failed to fetch tickets. Status: ${response.status}`
            );
        }

        const result = await response.json();

        console.log("API Response:", result);

        if (Array.isArray(result)) {
            tickets = result;
        }
        else if (Array.isArray(result.data)) {
            tickets = result.data;
        }
        else {
            tickets = [];
        }
        renderTickets(tickets);
        loadingMessage.textContent = "";
    }
    catch (error) {
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
                <td colspan="8">
                    No tickets found.
                </td>
            </tr>
        `;

        return;
    }
    ticketList.forEach(ticket => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${ticket.id ?? ticket.ticketId}</td>
            <td>${ticket.title}</td>
            <td>${ticket.description}</td>
            <td>${getPriorityName(ticket.priority)}</td>
            <td>${getStatusName(ticket.status)}</td>
            <td>${ticket.customerId}</td>
            <td>${ticket.agentId ?? "Not Assigned"}</td>
            <td>
                <button onclick="deleteTicket(${ticket.id ?? ticket.ticketId})">
                    Delete
                </button>
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

ticketForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    errorMessage.textContent = "";
    const ticket = {

        title: document.getElementById("title").value,

        description: document.getElementById("description").value,

        priority: Number(
            document.getElementById("priority").value
        ),

        status: Number(
            document.getElementById("status").value
        ),

        customerId: Number(
            document.getElementById("customerId").value
        ),

        agentId: document.getElementById("agentId").value
            ? Number(document.getElementById("agentId").value)
            : null
    };
    console.log("Sending ticket:", ticket);
    try { 
        const response = await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(ticket)
        });
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(
                `Failed to add ticket: ${response.status} ${errorText}`
            )
        }

        const createdTicket = await response.json();
        console.log("Created ticket:", createdTicket);
        alert("Ticket added successfully!");
        ticketForm.reset();
        getTickets();
    }
    catch (error) {
        console.error(error);
        errorMessage.textContent =
            "Unable to add ticket. Please check the API and entered data.";
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
        const response = await fetch(
            `${API_URL}/${ticketId}`,
            {
                method: "DELETE"
            }
        );
        if (!response.ok) {
            throw new Error(
                `Failed to delete ticket. Status: ${response.status}`
            );
        }
        alert("Ticket deleted successfully!");
        getTickets();

    }
    catch (error) {
        console.error(error);
        errorMessage.textContent =
            "Unable to delete ticket.";
    }
}
searchInput.addEventListener("input", function () {
    const searchText =
        searchInput.value.toLowerCase();
    const filteredTickets = tickets.filter(ticket => {
        return (
            String(ticket.id ?? ticket.ticketId)
                .toLowerCase()
                .includes(searchText)
            ||
            String(ticket.title)
                .toLowerCase()
                .includes(searchText)
            ||
            String(ticket.description)
                .toLowerCase()
                .includes(searchText)
            ||
            getPriorityName(ticket.priority)
                .toLowerCase()
                .includes(searchText)
            ||
            getStatusName(ticket.status)
                .toLowerCase()
                .includes(searchText)
        );
    });
    renderTickets(filteredTickets);
});
getTickets();