DetailsRequestService = function () {

    var deleteRequest = function (url, id) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    'id': id
                },
                datatype: 'json'
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject('Error while deleting!');
            });
        });
    };

    var getChangeStatusModal = function (url, id) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    id: id
                },
                datatype: 'html'
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject('Error fetching data');
            });

        });
    };

    var changeStatus = function (url, data) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                datatype: 'json'
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject('Error saving data!');
            });

        });

    };
    
    
    return {
        deleteRequest: deleteRequest,
        getChangeStatusModal: getChangeStatusModal,
        changeStatus: changeStatus
    };
}();