import React from 'react';
import TotalDownlines from './total-downlines';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';
import { UserLevel } from '../constants/user-level';

const DownlineListBlock = ({ resources, data, handleSearchFromUserLevel }) => {
    const header = <Link to="/" className="feature-item">
        <span className="icon-downline" />
        {resources.DownlineList}
    </Link>;

    // TO DO: Remove when drill down to member

    if (data !== null && data.DownlineList.length > 0) {
        return (
            <div className="feature-detail customer-list">
                {header}
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
        return (
            <div>
                {header}
                <NoInfo text={resources.NoInfo} />
            </div>
        );
    }
};

export default DownlineListBlock;