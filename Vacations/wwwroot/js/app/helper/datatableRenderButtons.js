//Function for rendering datatable hyperlinks, returns html for hyperlink, accepts redirect url, id, class of button, and class of material icons
function dataRender(redirectUrl, rowId, buttonTitle, buttonClass, materialClass) {
    return "<a target='_blank' href='" + redirectUrl + '/' + rowId +
        "'class='btn btn-sm " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='fal " + materialClass + "'></i></a>";
}

//Function for rendering datatable hyperlinks, returns html for hyperlink, accepts redirect url, id, class of button, and class of material icons
function dataRenderSamePage(redirectUrl, rowId, buttonTitle, buttonClass, materialClass) {
    return "<a href='" + redirectUrl + '/' + rowId +
        "'class='btn btn-sm " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='fal " + materialClass + "'></i></a>";
}


//Function for rendering datatable hyperlinks, returns html for hyperlink, accepts redirect url, id, class of button, and class of material icons
function dataRenderSamePageWithTab(redirectUrl, id, tab, buttonTitle, buttonClass, materialClass) {
    return "<a href='" + redirectUrl + '?id=' + id + '&tab=' + tab +
        "'class='btn btn-sm " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='fal " + materialClass + "'></i></a>";
}

//Function for rendering datatable buttons, returns html for button, accepts redirect url, id, class of button, and class of material icons
function dataRenderButton(classSelector, buttonDataId, buttonTitleInfo, buttonClass, fontAwesomeIconClass) {
    return "<button class='btn btn-sm " + buttonClass +
        " waves-effect waves-themed " + classSelector + "'" +
        "title='" + buttonTitleInfo + "'" +
        "data-id= '" + buttonDataId +
        "'><i class='fal " + fontAwesomeIconClass +
        "'></i></button>";
}

//Function for rendering datatable buttons, returns html for button, accepts redirect url, id, class of button, and class of material icons
function dataRenderDownloadAttachment(data) {

    return "<a download href='" + data + "' class='btn btn-xs btn-primary waves-effect waves-themed'" +
        " title='Preuzmi'><i class='far fa-download text-white'></i></a>";

    // return "<a target='blank' href='" + url + '/' + id +
    //     "'class='btn btn-sm " + buttonClass + " waves-effect waves-themed'" + "title='" + title + "'><i class='fal " + materialClass + "'></i></a>";
}


//Function for rendering datatable text links, returns html for hiperlink, accepts redirect url, id, hiperlink name, and class of material icons
function dataRenderTextLink(url, id, title, linkName) {

    return "<a href='" + url + '/' + id + "'title='" + title + " 'class='waves-effect waves-themed m-l-20'>" + linkName + "</a>";
}

//Function for rendering datatable text links, returns html for hiperlink, accepts redirect url, id, hiperlink name, and class of material icons
function dataRenderTextLinkQuery(url, queryString, id, title, linkName) {

    return "<a href='" + url + '?' + queryString + '=' + id + "'title='" + title + " 'class='waves-effect waves-themed m-l-20'>" + linkName + "</a>";
}