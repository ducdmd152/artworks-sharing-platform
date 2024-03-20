function showOutOfStockToastDanger(announceDanger, messageDanger) {
    var toastElement = document.getElementById('dangerToast');
    if (toastElement) {
        toastElement.querySelector('#announceDanger').innerText = announceDanger;
        toastElement.querySelector('#messageDanger').innerText = messageDanger;

        var cartToast = new bootstrap.Toast(toastElement);

        // Show the toast
        cartToast.show();


    } else {
        console.error("Toast element with ID 'dangerToast' not found.");
    }
}

function showOutOfStockToastSuccess(announceSuccess, messageSuccess) {
    var toastElement = document.getElementById('successToast');

    if (toastElement) {
        toastElement.querySelector('#announceSuccess').innerText = announceSuccess;
        toastElement.querySelector('#messageSuccess').innerText = messageSuccess;

        var cartToast = new bootstrap.Toast(toastElement);

        // Show the toast
        cartToast.show();


    } else {
        console.error("Toast element with ID 'dangerToast' not found.");
    }
}