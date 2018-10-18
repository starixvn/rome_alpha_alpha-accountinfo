export const mobileScale = (value) => value / 0.7;

export const resizePopupOnMobile = () => window.top.document.querySelector('[id^="page-popup-"]').classList.add("modal-account-info");

export const resizeBootboxOnMobile = () => window.top.document.getElementsByClassName('bootbox-confirm')[0].classList.add("bootbox-account-info");