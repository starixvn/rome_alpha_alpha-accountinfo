import { requests } from '../../../Shared/Scripts/api-client';

export const accountinfoUrl = top.alpha.urls.accountInfoRootPath;

export const profilePictureAPI = {
    updateActorProfilePicture: (data) => requests.post(accountinfoUrl + '/ProfilePicture/UpdateActorProfilePicture', data)
};