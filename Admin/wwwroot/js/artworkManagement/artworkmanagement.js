var filters = {
    ArtworkTitle: null,
    ArtworkID: null,
    Date: null,
    ArtworkName: null,
    Status: null,
    PageNumber: 1,
    PageSize: 6,
};

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
        success: function (partialHtml) {
            // Update the content of the partial container
            if(partialHtml.result === 'Error'){
                showOutOfStockToastDanger('Not found', 'Please check again the condition');
                return;
            }
            $('#listPartial').html(partialHtml);
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
        success: function (partialHtml) {
            // Update the content of the partial container
            if(partialHtml.result === 'Error'){
                showOutOfStockToastDanger('Not found', 'Please check again the condition');
                return;
            }
            $('#listPartial').html(partialHtml);
        },
        error: function (error) {
            console.error('Error loading partial:', error);
        }
    });
}
