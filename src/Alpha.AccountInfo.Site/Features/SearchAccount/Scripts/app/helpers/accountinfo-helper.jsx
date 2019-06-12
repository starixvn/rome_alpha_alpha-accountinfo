const htmlDecode = require('js-htmlencode').htmlDecode;

export const Scale = 0.7;
export const mobileScale = value => value / Scale;

export const decodeResources = resources => {
    for (const key in resources) {
        resources[key] = htmlDecode(resources[key]);
    }

    return resources;
};

export const buildTooltipProps = tooltip => {
    return {
        title: tooltip,
        position: 'bottom',
        trigger: 'mouseenter',
        arrow: 'true'
    };
};

export const resizePopupOnMobile = () => {
    const modalElement = window.top.document.querySelector('[id^="page-popup-"]');
    if (modalElement) {
        modalElement.classList.add('modal-account-info');
    }
};

export const resizeBootboxOnMobile = () => {
    const bootBoxElement = window.top.document.getElementsByClassName('bootbox-confirm')[0];
    if (bootBoxElement) {
        bootBoxElement.classList.add('bootbox-account-info');
    }
};

export const closeLeftPanelOnMobile = () => {
    const closeIconElement = window.parent.document.getElementById('closeIconOnMobile');
    if (closeIconElement) {
        closeIconElement.click();
    }
};

export const formatMessage = message => message.replace(/\\n/g, '<br />');

export const usernameRegExp = (message, username) => message.replace(/<username>/g, username);

const viewportMetaData = 'head meta[name=viewport]';

export const zoomDisable = () => {
    $(viewportMetaData).remove();
    $('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />');

    window.top.$(viewportMetaData).remove();
    window.top.$('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=0.7, user-scalable=no" />');
};

export const zoomEnable = () => {
    $(viewportMetaData).remove();
    $('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=0.7, user-scalable=yes" />');

    window.top.$(viewportMetaData).remove();
    window.top.$('head').prepend('<meta name="viewport" content="width=device-width, initial-scale=0.7, user-scalable=yes" />');
};
