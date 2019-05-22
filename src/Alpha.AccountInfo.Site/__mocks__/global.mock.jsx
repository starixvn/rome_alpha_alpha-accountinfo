import $ from 'jquery';

global.$ = global.jQuery = $;
global.$.fn.block = jest.fn();
global.$.unblock = jest.fn();
global.$.blockUI = jest.fn();
global.$.unblockUI = jest.fn();
global.__RequestVerificationToken = '';

// window.top = {
//     alpha: {
//         urls: {
//             accountInfoRootPath: '/site-accountinfo/'
//         }
//     },
//     main: {
//         DelayReloadPage: jest.fn()
//     },
//     ageWnd: {
//         close: jest.fn()
//     },
//     bootboxConfirm: jest.fn()
// };

window.top.alpha = {
    urls: {
        accountInfoRootPath: '/site-accountinfo/'
    }
};

window.top.main = {
    DelayReloadPage: jest.fn()
};

window.top.ageWnd = {
    close: jest.fn()
};

window.top.bootboxConfirm = jest.fn();
window.top.bootboxAlert = jest.fn();