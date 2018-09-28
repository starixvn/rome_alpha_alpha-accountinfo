import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

export default class TotalDownlines extends React.Component {
    render() {
        let { resources, levelName, totalActive, totalClosed, totalSuspended } = this.props;
        let activeClass = 'status-active', closedClass = 'status-closed', suspendedClass = 'status-suspended';

        let statusInfo = (classNames, value) => <span className={classNames}>{value}</span>;

        let tooltip = (title, classNames, value) =>
            <div className='nowrap bold'>{title}: <span className={classNames}>{value}</span></div>;

        let totalDownlineTooltip = <div>
            {tooltip(resources.Active, activeClass, totalActive)}
            {tooltip(resources.ConfirmClosedViewModelText, closedClass, totalClosed)}
            {tooltip(resources.ConfirmSuspendedViewModelText, suspendedClass, totalSuspended)}
        </div>;

        let activeInfo = statusInfo(activeClass, totalActive);
        let closedInfo = statusInfo(closedClass, totalClosed);
        let suspendedInfo = statusInfo(suspendedClass, totalSuspended);

        return (
            <div className="col-xs-12 row-info">
                <div className="col-xs-6 px-0">{levelName} (A/C/S)</div>
                <div className="col-xs-6 col-right px-0">
                    <div className="pull-right">
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