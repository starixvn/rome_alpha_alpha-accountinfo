import React from 'react';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    componentDidMount() {
    
    }

    render() {
        return (
            <CustomizedAccordionItem title="Downline list" iconClassname="icon-downline">
                <DownlineInfo downlineList={this.props.downlineList.DownlineList} />
                <div className="col-xs-12 row-info"><hr /></div>
                <TotalDownlineList levelName="Agents"
                    totalActive={this.props.downlineList.TotalStatusAgent.TotalActive}
                    totalClosed={this.props.downlineList.TotalStatusAgent.TotalClosed}
                    totalSuspended={this.props.downlineList.TotalStatusAgent.TotalSuspend} />
                <TotalDownlineList levelName="Members"
                    totalActive={this.props.downlineList.TotalStatusMember.TotalActive}
                    totalClosed={this.props.downlineList.TotalStatusMember.TotalClosed}
                    totalSuspended={this.props.downlineList.TotalStatusMember.TotalSuspend} />
            </CustomizedAccordionItem>    
        );
    }
}