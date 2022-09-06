var editEmployeeController = function (messageService, editEmployeeService) {

    // properties
    var id;
    var companyId;

    // urls
    var urlGetDepartmentsPartial;

    //#region initialize urls

    var initializeUrls = function (urlInfo) {
        urlGetDepartmentsPartial = urlInfo.getDepartmentsPartial;
    };
    
    //#endregion initialize urls

    
    var initializeSelect2 = function(){
        $('.select2').select2();
    };
    
    var handleGetDepartmentsPartialResult = function(htmlResult){
        $('.departmentContainer').empty().html(htmlResult);
        $('.departmentContainer').find('.select2').select2();
    }

    var init = function (urlInfo, idInfo, companyIdInfo) {

        id = idInfo;
        companyId = companyIdInfo;
        initializeUrls(urlInfo);
        initializeSelect2();
        markInputsRequired();

        //click events

        $('.js-ddlCompanies').on('change', function () {
            chosenCompanyId = $(this).val();

            if(companyId != chosenCompanyId && chosenCompanyId != ''){
                companyId = chosenCompanyId;
                editEmployeeService.getDepartmentsPartial(urlGetDepartmentsPartial, chosenCompanyId)
                    .then(handleGetDepartmentsPartialResult)
                    .catch(function (errorMessage) {
                        messageService.showToastrMessage(errorMessage, 'error');

                    });    
            }
            
        });

    };


    return {
        init: init
    };

}(MessageService, EditEmployeeService);