import React from 'react';
import TotalDownlines from './total-downlines';
import { UserLevel } from '../../constants/user-level';

const DownlineListContent = ({ resources, data, mobileView, handleSearchFromUserLevel }) => <div>
    {
        data.TotalStatusAgent !== null &&
        <TotalDownlines levelName="Agents"
            resources={resources}
            totalActive={data.TotalStatusAgent.TotalActive}
            totalClosed={data.TotalStatusAgent.TotalClosed}
            totalSuspended={data.TotalStatusAgent.TotalSuspend}
        />
    }
    {
        data.TotalStatusMember !== null &&
        <TotalDownlines levelName="Members"
            resources={resources}
            totalActive={data.TotalStatusMember.TotalActive}
            totalClosed={data.TotalStatusMember.TotalClosed}
            totalSuspended={data.TotalStatusMember.TotalSuspend}
        />
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
</div>;

export default DownlineListContent;