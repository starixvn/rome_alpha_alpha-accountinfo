import * as React from 'react';

const CustomerInfoBlock = ({
    data,
    resources,
    handleSearchFromUserLevel,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right"}) =>
    <div >
        {
            data.ShowMasterName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Master}</div>
                <div className={secondCellClass}>
                    <span className={data.RoleId == 3 && `${data.Suspended && "suspended"} ${data.Closed && "closed"}`} 
                        onClick={handleSearchFromUserLevel}
                        data-username={data.MasterName}>
                        {data.MasterName}
                    </span>
                    {data.RoleId == 3 && <span className="icon-viewlog" title={resources.Viewlog}></span>}
                </div>
            </div>
        }

        {
            data.ShowAgentName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Agent}</div>
                <div className={secondCellClass}>
                    <span className={data.RoleId == 2 && `${data.Suspended && "suspended"} ${data.Closed && "closed"}`} 
                        onClick={handleSearchFromUserLevel}
                        data-username={data.AgentName}>
                        {data.AgentName}
                    </span>
                    {data.RoleId == 2 && <span className="icon-viewlog" title={resources.Viewlog}></span>}
                </div>
            </div>
        }

        {
            data.ShowMemberName &&
            <div className={rowClasses}>
                <div className={firstCellClass}>{resources.Customer}</div>
                <div className={secondCellClass}>
                    <span className={`${data.Suspended && "suspended"} ${data.Closed && "closed"}`} 
                        onClick={handleSearchFromUserLevel}
                        data-username={data.MemberName}>
                        {data.MemberName}
                    </span>
                    <span className="icon-viewlog" title={resources.Viewlog}></span>
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
    </div>

export default CustomerInfoBlock;