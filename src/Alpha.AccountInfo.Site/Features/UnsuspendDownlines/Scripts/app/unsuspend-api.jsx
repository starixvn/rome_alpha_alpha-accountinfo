import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const unsuspendAPI = {
    updateSuspend: (action, data) => requests.post(accountinfoUrl + action, data),
    fetchCustomerList: data => requests.get(accountinfoUrl + 'UnsuspendDownlines/FetchCustomerList', data)
};