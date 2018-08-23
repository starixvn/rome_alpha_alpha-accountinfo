import * as React from 'react';
import UserNameViewLog from './username-viewlog';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const CustomerInfoBlock = ({
    data,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {
    return (
        <div>
        {
            data.ShowMasterName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Master}</div>
                <div className={secondCellClass}>
                    {
                        data.RoleId < 3 && <span className="enabled" onClick={handleSearchFromUserLevel} data-username={data.MasterName}>
                            {data.MasterName}
                        </span>
                    }

                    {
                            data.RoleId == 3 && data.StatusMessage != '' &&
                            <Tooltip
                                title={data.StatusMessage}
                                position="bottom"
                                trigger="mouseenter"
                                arrow="true">
                                <a title={resources.Viewlog} className="link-viewlog" href={data.ViewLogLink} target="main">
                                    <span className={data.StatusCss} data-username={data.MasterName}> {data.MasterName} </span>
                                    <span className="icon-viewlog"></span>
                                </a>       
                            </Tooltip>

                    }

                    {
                        data.RoleId == 3 && data.StatusMessage == '' &&
                            <UserNameViewLog
                                viewLogTitle={resources.Viewlog}
                                viewLogLink={data.ViewLogLink}
                                statusCSS={data.StatusCss}
                                username={data.MasterName} /> 
                    }
                </div>
            </div>
        }

        {
            data.ShowAgentName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Agent}</div>
                <div className={secondCellClass}>
                    {
                        data.RoleId < 2 && <span className="enabled"
                            onClick={handleSearchFromUserLevel}
                            data-username={data.AgentName}>
                            {data.AgentName}
                        </span>
                    }

                    {
                            data.RoleId == 2 && data.StatusMessage != '' &&
                            <Tooltip
                                title={data.StatusMessage}
                                position="bottom"
                                trigger="mouseenter"
                                arrow="true">
                                <a title={resources.Viewlog} className="link-viewlog" href={data.ViewLogLink} target="main">
                                    <span className={data.StatusCss} data-username={data.AgentName}> {data.AgentName} </span>
                                    <span className="icon-viewlog"></span>
                                </a>
                            </Tooltip>
                    }

                    {
                        data.RoleId == 2 && data.StatusMessage == '' &&
                        <UserNameViewLog
                            viewLogTitle={resources.Viewlog}
                            viewLogLink={data.ViewLogLink}
                            statusCSS={data.StatusCss}
                            username={data.AgentName} />
                    }
                </div>
            </div>
        }

        {
            data.ShowMemberName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Customer}</div>
                <div className={secondCellClass}>
                    {

                            data.StatusMessage != '' &&
                            <Tooltip
                                title={data.StatusMessage}
                                position="bottom"
                                trigger="mouseenter"
                                arrow="true">
                                <a title={resources.Viewlog} className="link-viewlog" href={data.ViewLogLink} target="main">
                                    <span className={data.StatusCss} data-username={data.MemberName}> {data.MemberName} </span>
                                    <span className="icon-viewlog"></span>
                                </a>
                            </Tooltip>

                    }

                    {
                        data.StatusMessage == '' &&
                        <UserNameViewLog
                            viewLogTitle={resources.Viewlog}
                            viewLogLink={data.ViewLogLink}
                            statusCSS={data.StatusCss}
                            username={data.MemberName} />
                    }
                </div>
            </div>
        }

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Name}</div>
            <div className={secondCellClass}>{data.username}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Phone}</div>
            <div className={secondCellClass}>{data.Phone}</div>
        </div>

        <div className={rowClasses}>
            <div className={firstCellClass}>{resources.Fax}</div>
            <div className={secondCellClass}>{data.Fax}</div>
        </div>

        <div className={rowClasses + " text-center list-button"}>
            {
                data.RoleId == 1 &&
                <a className="btn-default with-level" href={data.BetListLink} target="main">{resources.BetList}</a>
            }

            {
                data.RoleId == 2 &&
                <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.MemberList}</a>
            }

            {
                data.RoleId == 3 &&
                <a className="btn-default with-level" href={data.CustomerListLink} target="main">{resources.AgentList}</a>
            }

            <a className="btn-default with-level" href={data.StatemenLink} target="main">{resources.Statement}</a>
        </div>
    </div>);
}

export default CustomerInfoBlock;