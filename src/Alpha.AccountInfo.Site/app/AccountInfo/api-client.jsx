import axios from 'axios';

const handleErrors = (err) => {
    console.error(err);
    throw err;
};

const responseData = (res) => res.data.Result;

const requests = {
    get: (url) => axios.get(url)
        .then(responseData)
        .catch(handleErrors),
    post: (url, data) => axios.post(url, data)
        .then(responseData)
        .catch(handleErrors),
};

export const accountInfoAPI = {
    getAutoUsernameSuggestion: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/GetUsernamesSuggestion", data),
    getCustomerInfo: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/GetCustomerInfo", data),
    getDownlineList: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/GetDownlineList", data),
    getCustomerAllStatus: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/GetCustomerAllStatus", data),
    getOnlineList: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/GetOnlineList", data),
    updateGivenCredit: (data) => requests.post(window.accountInfoSiteAppPath + "AccountInfo/UpdateGivenCredit", data)
}