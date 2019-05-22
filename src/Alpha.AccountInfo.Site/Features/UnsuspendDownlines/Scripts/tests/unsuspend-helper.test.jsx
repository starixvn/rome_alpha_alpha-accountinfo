import '../../../../__mocks__/global.mock';
import  {resources} from './unspend-downlines-resources.mock';
import * as unsuspendHelper from 'UnsuspendHelper';

test.each([[3, 'Master'], [2, 'Agent'], [1, 'Member']])(
    "#getUserLevelName Should return correct Level Name",
    (levelRoleId, expectedLevelName) => {
        const actualLevelName = unsuspendHelper.getUserLevelName(levelRoleId, resources);
        expect(actualLevelName).toBe(expectedLevelName);
    }
);

test('#window.top.bootboxAlert called when call viewInfomation', () => {
    unsuspendHelper.viewInfomation(resources);
    expect(window.top.bootboxAlert).toHaveBeenCalled();
});