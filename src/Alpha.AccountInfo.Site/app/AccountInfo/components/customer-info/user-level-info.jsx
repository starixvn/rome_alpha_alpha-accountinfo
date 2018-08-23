import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const UserLevelLevelInfo = ({
    data,
    userLevelName,
    userLevelId,
    username,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {

    const contentWithViewlogLink = <a title={resources.Viewlog} className="link-viewlog" href={data.ViewLogLink} target="main">
        <span className={data.StatusCss} data-username={username}> {username} </span>
        <span className="icon-viewlog"></span>
    </a>;

    return (
        <div className={rowClasses}>
            <div className={firstCellClass}>{userLevelName}</div>
            <div className={secondCellClass}>
                {
                    data.RoleId < userLevelId &&
                    <span className="enabled" onClick={handleSearchFromUserLevel} data-username={username}>
                        {username}
                    </span>
                }

                {
                    data.RoleId == userLevelId && data.StatusMessage == '' && contentWithViewlogLink
                }

                {
                    data.RoleId == userLevelId && data.StatusMessage != '' &&
                    <Tooltip
                        title={data.StatusMessage}
                        position="bottom"
                        trigger="mouseenter"
                        arrow="true">
                        {contentWithViewlogLink}
                    </Tooltip>
                }
            </div>
        </div>);
}

export default UserLevelLevelInfo;