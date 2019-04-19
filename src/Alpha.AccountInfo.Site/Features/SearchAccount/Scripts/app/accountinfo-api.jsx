import { requests } from '../../../Shared/Scripts/api-client';

export const accountInfoAPI = {
	getAutoUsernameSuggestion: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetUsernamesSuggestion", data),
	getCustomerInfo: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetCustomerInfo", data),
	getDownlineList: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetDownlineList", data),
	getCustomerAllStatus: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetCustomerAllStatus", data),
	getOnlineList: (data) => requests.get(accountInfoSiteAppPath + "SearchAccount/GetOnlineList", data),
	updateGivenCredit: (data) => requests.post(accountInfoSiteAppPath + "SearchAccount/UpdateGivenCredit", data),
	updateCustomerStatus: (url, data) => requests.post(url, data)
};