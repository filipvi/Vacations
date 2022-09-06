//Get pdv multiplier
function definePdvMultiplier(pdv) {

    var pdvEnums = {
        pdv0: 5,
        pdv5: 4,
        pdv10: 3,
        pdv13: 2,
        pdv25: 1
    };

    var pdvMultiplier = 1;

    switch (pdv) {
        case pdvEnums.pdv13:
            pdvMultiplier = 1.13;
            break;

        case pdvEnums.pdv10:
            pdvMultiplier = 1.10;
            break;

        case pdvEnums.pdv5:
            pdvMultiplier = 1.05;
            break;

        case pdvEnums.pdv0:
            pdvMultiplier = 1;
            break;
        case pdvEnums.pdv25:
            pdvMultiplier = 1.25;
            break;

        default:
            pdvMultiplier = 0;
            break;
    }

    return pdvMultiplier;
}

// Check if input is float number
function isFloat(val) {
    //var floatRegex = /^-?\d+(?:[.,]\d*?)?$/;
    var floatRegex = /^(?!0*[.,]0*$|[.,]0*$|0*$)\d+[,.]?\d{0,6}$/;
    if (!floatRegex.test(val))
        return false;

    val = parseFloat(val);
    if (isNaN(val)) { return false; }
    else {
        return true;
    }

}

// returns false if OIB is invalid, true otherwise
function oibCheck(oibCode) {
    var checkDigit, num;
    var code = oibCode.toString();

    if (code.length === 13 && code.substr(0, 2).toUpperCase() === 'HR') {
        code = code.substr(2, 11);
    }
    else if (code.length !== 11) {
        return false;
    }

    var numCheck = parseInt(code, 10);
    if (isNaN(numCheck)) {
        return false;
    }

    num = 10;
    for (var i = 0; i < 10; i++) {
        num = num + parseInt(code.substr(i, 1), 10);
        num = num % 10;
        if (num === 0) {
            num = 10;
        }
        num *= 2;
        num = num % 11;
    }

    checkDigit = 11 - num;
    if (checkDigit === 10) {
        checkDigit = 0;
    }

    return parseInt(code.substr(10, 1), 10) === checkDigit;
}

// Precise round
function preciseRound(num, decimals) {
    var t = Math.pow(10, decimals);
    return (Math.round((num * t) + (decimals > 0 ? 1 : 0) * (Math.sign(num) * (10 / Math.pow(100, decimals)))) / t)
        .toFixed(decimals);
}

//jQuery plugin to prevent double submission of forms
jQuery.fn.preventDoubleSubmission = function () {
    $(this).on('submit',
        function (e) {
            var $form = $(this);
            if ($form.data('submitted') === true) {
                // Previously submitted - don't submit again
                e.preventDefault();
            } else {
                // Mark it so that the next submit can be ignored
                if ($form.valid()) {
                    $form.data('submitted', true);
                }

                //$form.data('submitted', true);
            }
        });
    // Keep chainability
    return this;
};


markInputsRequired = function () {
    $('input:not(:disabled)[type=tel]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('input:not(:disabled)[type=mail]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('input:not(:disabled)[type=email]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('input:not(:disabled)[type=text]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('input:not(:disabled)[type=file]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('input:not(:disabled)[type=select]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });


    $('textarea:not(:disabled)').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });

    $('select:not(:disabled)').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && !text.includes("*")) {
                label.append('<span style="color:#b22222"> *</span>');
            }
        }
    });
};


// Pass object, max length number and id of container to push counter into
function countChars(obj, maxLength, containerId) {
    document.getElementById(containerId).innerHTML = 'Uneseno ' + obj.value.length + ' od dopuštenih ' + maxLength + ' znakova';
}

// Scroll page to bottom with optional specified timer
function scrollToBottom(duration) {
    setTimeout(
        function () {
            $("html, body").animate({ scrollTop: $(document).height() }, 1000);
        }, duration != null ? duration : 1500);
}

// Scroll page to top with optional specified timer
function scrollToTop(duration) {
    setTimeout(
        function () {
            $("html, body").animate({ scrollTop: 0 }, 1000);
        }, duration != null ? duration : 1500);
}