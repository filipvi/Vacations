//custom sort for sifra column
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "sifra-pre": function (a) {
        var x = a.split('/');
        return parseInt(x[0]);
    },

    "sifra-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "sifra-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});


//custom sort for date column
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-eu-pre": function (date) {
        if (date == null || date == '' || date === "N/A") {
            date = "01.01.1900.";
        }

        //var date = date.replace(" ", "");

        var euDate = date.split('.');

        var year;

        /*year (optional)*/
        if (euDate[2]) {
            year = euDate[2];
        } else {
            year = 0;
        }

        /*month*/
        var month = euDate[1];
        if (month.length == 1) {
            month = 0 + month;
        }

        /*day*/
        var day = euDate[0];
        if (day.length == 1) {
            day = 0 + day;
        }

        return (year + month + day) * 1;
    },

    "date-eu-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-eu-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});