import $ from 'jquery';

global.$ = global.jQuery = $;
global.$.fn.block = jest.fn();
global.$.unblock = jest.fn();
global.$.blockUI = jest.fn();
global.$.unblockUI = jest.fn();
global.__RequestVerificationToken = '';

window.top.alpha = {
    urls: {
        accountInfoRootPath: '/site-accountinfo/'
    },
    core:{
        isIe: jest.fn()
    }
};

window.top.main = {
    DelayReloadPage: jest.fn()
};

window.top.ageWnd = {
    open: jest.fn(),
    close: jest.fn(),
    createInstance: jest.fn() 
};

window.top.updateGivenCreditPopup = {
    onClosing: jest.fn()
};

window.top.confirmPopupPopup = {
    onClosing: jest.fn()
};

window.top.updateProductStatusPopup = {
    onClosing: jest.fn()
};

window.top.popupManager = {
    openWithHanlder: jest.fn()
};

window.top.bootboxConfirm = jest.fn();
window.top.bootboxAlert = jest.fn();
