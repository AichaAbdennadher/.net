window.showNotification = (message, type) => {
    // type = "success", "error", "warning"
    const bg = type === "success" ? "green" : type === "error" ? "red" : "yellow";
    const toast = document.createElement("div");
    toast.textContent = message;
    toast.className = `fixed top-5 right-5 bg-${bg}-500 text-white px-4 py-2 rounded shadow-lg`;
    document.body.appendChild(toast);
    setTimeout(() => { toast.remove(); }, 4000);
};
