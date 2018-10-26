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
        <div className="customer-list">
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