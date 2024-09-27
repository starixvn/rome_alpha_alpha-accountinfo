export const croppedImageId = -1, defaultImageId = -100;
const accountinfoControllerUrl = `${window.top.alpha.urls.accountInfoRootPath}/ProfilePicture`;

export const urls = {
    UpdateActorProfilePicture: `${accountinfoControllerUrl}/UpdateActorProfilePicture`,
    GetPictureLibrary: `${accountinfoControllerUrl}/GetPictureLibrary`
}

export const changeDisplayLanguage = (languageCode) => window.top.headerNav.changeLanguage(languageCode, true);

export const viewDataURL = (event) => window.top.alpha.viewDataURL(event);