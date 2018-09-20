import React from 'react';
import TotalDownlines from './total-downlines';
import { NoInformFeature } from '../components/common/no-inform-feature';
import { HeaderFeature } from '../components/common/header-feature';
import { UserLevel } from '../constants/user-level';
import { Redirect } from 'react-router-dom';

const DownlineListBlock = ({ resources, data, searchAccountRoleId, handleSearchFromUserLevel }) => {

    if (searchAccountRoleId === 1) {
        return <Redirect to='/' />;
    }

    if (data !== null && data.DownlineList.length > 0) {
        return (
            <div className="feature-detail customer-list">
                <HeaderFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} />
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
            </div>
        );
    }
    else {
        return <NoInformFeature url="/" iconClass="icon-downline" featureText={resources.DownlineList} noInform={resources.NoInfo} />
    }
};

export default DownlineListBlock;