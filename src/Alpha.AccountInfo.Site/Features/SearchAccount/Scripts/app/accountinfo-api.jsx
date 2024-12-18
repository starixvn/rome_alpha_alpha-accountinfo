import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const accountInfoAPI = {
    getAutoUsernameSuggestion: data => requests.get(top.alpha.urls.memberInfoRootPath + 'UserNameSuggestion/GetUsernamesSuggestion', data),
    getCustomerInfo: (data, requestId) => requests.get(accountinfoUrl + 'SearchAccount/GetCustomerInfo', data, requestId),
    getTotalDownlines: (data) => requests.get(accountinfoUrl + 'SearchAccount/GetTotalDownlines', data),
    getDownlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetDownlineList', data),
    getDirectMembers: data => requests.get(accountinfoUrl + 'SearchAccount/GetDirectMemers', data),
    getCustomerAllStatus: data => requests.get(top.alpha.urls.memberInfoRootPath + 'CustomerList/GetCustomerAllStatus', data),
    getOnlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetOnlineList', data),
    updateGivenCredit: data => requests.post(accountinfoUrl + 'SearchAccount/UpdateGivenCredit', data),
    updateCustomerStatus: (action, data) => requests.post(accountinfoUrl + 'SearchAccount/' + action, data),
    getLastTxn: (data) => requests.get(accountinfoUrl + 'SearchAccount/GetLastTxn', data),
};