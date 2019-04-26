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