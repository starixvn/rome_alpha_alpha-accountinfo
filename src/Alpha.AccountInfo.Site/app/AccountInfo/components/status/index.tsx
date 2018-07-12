import * as React from 'react';
import BlockHeading from '../common/block-heading';
import Status from './status';
import CellContainLink from '../common/cell-contain-link';

interface IStatusTabProps {
}

interface IStatusTabState {
}

export default class StatusBlock extends React.Component<IStatusTabProps, IStatusTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <BlockHeading text="Status"
                    id="headingStatus"
                    iconClassname="icon-status"
                    tabHref="collapseStatus" />

                <div id="collapseStatus"
                    className="panel-collapse collapse"
                    role="tabpanel"
                    aria-labelledby="headingStatus">
                    <div className="panel-body">
                        <div className="col-xs-6 no-p-x">
                            <Status text="Suspended" checked={true}/>
                            <Status text="Auto PT" checked={true}/>
                            <Status text="Outright" checked={true} />
                            <Status text="Outright" checked={true} />
                            <Status text="Double Comm"/>

                            <CellContainLink text="Enable Keno01"
                                classNames="row-info"
                                iconClassname="icon-ic_list"
                                hrefLink="#" />

                            <Status text="Colossus Bets" checked={true} />
                        </div>
                        <div className="col-xs-6 no-p-x">
                            <Status text="Closed" />
                            <Status text="Virtual Sports" />
                            <Status text="HR Fixed Odds" />
                            <Status text="Allbet" />
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}