export const Scale = 0.7
export const mobileScale = (value) => value / Scale;

export const resizePopupOnMobile = () => {
    let modalElement = window.top.document.querySelector('[id^="page-popup-"]');
    if (modalElement) {
        modalElement.classList.add("modal-account-info");
    }
}

export const resizeBootboxOnMobile = () => {
    let bootBoxElement = window.top.document.getElementsByClassName('bootbox-confirm')[0];
    if (bootBoxElement) {
        bootBoxElement.classList.add("bootbox-account-info");
    }
}

export const closeLeftPanelOnMobile = () => {
    let closeIconElement = window.parent.document.getElementById('closeIconOnMobile');
    if (closeIconElement) {
        closeIconElement.click();
    }
}