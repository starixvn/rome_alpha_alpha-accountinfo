import { UserLevel } from '../../../SearchAccount/Scripts/app/constants';

export const getUserLevelName = (level, resources) => {
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
        return (item.LevelId === level.RoleId);
    })[0].LevelName;
}

export const viewInfomation = (resources) => {
    const style = `style='padding-left: 15px;'`;
    let messages = `<div>
            <div> ${resources.ViewInformation1}</div>
            <div> ${resources.ViewInformation2}</div>
            <div ${style}> ${resources.ViewInformation3}</div>
            <div ${style}> ${resources.ViewInformation4}</div>
            <div> ${resources.ViewInformation5}</div>
            <div> ${resources.ViewInformation6}</div>
        </div>`;

    window.top.bootboxAlert(messages);
}