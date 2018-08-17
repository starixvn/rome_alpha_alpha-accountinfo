import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineList from './downline-list';
import TotalDownlines from './total-downlines';

const DownlineListContent = ({ resources, downlineList, fetchCustomerInfoFromServer }) =>
    <div>
        <DownlineList
            resources={resources}
            downlineList={downlineList.DownlineList}
            fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
        <div><hr /></div>
        {
            downlineList.TotalStatusAgent != null &&
            <TotalDownlines levelName="Agents"
                resources={resources}
                totalActive={downlineList.TotalStatusAgent.TotalActive}
                totalClosed={downlineList.TotalStatusAgent.TotalClosed}
                totalSuspended={downlineList.TotalStatusAgent.TotalSuspend} />
        }
        {
           downlineList.TotalStatusMember != null &&
            <TotalDownlines levelName="Members"
                resources={resources}
                totalActive={downlineList.TotalStatusMember.TotalActive}
                totalClosed={downlineList.TotalStatusMember.TotalClosed}
                totalSuspended={downlineList.TotalStatusMember.TotalSuspend} />
        }
    </div>    

export default DownlineListContent;