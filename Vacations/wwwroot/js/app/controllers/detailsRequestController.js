var detailsRequestController = function (messageService, detailsRequestService) {

    // properties
    var id;
    var statusEnums;

    // urls
    var urlDetailsRequest;
    var urlDeleteRequest;
    var urlMyRequests;
    var urlGetChangeRequestStatusModal;
    var urlChangeStatus;

    //#region initialize urls

    var initializeUrls = function (urlInfo) {
        urlDetailsRequest = urlInfo.detailsRequestUrl;
        urlDeleteRequest = urlInfo.deleteRequestUrl;
        urlMyRequests = urlInfo.myRequestsUrl;
        urlGetChangeRequestStatusModal = urlInfo.getChangeRequestStatusModalUrl;
        urlChangeStatus = urlInfo.changeRequestStatusUrl;
    }
    //#endregion initialize urls

    //#region init click events documents table

    var initializeDocumentsClickEvents = function () {
        documentsTable.on('click',
            '.btn',
            function () {
                documentId = $(this).data('id');
                rowDocument = documentsTable.api().row($(this).parents("tr"));

                if ($(this).hasClass('js-lockDocument')) {
                    confirmLockDocument();
                } else if ($(this).hasClass('js-unlockDocument')) {
                    confirmUnlockDocument();
                } else if ($(this).hasClass('js-editDocument')) {
                    handleGetEditDocumentModal();
                } else if ($(this).hasClass('js-deleteDocument')) {
                    confirmDeleteDocument();
                }
            });

        $('.js-createDocument').on('click',
            function () {
                handleGetCreateDocumentModal();
            });
    };

    //#endregion init click events documents table

    //#region initialize datatables

    var initializeDatatables = function () {

        $('#dtReplacementEmployees').DataTable({
            'pagingType': 'numbers',
            responsive: true,
            order: [[0, "asc"]],
            lengthMenu: [10, 25, 50],
            'columns': [
                {data: 'fullName'},
                {data: 'department'}
            ],
            'columnDefs': [
                {className: 'dt-center', targets: "_all"},
                {responsivePriority: 1, targets: 0},
                {responsivePriority: 2, targets: -1}
            ]
        });

        $('#dtResponses').DataTable({
            'pagingType': 'numbers',
            responsive: true,
            order: [[2, "desc"]],
            lengthMenu: [10, 25, 50],
            'columns': [
                {
                    data: 'statusId',
                    render: function (data, type, request) {
                        
                        var statusInt = parseInt(request.statusId);
                        
                        if (statusInt === statusEnums.pending) {
                            return "<span class='badge badge-secondary'>Pending</span>";
                        } else if (statusInt === statusEnums.approved) {
                            return "<span class='badge badge-success'>Approved</span>";
                        } else if (statusInt === statusEnums.rejected) {
                            return "<span class='badge badge-warning'>Rejected</span>";
                        } else if (statusInt === statusEnums.invalid) {
                            return "<span class='badge badge-danger'>Invalid</span>";
                        }
                        return "";
                    }
                },
                {data: 'employee'},
                {data: 'dateCreated'},
                {data: 'description'}
            ],
            'columnDefs': [
                {className: 'dt-center', targets: "_all"},
                { type: 'date-eu', targets: 2 },
                {responsivePriority: 1, targets: 0},
                {responsivePriority: 2, targets: -1}
            ]
        });
    };

    //#endregion initialize datatables

    //#region delete

    var processDeleteRequest = function (result) {
        if (result.success) {
            window.location.href = urlMyRequests;
        } else {
            messageService.showAlertMessage('danger', result.message);
        }
    };

    var deleteRequest = function () {
        detailsRequestService.deleteRequest(urlDeleteRequest, id)
            .then(processDeleteRequest)
            .catch(function (errorMessage) {
                messageService.showAlertMessage(errorMessage, "error", "3000", "toast-top-center");

            });
    };

    var confirmDeleteRequest = function () {

        messageService.showConfirmationDialogDelete(
            "Delete request",
            "You cannot revert this!",
            "Delete anyway",
            "Cancel",
            deleteRequest,
            messageService.cancelAction
        );
    };

    //#endregion delete

    //#region change status

    var processChangeStatusResult = function (result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
            window.location.href = urlDetailsRequest;
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var handleGetChangeStatusModalResult = function (htmlResult) {

        $('#changeStatusPlaceholder').html(htmlResult);
        $('#changeStatusPlaceholder #changeStatusModal').modal('show');
        $('#changeStatusPlaceholder #changeStatusModal').find('.select2').select2({
            dropdownParent: $('#changeStatusModal'),
        });
        revalidateForm();
        markInputsRequired();
    };

    var handleGetChangeStatusClickEvent = function () {

        detailsRequestService.getChangeStatusModal(urlGetChangeRequestStatusModal, id)
            .then(handleGetChangeStatusModalResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');

            });

    };

    //#endregion change status

    var init = function (urlInfo, idInfo, statusEnumsInfo) {

        id = idInfo;
        statusEnums = statusEnumsInfo;
        initializeUrls(urlInfo);
        initializeDatatables();

        //click events
        $('.js-deleteRequest').on('click', function () {
            confirmDeleteRequest();
        });

        $('.js-changeStatus').on('click', handleGetChangeStatusClickEvent);

        $('#changeStatusPlaceholder').on('change', '.js-ddlStatus', function () {
            var statusId = $(this).val();

            if (statusId == statusEnums.rejected || statusId == statusEnums.invalid) {
                $('#changeStatusPlaceholder').find('.js-statusMessageContainer').removeClass('d-none');
                document.getElementById("js-description").required = true;
            } else {
                $('#changeStatusPlaceholder').find('.js-statusMessageContainer').addClass('d-none');
                document.getElementById("js-description").required = false;
            }

            markInputsRequired();
        });

        $('#formChangeStatus').on('submit',
            function (e) {

                e.preventDefault();

                if ($(this).valid()) {

                    var data = $(this).serialize();

                    $('#changeStatusPlaceholder #changeStatusModal').modal('hide');
                    detailsRequestService.changeStatus(urlChangeStatus, data)
                        .then(processChangeStatusResult)
                        .catch(function (errorMessage) {
                            messageService.showToastrMessage(errorMessage, 'error');

                        });
                }
            });

    };

    function revalidateForm() {
        var form = $('#formChangeStatus');
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    };

    return {
        init: init
    };

}(MessageService, DetailsRequestService);