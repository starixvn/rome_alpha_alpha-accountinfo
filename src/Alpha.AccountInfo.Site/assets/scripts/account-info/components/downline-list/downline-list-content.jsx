import React from 'react';
import TotalDownlines from './total-downlines';
import { UserLevel } from '../../constants/user-level';
import { Redirect } from 'react-router-dom';

const DownlineListContent = ({ resources, data, searchAccountRoleId, handleSearchFromUserLevel }) => {
    if (searchAccountRoleId === 1) {
        return <Redirect to='/' />;
    }

    return (
        <div>
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

            <div><hr /></div>
            <div className="col-xs-12 customer-list">
                {
                    data.DownlineList.map((downline, index) => {
                        return <div className="col-xs-6" key={index}>
                            <a href="#" data-username={downline} onClick={handleSearchFromUserLevel}>{downline}</a>
                        </div>;
                    })
                }
            </div>
        </div>
    );
};

export default DownlineListContent;