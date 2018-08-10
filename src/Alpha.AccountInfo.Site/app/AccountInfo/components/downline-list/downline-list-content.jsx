import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineList from './downline-list';
import TotalDownlines from './total-downlines';

export default class DownlineListContent extends React.Component{
    constructor(props) {
        super(props);
    }

    render() {
        const { resources, downlineList, fetchCustomerInfoFromServer } = this.props;

        return (
            <div>
                <DownlineList downlineList={downlineList.DownlineList} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />
                <div><hr /></div>
                {
                    this.props.downlineList.TotalStatusAgent != null &&
                    <TotalDownlines levelName="Agents"
                        resources={resources}
                        totalActive={downlineList.TotalStatusAgent.TotalActive}
                        totalClosed={downlineList.TotalStatusAgent.TotalClosed}
                        totalSuspended={downlineList.TotalStatusAgent.TotalSuspend} />
                }
                {
                    this.props.downlineList.TotalStatusMember != null &&
                    <TotalDownlines levelName="Members"
                        resources={resources}
                        totalActive={downlineList.TotalStatusMember.TotalActive}
                        totalClosed={downlineList.TotalStatusMember.TotalClosed}
                        totalSuspended={downlineList.TotalStatusMember.TotalSuspend} />
                }
            </div>    
        );
    }
}