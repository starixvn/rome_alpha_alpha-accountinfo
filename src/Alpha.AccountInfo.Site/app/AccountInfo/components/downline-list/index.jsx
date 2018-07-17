import React from 'react';
import BlockHeading from '../common/block-heading';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    componentDidMount() {
    
    }

    render() {
        return (
            <div className="panel panel-default">
                <BlockHeading text="Downline list"
                    id="headingDownlineList"
                    iconClassname="icon-downline"
                    tabHref="collapseDownlineList" />

                <div id="collapseDownlineList"
                    className="panel-collapse collapse"
                    role="tabpanel"
                    aria-labelledby="headingDownlineList">
                    <div className="panel-body panel-downline-list">
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
                    </div>
                </div>
            </div>
        );
    }
}