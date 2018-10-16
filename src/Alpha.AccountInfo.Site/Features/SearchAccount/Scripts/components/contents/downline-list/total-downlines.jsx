import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

export default class TotalDownlines extends React.Component {
    buildStatusInfo(classNames, value) {
        return <span className={classNames}>{value}</span>;
    }

    buildTooltip(title, classNames, value) {
        return <div className='nowrap bold'>{title}: <span className={classNames}>{value}</span></div>;
    }

    render() {
        let { resources, levelName, totalActive, totalClosed, totalSuspended } = this.props;
        let activeClass = 'status-active', closedClass = 'status-closed', suspendedClass = 'status-suspended';

        let totalDownlineTooltip = <div>
            {this.buildTooltip(resources.Active, activeClass, totalActive)}
            {this.buildTooltip(resources.ConfirmClosedViewModelText, closedClass, totalClosed)}
            {this.buildTooltip(resources.ConfirmSuspendedViewModelText, suspendedClass, totalSuspended)}
        </div>;

        let activeInfo = this.buildStatusInfo(activeClass, totalActive);
        let closedInfo = this.buildStatusInfo(closedClass, totalClosed);
        let suspendedInfo = this.buildStatusInfo(suspendedClass, totalSuspended);

        return (
            <div className="col-xs-12 row-info">
                <div className="col-xs-6 px-0">{levelName} (A/C/S)</div>
                <div className="col-xs-6 col-right px-0">
                    <div className="pull-right pointer">
                        <Tooltip
                            html={totalDownlineTooltip}
                            position="top"
                            trigger="mouseenter"
                            arrow="true"
                            theme="light"
                        >
                            {activeInfo}/{closedInfo}/{suspendedInfo}
                        </Tooltip>
                    </div>
                </div>
            </div>
        );
    }
}