EditEmployeeService = function () {

    var getDepartmentsPartial = function(url, companyId) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    companyId: companyId
                },
                datatype: 'html'
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject('Error fetching data!');
            });

        });

    };

    return {
        getDepartmentsPartial: getDepartmentsPartial,
    };
}();
