import * as React from 'react';
import BlockHeading from '../common/block-heading';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';

interface IDownlineListTabProps {
}

interface IDownlineListTabState {
}

export default class DownlineListBlock extends React.Component<IDownlineListTabProps, IDownlineListTabState> {
    constructor(props) {
        super(props);
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
                        <DownlineInfo />
                        <div className="col-xs-12 row-info"><hr /></div>
                        <TotalDownlineList levelName="Agents" totalActive={90} totalClosed={47} totalSuspended={2} />
                        <TotalDownlineList levelName="Members" totalActive={80} totalClosed={584} totalSuspended={3} />
                    </div>
                </div>
            </div>
        );
    }
}