function openCloseDropdown(elementId, isOpen) {

    var element = document.getElementById(elementId);

    if (isOpen) {
        element.classList.remove("disabled");
    }
    else {
        element.classList.add("disabled");
    }

}