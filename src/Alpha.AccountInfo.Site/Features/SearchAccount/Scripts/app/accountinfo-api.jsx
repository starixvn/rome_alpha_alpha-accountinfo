import { requests, handleError } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const accountInfoAPI = {
    getAutoUsernameSuggestion: data => requests.get(accountinfoUrl + 'SearchAccount/GetUsernamesSuggestion', data).catch(handleError),
    getCustomerInfo: (data, requestId) => requests.get(accountinfoUrl + 'SearchAccount/GetCustomerInfo', data, requestId).catch(handleError),
    getDownlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetDownlineList', data).catch(handleError),
    getCustomerAllStatus: data => requests.get(accountinfoUrl + 'SearchAccount/GetCustomerAllStatus', data).catch(handleError),
    getOnlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetOnlineList', data).catch(handleError),
    updateGivenCredit: data => requests.post(accountinfoUrl + 'SearchAccount/UpdateGivenCredit', data).catch(handleError),
    updateCustomerStatus: (url, data) => requests.post(url, data).catch(handleError)
};