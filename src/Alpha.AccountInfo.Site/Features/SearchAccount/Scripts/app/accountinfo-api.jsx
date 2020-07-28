import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const accountInfoAPI = {
	getAutoUsernameSuggestion: data => requests.get(top.alpha.urls.memberInfoRootPath + 'UserNameSuggestion/GetUsernamesSuggestion', data),
    getCustomerInfo: (data, requestId) => requests.get(accountinfoUrl + 'SearchAccount/GetCustomerInfo', data, requestId),
    getDownlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetDownlineList', data),
	getCustomerAllStatus: data => requests.get(accountinfoUrl + 'EditProduct/GetCustomerAllStatus', data),
    getOnlineList: data => requests.get(accountinfoUrl + 'SearchAccount/GetOnlineList', data),
    updateGivenCredit: data => requests.post(accountinfoUrl + 'SearchAccount/UpdateGivenCredit', data),
    updateCustomerStatus: (url, data) => requests.post(url, data)
};