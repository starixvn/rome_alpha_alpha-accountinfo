import React from 'react';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class DownlineListContent extends React.Component{
    constructor(props) {
        super(props);

    }

    render() {
        const { downlineList } = this.props;

        return (
            <div>
                <DownlineInfo downlineList={downlineList.DownlineList} />
                <div><hr /></div>
                <TotalDownlineList levelName="Agents"
                    totalActive={downlineList.TotalStatusAgent.TotalActive}
                    totalClosed={downlineList.TotalStatusAgent.TotalClosed}
                    totalSuspended={downlineList.TotalStatusAgent.TotalSuspend} />
                <TotalDownlineList levelName="Members"
                    totalActive={downlineList.TotalStatusMember.TotalActive}
                    totalClosed={downlineList.TotalStatusMember.TotalClosed}
                    totalSuspended={downlineList.TotalStatusMember.TotalSuspend} />
            </div>    
        );
    }
}