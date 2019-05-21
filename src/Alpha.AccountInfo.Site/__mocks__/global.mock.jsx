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
    }
};

window.top.bootboxConfirm = jest.fn();