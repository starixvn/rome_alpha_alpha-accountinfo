const base64js = require('base64-js');

export const base64Helper = {
	encode: (str, encoding = 'utf-8') => {
		let bytes = new (TextEncoder || TextEncoderLite)(encoding).encode(str);
		return base64js.fromByteArray(bytes);
	},
	decode: (str, encoding = 'utf-8') => {
		let bytes = base64js.toByteArray(str);
		return new (TextDecoder || TextDecoderLite)(encoding).decode(bytes);
	},
	decodeResources: (resources) => {
		for (let key in resources) {
			resources[key] = base64Helper.decode(resources[key]);
		}

		return resources;
	}
};

export const Scale = 0.7
export const mobileScale = (value) => value / Scale;

export const buildTooltipProps = (tooltip) => {
	return {
		title: tooltip,
		position: "bottom",
		trigger: "mouseenter",
		arrow: "true"
	}
}

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