var filters = {
    Status: 1,
    PageNumber: 1,
    PageSize: 6,
}

var updateModel = {
    reportId: null,
    mode: null
}

function showDetailArtwork(postId, reportId){
    $.ajax({
        url: '/Moderator/ReportManagement?handler=GetReportDetail', // Replace with your actual controller and action
        type: 'POST',
        contentType: 'application/json',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(postId),
        success: function (response) {
            // Update the content of the partial container
            if(response.result === 'Ok'){
                updateModel.reportId = reportId;
                var data = JSON.parse(response.data);
                document.querySelector('#modalArtworkTitle').textContent = data.Title;
                document.querySelector('#modalImagePreview').src = data.Images[0].ImageUrl;
                document.querySelector('#modalDescription').textContent = data.Description;
                document.querySelector('#modelAvatarImage').src = data.Artist.Account.Avatar;
                document.querySelector('#modelArtistName').textContent = data.Artist.Account.FirstName + ' ' + data.Artist.Account.LastName;
                document.querySelector('#modalVisibility').textContent = showScope(data.Scope);
                var visibilitySpan = document.querySelector('#modelBorderVisibility');
                visibilitySpan.classList.remove('bg-success', 'bg-danger', 'bg-purple', 'text-white');

                switch(data.Scope) {
                    case 1:
                        visibilitySpan.classList.add('bg-success', 'text-white');
                        break;
                    case 2:
                        visibilitySpan.classList.add('bg-purple', 'text-white'); // Red background
                        break;
                    case 3:
                        visibilitySpan.classList.add('bg-danger', 'text-white'); // Purple background
                        break;
                    default:
                        // Handle other cases if needed
                        break;
                }
                $('#artworkDetailModal').modal('show');
            }else{
                updateModel.postId = null;
            }
        },
        error: function (error) {
            console.error('Error loading partial:', error);
        }
    });
}

function skipReport(){
    var confirmReject = confirm("Are you sure you want to skip this report?");

    // If user confirms, proceed with rejection
    if (confirmReject) {
        updateModel.mode = modeUpdateReport.Reviewed;
        $.ajax({
            url: '/Moderator/ReportManagement?handler=SkipOrBanPost',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(updateModel),
            success: function (response) {
                // Handle success response here
                if(response.result === 'Ok'){
                    showOutOfStockToastSuccess('Reviewed', 'Reviewed successfully!' );
                }else{
                    showOutOfStockToastDanger('Reviewed', 'Reviewed fail');
                }
                getListByPaging(1);
                $('#artworkDetailModal').modal('hide');
            },
            error: function (error) {
                // Handle error response here
                console.error('Error approving artwork:', error);
            }
        });
    } else {
        // If user cancels, do nothing or provide feedback
        console.log('Rejection canceled by user.');
    }
}

function banThePost(){
    var confirmReject = confirm("Are you sure you want to ban this post from this report?");

    // If user confirms, proceed with rejection
    if (confirmReject) {
        updateModel.mode = modeUpdateReport.BanThePost;
        $.ajax({
            url: '/Moderator/ReportManagement?handler=SkipOrBanPost',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(updateModel),
            success: function (response) {
                // Handle success response here
                if(response.result === 'Ok'){
                    showOutOfStockToastSuccess('Reviewed', 'Ban successfully!' );
                }else{
                    showOutOfStockToastDanger('Reviewed', 'Ban fail');
                }
                getListByPaging(1);
                $('#artworkDetailModal').modal('hide');
                console.log('Approved artwork successfully:', response);
            },
            error: function (error) {
                // Handle error response here
                console.error('Error approving artwork:', error);
            }
        });
    } else {
        // If user cancels, do nothing or provide feedback
        console.log('Rejection canceled by user.');
    }
}

function banTheArtist(){
    var confirmReject = confirm("Are you sure you want to ban this artist from this report?");

    // If user confirms, proceed with rejection
    if (confirmReject) {
        updateModel.mode = modeUpdateReport.BanTheArtist;
        $.ajax({
            url: '/Moderator/ReportManagement?handler=SkipOrBanPost',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(updateModel),
            success: function (response) {
                // Handle success response here
                if(response.result === 'Ok'){
                    showOutOfStockToastSuccess('Reviewed', 'Ban successfully!' );
                }else{
                    showOutOfStockToastDanger('Reviewed', 'Ban fail');
                }
                getListByPaging(1);
                $('#artworkDetailModal').modal('hide');
                console.log('Approved artwork successfully:', response);
            },
            error: function (error) {
                // Handle error response here
                console.error('Error approving artwork:', error);
            }
        });
    } else {
        // If user cancels, do nothing or provide feedback
        console.log('Rejection canceled by user.');
    }
}

function getListByPaging(pageNumber) {
    filters.PageNumber = pageNumber;
    console.log(filters);
    $.ajax({
        url: '/Moderator/ReportManagement?handler=Paging', // Replace with your actual controller and action
        type: 'POST',
        contentType: 'application/json',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(filters),
        success: function (response) {
            // Update the content of the partial container
            if(response.result === 'Error'){
                showOutOfStockToastDanger('Not found', 'Please check again the condition');
                return;
            }
            $('#listPartial').html(response.partial2.result);
            $('#pagingPartial').html(response.partial1.result);
        },
        error: function (error) {
            console.error('Error loading partial:', error);
        }
    });
}

function showScope(scope){
    switch (scope){
        case 1:
            return 'Public';
        case 2:
            return 'Subscriber';
        case 3:
            return 'Private';
    }
}


const modeUpdateReport = {
    Reviewed : 1,
    BanThePost : -1,
    BanTheArtist: -2,
}
