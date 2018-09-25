import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const TotalDownlines = ({ resources, levelName, totalActive, totalClosed, totalSuspended }) => {
    const totalDownlineTooltip = <div>
        <div className='nowrap bold'>{resources.Active}: <span className='status-active'>{totalActive}</span></div>
        <div className='nowrap bold'>{resources.ConfirmClosedViewModelText}: <span className='status-closed'>{totalClosed}</span></div>
        <div className='nowrap bold'>{resources.ConfirmSuspendedViewModelText}: <span className='status-suspended'>{totalSuspended}</span></div>
    </div>;

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
                        <span className="status-active">{totalActive}</span>
                        /<span className="status-closed">{totalClosed}</span>
                        /<span className="status-suspended">{totalSuspended}</span>
                    </Tooltip>
                </div>
            </div>
        </div>
    );
};

export default TotalDownlines;