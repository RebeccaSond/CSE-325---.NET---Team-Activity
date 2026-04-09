window.closeModal = (id) => {
    const modalElement = document.getElementById(id);
    const modal = bootstrap.Modal.getInstance(modalElement);
    if (modal) modal.hide();
};