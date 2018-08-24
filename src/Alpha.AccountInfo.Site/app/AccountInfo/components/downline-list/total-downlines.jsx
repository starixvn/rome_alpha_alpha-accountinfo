import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const TotalDownlines = ({ resources, levelName, totalActive, totalClosed, totalSuspended }) => {
    const totalDownline = <div>
        <div className='nowrap bold'>{resources.Active}: <span className='status-active'>{totalActive}</span></div>
        <div className='nowrap bold'>{resources.Closed}: <span className='status-closed'>{totalClosed}</span></div>
        <div className='nowrap bold'>{resources.Suspended}: <span className='status-suspended'>{totalSuspended}</span></div>
    </div>;

    return (
        <div className="col-xs-12 row-info">
            <div className="col-xs-6 px-0">{levelName} (A/C/S)</div>
            <div className="col-xs-6 col-right px-0">
                <div className="pull-right">

                    <Tooltip
                        html={totalDownline}
                        position="bottom"
                        trigger="mouseenter"
                        arrow="true">
                        <span className="status-active">{totalActive}</span>
                        /<span className="status-closed">{totalClosed}</span>
                        /<span className="status-suspended">{totalSuspended}</span>
                    </Tooltip>
                </div>
            </div>
        </div>
        );
}

export default TotalDownlines;
