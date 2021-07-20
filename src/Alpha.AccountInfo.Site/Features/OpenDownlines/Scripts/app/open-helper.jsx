import { UserLevel } from '../../../SearchAccount/Scripts/app/constants';
import { usernameRegExp } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

export const getUserLevelName = (levelRoleId, resources) => {
    const levelNames = [
        {
            LevelId: UserLevel.Master,
            LevelName: resources.Master
        },
        {
            LevelId: UserLevel.Agent,
            LevelName: resources.Agent
        },
        {
            LevelId: UserLevel.Member,
            LevelName: resources.Member
        }];

    return levelNames.filter(function (item) {
        return (item.LevelId === levelRoleId);
    })[0].LevelName;
};

export const viewInfomation = resources => {
    const style = `style='padding-left: 15px;'`;
    const messages = `<div>
            <div> ${resources.ViewInformation1}</div>
            <div> ${resources.ViewInformation2}</div>
            <div ${style}> ${resources.ViewInformation3}</div>
            <div ${style}> ${resources.ViewInformation4}</div>
            <div> ${resources.ViewInformation5}</div>
            <div> ${resources.ViewInformation6}</div>
        </div>`;

    window.top.bootboxAlert(messages);
};

export const getMultipleConfirmMessage = (resources, searchAccountRoleId, viewModel, originalViewModel) => {
    if (searchAccountRoleId > 1) {
        return resources.MultipleConfirm;
    }

    let openingUsernames = [],
        closingUsernames = [];

    viewModel.map(function (item, index) {
        if (item.IsClosed !== originalViewModel[index].IsClosed) {
            if (item.IsClosed) {
                closingUsernames.push(item.Username);
            } else {
                openingUsernames.push(item.Username);
            }
        }
    });

    let message = openingUsernames.length > 0
        ? usernameRegExp(resources.OpenMultipleMemberConfirmMessage, openingUsernames.join(', ')) + '<br />'
        : '';

    message += closingUsernames.length > 0
        ? usernameRegExp(resources.CloseMultipleMemberConfirmMessage, closingUsernames.join(', ')) + '<br />'
        : '';

    message += resources.SubmitMultipleConfirm3;

    return message;
};