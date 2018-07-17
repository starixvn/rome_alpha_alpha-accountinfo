import React from 'react';

export default class TotalDownlineList extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="col-xs-12 row-info">
                <div className="col-xs-6">{this.props.levelName} (A/C/S)</div>
                <div className="col-xs-6 col-right">
                    <div className="pull-right"
                        data-toggle="tooltip"
                        data-placement="top"
                        title={"Active:" + this.props.totalActive + " Closed:" + this.props.totalClosed + " Suspended:" + this.props.totalSuspended}>
                        <span className="number">{this.props.totalActive}</span>/<span className="status-closed">{this.props.totalClosed}</span>/<span className="status-suspended">{this.props.totalSuspended}</span>
                    </div>
                </div>
            </div>
        )
    }
}
