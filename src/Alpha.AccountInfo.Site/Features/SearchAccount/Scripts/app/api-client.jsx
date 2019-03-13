import axios from 'axios';

const handleErrors = (err) => {
    $.unblockUI();
    console.error(err);
    throw err;
};

const responseData = (res) => typeof res.data.Result === 'undefined' ? res.data : res.data.Result;

export const requests = {
	get: (url, data) => {
		let axiosPromise = axios.get(url, { params: data });
		axiosPromise = axiosPromise.then(res => res.data.Result);
		return (axiosPromise);
	},
    post: (url, data) => axios.post(url, data, { headers: { __RequestVerificationToken: __RequestVerificationToken } })
        .then(responseData)
        .catch(handleErrors)
};

export const accountInfoAPI = {
    getAutoUsernameSuggestion: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetUsernamesSuggestion", data),
    getCustomerInfo: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetCustomerInfo", data),
    getDownlineList: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetDownlineList", data),
    getCustomerAllStatus: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetCustomerAllStatus", data),
    getOnlineList: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetOnlineList", data),
    updateGivenCredit: (data) => requests.post(accountInfoSiteAppPath + "SearchAccount/UpdateGivenCredit", data),
    updateCustomerStatus: (url, data) => requests.post(url, data)
};