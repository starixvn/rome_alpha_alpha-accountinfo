import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const openAPI = {
    updateClose: (action, data) => requests.post(accountinfoUrl + action, data),
    fetchCustomerList: data => requests.get(accountinfoUrl + 'OpenDownlines/FetchCustomerList', data)
};