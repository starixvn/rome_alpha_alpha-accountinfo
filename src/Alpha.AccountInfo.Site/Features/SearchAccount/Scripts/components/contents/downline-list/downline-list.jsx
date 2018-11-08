import React from 'react';
import TotalDownlines from './total-downlines';
import { UserLevel } from '../../../constants';

const DownlineList = ({
    resources,
    data,
    mobileView,
    searchAccountRoleId,
    handleSearchFromUserLevel }) => {
    const totalDownlineByLevel = [
        {
            levelName: resources.Agents,
            data: data.TotalStatusAgent,
            visible: searchAccountRoleId === UserLevel.Master
        },
        {
            levelName: resources.Members,
            data: data.TotalStatusMember,
            visible: true
        }
    ];

    return (
        <div>
            {
                totalDownlineByLevel.map((totalDownline, index) => {
                    let totalDownlinesProps = {
                        levelName: totalDownline.levelName,
                        resources: resources,
                        totalActive: totalDownline.data.TotalActive,
                        totalClosed: totalDownline.data.TotalClosed,
                        totalSuspended: totalDownline.data.TotalSuspend,
                        key: index
                    }

                    return totalDownline.visible && < TotalDownlines {...totalDownlinesProps} />;
                })
            }

            <div className="col-xs-12 row-info"><hr /></div>

            <div className="totalstatus-list">
                <div className="radio">
                    <input type="radio" id="active" value="active" name="totalStatus" defaultChecked="true" />
                    <label className="label-text" htmlFor="active">A <span className="status-active">(3)</span></label>
                </div>
                <div className="radio">
                    <input type="radio" id="closed" value="closed" name="totalStatus" />
                    <label className="label-text" htmlFor="closed">C <span className="status-closed">(152)</span></label>
                </div>
                <div className="radio">
                    <input type="radio" id="suspended" value="suspended" name="totalStatus" />
                    <label className="label-text" htmlFor="suspended">S <span className="status-suspended">(0)</span></label>
                </div>   
            </div>
                       

            <div className={`${mobileView ? "" : "customer-list"} col-xs-12`}>
                {
                    data.DownlineList.map((downline, index) => {
                        return <div className="col-xs-6" key={index}>
                            <a href="#" data-username={downline.Username} onClick={handleSearchFromUserLevel}>{downline.Username}</a>
                        </div>;
                    })
                }
            </div>
        </div>
    );
}

export default DownlineList;