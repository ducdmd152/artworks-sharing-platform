var filters = {
    ReportId: null,
    Status: 1,
    PageNumber: 1,
    PageSize: 6,
}

var updateModel = {
    reportId: null,
    reason: null,
    mode: null
}

function showDetailArtwork(postId, reportId){
    var report = {
        reportId: reportId,
        postId: postId,
    }

    $.ajax({
        url: '/Moderator/ReportManagement?handler=GetReportDetail', // Replace with your actual controller and action
        type: 'POST',
        contentType: 'application/json',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(report),
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

                var buttonActionDiv = document.getElementById("buttonAction");
                if (data.Reports[0].Status === reportStatus.Reviewed) {
                    // Hide the div with id "buttonAction"
                    if (buttonActionDiv) {
                        buttonActionDiv.style.display = "none";
                    }
                }else {
                    buttonActionDiv.style.display = "block";
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

function banThePost() {
    // Create an input field for the reason
    var reason = prompt("Please provide a reason for banning this post:");

    // If the user cancels or doesn't provide a reason, exit the function
    if (!reason) {
        showOutOfStockToastDanger('Reviewed','Ban canceled by user or no reason provided.');
        return;
    }

    // If the user provides a reason, proceed with banning the post
    updateModel.mode = modeUpdateReport.BanThePost;
    updateModel.reason = reason; // Add reason to the updateModel
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
            if (response.result === 'Ok') {
                showOutOfStockToastSuccess('Reviewed', 'Ban successful!');
            } else {
                showOutOfStockToastDanger('Reviewed', 'Ban failed');
            }
            getListByPaging(1);
            $('#artworkDetailModal').modal('hide');
            console.log('Post banned successfully:', response);
        },
        error: function (error) {
            // Handle error response here
            console.error('Error banning post:', error);
        }
    });
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

function updateSearchCondition(){
    var filter1Select = document.getElementById('search-key');
    var filter1Input = document.querySelector('.form-control');

    filters[filter1Select.value === '1' ? 'ReportId' : null] = filter1Input.value === '' ? null : filter1Input.value;;

    // Get value from the second filter section
    var filter2Select = document.getElementById('search-status');
    filters.Status = filter2Select.value;
}

function getListBySearch(modeSearch) {
    if(modeSearch === 1){
        filters.ReportId = null;
    }
    updateSearchCondition();
    filters.PageNumber = 1;
    console.log(filters);
    $.ajax({
        url: '/Moderator/ReportManagement?handler=Search', // Replace with your actual controller and action
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

const reportStatus ={
    Pending: 1,
    Reviewed: 2
}