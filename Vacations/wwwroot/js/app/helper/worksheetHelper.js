// worksheet item result status
var resultStatusEnums = {};

var worksheetItemStatusEnumsInfo = {};
resultStatusEnums.Zaprimljen = 1;
resultStatusEnums.UAnalizi = 2;
resultStatusEnums.SveVrijednostiUnesene = 3;
resultStatusEnums.PotvrdjenRezultat = 4;
resultStatusEnums.NepouzdanRezultat = 5;


// all color classes for removal from button
var btnColorClassesAll = [];
btnColorClassesAll.push('btn-primary', 'bg-primary-gradient', 'btn-warning', 'bg-warning-gradient', 'btn-success', 'bg-success-gradient');

// all color classes for 'no results entered'
var buttonColorClassesResultsNotEntered = [];
buttonColorClassesResultsNotEntered.push('btn-primary', 'bg-primary-gradient');

// all color classes for 'partially results entered'
var buttonColorClassesResultsPartiallyEntered = [];
buttonColorClassesResultsPartiallyEntered.push('btn-warning', 'bg-warning-gradient');

// all color classes for 'fully entered results'
var buttonColorClassesResultsFullyEntered = [];
buttonColorClassesResultsFullyEntered.push('btn-success', 'bg-success-gradient');

var updateButtonInfo = function (buttonObject, buttonLabel, buttonClass) {

    var textContainer = buttonObject.find('.js-textContainer'); //da nije u zasebnom containeru, sjebo bi sa novim textom i fa iconu
    //var text = textContainer.text();
    //text = buttonLabel + " - " + text;

    //update button text
    textContainer.text(buttonLabel);

    removeAllButtonColorClasses(buttonObject);
    addColorClassesToButton(buttonClass, buttonObject);
}

function removeAllButtonColorClasses(buttonObject) {
    $.each(btnColorClassesAll, function (index, val) {
        buttonObject.removeClass(val);
    });
}

function addColorClassesToButton(buttonClass, buttonObject) {

    buttonObject.addClass(buttonClass);
}
