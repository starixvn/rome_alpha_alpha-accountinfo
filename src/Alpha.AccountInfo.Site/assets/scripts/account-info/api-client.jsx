import axios from 'axios';

const handleErrors = (err) => {
    $.unblockUI();
    console.error(err);
    throw err;
};

const responseData = (res) => res.data.Result;

const requests = {
    get: (url, data) => axios.get(url, { params: data })
        .then(responseData)
        .catch(handleErrors),
    post: (url, data) => axios.post(url, data, { headers: { __RequestVerificationToken: __RequestVerificationToken } })
        .then(responseData)
        .catch(handleErrors)
};

export const accountInfoAPI = {
    getAutoUsernameSuggestion: (data) => requests.get(accountInfoSiteAppPath + "AccountInfo/GetUsernamesSuggestion", data),
    getCustomerInfo: (data) => requests.get(accountInfoSiteAppPath + "AccountInfo/GetCustomerInfo", data),
    getDownlineList: (data) => requests.get(accountInfoSiteAppPath + "AccountInfo/GetDownlineList", data),
    getCustomerAllStatus: (data) => requests.get(accountInfoSiteAppPath + "AccountInfo/GetCustomerAllStatus", data),
    getOnlineList: (data) => requests.get(accountInfoSiteAppPath + "AccountInfo/GetOnlineList", data),
    updateGivenCredit: (data) => requests.post(accountInfoSiteAppPath + "AccountInfo/UpdateGivenCredit", data),
    updateCustomerStatus: (url, data) => requests.post(url, data)
};