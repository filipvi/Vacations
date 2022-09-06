var indexRequestController = function (messageService, indexRequestService) {

    // properties
    var table;
    var statusId;

    // urls
    var urlGetData;
    var urlGetRequestDetails;

    //#region initialize urls

    var initializeUrls = function (urlInfo) {
        urlGetData = urlInfo.getDataUrl;
        urlGetRequestDetails = urlInfo.getRequestDetailsUrl;
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

        table = $('#dtRequests').DataTable({
            'pagingType': 'numbers',
            'processing': true,
            'serverSide': true,
            responsive: true,
            order: [[2, "desc"]],
            lengthMenu: [10, 25, 50],
            'ajax':
                {
                    'url': urlGetData,
                    'data': { 'statusId': statusId },
                    'type': 'POST',
                    'dataType': 'JSON'
                },
            'columns': [
                { data: 'employee' },
                { data: 'company' },
                { data: 'department' },
                { data: 'dateCreated' },
                { data: 'dateFrom' },
                { data: 'dateTo' },
                { data: 'year' },
                { data: 'durationInWorkingDays' },
                { data: 'durationInDays' },
                { data: 'id' }
            ],
            'columnDefs': [
                { className: 'dt-center', targets: "_all" },
                { orderable: false, targets: "no-sort" },
                {
                    'render': function (data, type, row, full) {
                        return dataRenderSamePage(urlGetRequestDetails, data, 'Request details', 'btn-primary', 'fa-info-circle text-white');
                    },
                    'targets': 9
                },
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ]
        });

        $('.js-ddlEmployees').on('change', function () {
            var val = $(this).val();

            if (table.column(0).search() !== val) {
                table.column(0).search(val).draw();
            }
        });

        $('.js-ddlCompanies').on('change', function () {
            var val = $(this).val();

            if (table.column(1).search() !== val) {
                table.column(1).search(val).draw();
            }
        });

        $('.js-ddlDepartments').on('change', function () {
            var val = $(this).val();

            if (table.column(2).search() !== val) {
                table.column(2).search(val).draw();
            }
        });
        
        $('.js-dateCreated').on('change', function () {
            var val = $(this).val();

            if (table.column(3).search() !== val) {
                table.column(3).search(val).draw();
            }
        });

        $('.js-dateFrom').on('change', function () {
            var val = $(this).val();

            if (table.column(4).search() !== val) {
                table.column(4).search(val).draw();
            }
        });

        $('.js-dateTo').on('change', function () {
            var val = $(this).val();

            if (table.column(5).search() !== val) {
                table.column(5).search(val).draw();
            }
        });

        $('.js-ddlYear').on('change', function () {
            var val = $(this).val();

            if (table.column(6).search() !== val) {
                table.column(6).search(val).draw();
            }
        });

        $('.js-resetFilter').on('click',
            function () {
                $('.js-ddlEmployees').val('').trigger('change');
                $('.js-ddlCompanies').val('').trigger('change');
                $('.js-ddlDepartments').val('').trigger('change');
                $('.js-dateCreated').val('');
                $('.js-dateFrom').val('');
                $('.js-dateTo').val('');
                $('.js-ddlYear').val('').trigger('change');

                table.search('').columns().search('').draw();
            });
    };

    //#endregion initialize datatables
    
    var init = function (urlInfo, statusIdInfo) {

        statusId = statusIdInfo;
        initializeUrls(urlInfo);
        initializeDatatables();
            
        $('.select2').select2();

        $('.datepicker').datepicker({
            language: 'hr-HR',
            format: 'dd.mm.yyyy',
            autoclose: true,
            clearBtn: true,
            todayHighlight: true,
            showClear: true,
            orientation: 'bottom'
        });
    };

    return {
        init: init
    };

}(MessageService, IndexRequestService);