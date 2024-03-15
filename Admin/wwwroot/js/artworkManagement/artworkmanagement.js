var filters = {
    ArtworkTitle: null,
    ArtworkID: null,
    Date: null,
    ArtworkName: null,
    Status: null,
    PageNumber: 1,
    PageSize: 6,
};

var updateModel = {
    postId: null,
    mode: null
}

function updateSearchCondition(){
    var filter1Select = document.getElementById('search-key');
    var filter1Input = document.querySelector('.form-control');

    filters[filter1Select.value === '1' ? 'ArtworkTitle' :
        filter1Select.value === '2' ? 'ArtworkID' :
            filter1Select.value === '3' ? 'Date' :
                filter1Select.value === '4' ? 'ArtworkName' : null] = filter1Input.value;

    // Get value from the second filter section
    var filter2Select = document.getElementById('search-status');
    if(filter2Select.value !== 'null'){
        filters.Status = filter2Select.value;
    }
}
function getListBySearch(modeSearch) {
    if(modeSearch === 1){
        filters.ArtworkTitle = null;
        filters.ArtworkID = null;
        filters.Date = null;
        filters.ArtworkName = null;
    }
    updateSearchCondition();
    filters.PageNumber = 1;
    console.log(filters);
    $.ajax({
        url: '/Moderator/ArtWorksManagement?handler=Search', // Replace with your actual controller and action
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

function getListByPaging(pageNumber) {
    updateSearchCondition();
    filters.PageNumber = pageNumber;
    console.log(filters);
    $.ajax({
        url: '/Moderator/ArtWorksManagement?handler=Paging', // Replace with your actual controller and action
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

function showDetailArtwork(postId) {
    $.ajax({
        url: '/Moderator/ArtWorksManagement?handler=GetProductDetail', // Replace with your actual controller and action
        type: 'POST',
        contentType: 'application/json',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(postId),
        success: function (response) {
            // Update the content of the partial container
           if(response.result === 'Ok'){
               var data = JSON.parse(response.data);
               updateModel.postId = data.PostId;
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

function onCloseClick() {
    $('#artworkDetailModal').modal('hide');
}

function approveArtwork() {
    var confirmReject = confirm("Are you sure you want to approve this post?");

    // If user confirms, proceed with rejection
    if (confirmReject) {
        updateModel.mode = modeUpdate.Approved;
        $.ajax({
            url: '/Moderator/ArtWorksManagement?handler=ApprovedOrRejectArtwork',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(updateModel),
            success: function (response) {
                // Handle success response here
                if(response.result === 'Ok'){
                    showOutOfStockToastSuccess('Approve', 'Approve successfully!' );
                }else{
                    showOutOfStockToastSuccess('Approve', 'Approve fail');
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

function rejectArtwork() {
    // Show confirmation dialog
    var confirmReject = confirm("Are you sure you want to reject this post?");

    // If user confirms, proceed with rejection
    if (confirmReject) {
        updateModel.mode = modeUpdate.Reject;
        $.ajax({
            url: '/Moderator/ArtWorksManagement?handler=ApprovedOrRejectArtwork',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(updateModel),
            success: function (response) {
                // Handle success response here
                if(response.result === 'Ok'){
                    showOutOfStockToastDanger('Reject', 'Reject successfully!' );
                }else{
                    showOutOfStockToastDanger('Reject', 'Reject fail');
                }
                getListByPaging(1);
                $('#artworkDetailModal').modal('hide');
                console.log('Post rejected successfully:', response);
            },
            error: function (error) {
                // Handle error response here
                console.error('Error rejecting post:', error);
            }
        });
    } else {
        // If user cancels, do nothing or provide feedback
        console.log('Rejection canceled by user.');
    }
}

const modeUpdate = {
    Approved : 2,
    Reject : 3,
}
