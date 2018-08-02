import React from 'react';

export default class TotalDownlines extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const {
            levelName,
            totalActive,
            totalClosed,
            totalSuspended
        } = this.props;

        return (
            <div className="col-xs-12 row-info px-0">
                <div className="col-xs-6">{levelName} (A/C/S)</div>
                <div className="col-xs-6 col-right">
                    <div className="pull-right"
                        data-toggle="tooltip"
                        data-placement="top"
                        title={"Active:" + totalActive + " Closed:" + totalClosed + " Suspended:" + totalSuspended}>
                        <span className="number">{totalActive}</span>
                        /<span className="status-closed">{totalClosed}</span>
                        /<span className="status-suspended">{totalSuspended}</span>
                    </div>
                </div>
            </div>
        )
    }
}
