window.showToastr = (type, message) => {
    if (!toastr) {
        console.error("Toastr n'est pas chargé !");
        return;
    }

    toastr.options = {
        closeButton: false,
        progressBar: true,
        positionClass: "toast-top-right",
        timeOut: "3000",
        extendedTimeOut: "1000",
        showDuration: "300",
        hideDuration: "300",
        showEasing: "swing",   // jQuery core supporte "swing" ou "linear"
        hideEasing: "swing",   // idem
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    };

    // Vérification du type pour éviter les erreurs
    const types = ["success", "info", "warning", "error"];
    if (types.includes(type)) {
        toastr[type](message);
    } else {
        toastr.info(message); // fallback si type inconnu
    }
}
