function changeMainScreenView (typeUpdate){
    const form = document.getElementById('left-form');
    form.action = 'ModeratorEditProfile?handler=ChangeTypeEdit';
    form.querySelector('input[name="AccountUpdateType.TypeUpdate"]').value = typeUpdate;
    form.submit();
}

function updateProfileInfo() {
    var form = document.forms.namedItem("updateProfileInfo");
    var $form = $(form);

    // Trigger validation
    $form.validate();

    if ($form.valid()) {
        var data = new FormData(form);

        $.ajax({
            url: '/Moderator/ModeratorEditProfile?handler=UpdateProfileInfo',
            type: 'POST',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: data,
            processData: false,  // Don't process the data (already FormData)
            contentType: false,  // Don't set contentType (let jQuery handle it)
            success: function (response) {
                if (response.result === 'Error') {
                    showOutOfStockToastDanger('Fail', response.data);
                } else {
                    showOutOfStockToastSuccess('Upload', response.data);
                    setTimeout(function () {
                        window.location.href = '/Creator/EditProfile';
                    }, 2000);
                }
            },
            error: function (error) {
                console.error('Error loading partial:', error);
            }
        });
    } else {
        console.error('Client-side validation failed.');
    }
}

function changePassword() {
    var form = document.forms.namedItem("changePassword");
    var $form = $(form);

    // Trigger validation
    $form.validate();

    if ($form.valid()) {
        var data = new FormData(form);

        $.ajax({
            url: '/Moderator/ModeratorEditProfile?handler=ChangePassword',
            type: 'POST',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: data,
            processData: false,  // Don't process the data (already FormData)
            contentType: false,  // Don't set contentType (let jQuery handle it)
            success: function (response) {
                if (response.result === 'Error') {
                    showOutOfStockToastDanger('Fail', response.data);
                } else {
                    showOutOfStockToastSuccess('Success', response.data);
                    setTimeout(function () {
                        window.location.href = '/Authenticate/Login';
                    }, 2000);
                }
            },
            error: function (error) {
                console.error('Error loading partial:', error);
            }
        });
    } else {
        console.error('Client-side validation failed.');
    }
}