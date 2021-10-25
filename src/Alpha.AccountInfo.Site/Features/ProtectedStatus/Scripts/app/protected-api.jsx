import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const protectedAPI = {
    editMultipleProtectedStatus: (data) => requests.post(accountinfoUrl + 'ProtectedStatus/EditMultiple', data)
};