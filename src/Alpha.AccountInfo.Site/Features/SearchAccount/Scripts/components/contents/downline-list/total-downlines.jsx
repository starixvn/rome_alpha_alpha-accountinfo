import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

const StatusInfo = ({ classNames, value, slash }) => {
    return (
        <span className={classNames}>
            {value}{slash}
        </span>
    );
}

const DownlineStatusTooltip = ({ title, classNames, value }) => {
    return (
        <div className='nowrap bold'>
            {title}: <span className={classNames}>{value}</span>
        </div>
    );
}

const TotalDownlines = ({
    resources,
    levelName,
    totalActive,
    totalClosed,
    totalSuspended
}) => {
    const activeClass = 'status-active', closedClass = 'status-closed', suspendedClass = 'status-suspended';

    const statusTooltips = [
        {
            title: resources.Active,
            classNames: activeClass,
            value: totalActive
        },
        {
            title: resources.ConfirmClosedViewModelText,
            classNames: closedClass,
            value: totalClosed
        },
        {
            title: resources.ConfirmSuspendedViewModelText,
            classNames: suspendedClass,
            value: totalSuspended
        }
    ];

    let statusContents = [
        {
            classNames: activeClass,
            value: totalActive,
            slash: '/'
        },
        {
            classNames: closedClass,
            value: totalClosed,
            slash: '/'
        },
        {
            classNames: suspendedClass,
            value: totalSuspended,
            slash: ''
        }
    ];

    const tooltipProps = {
        position: "top",
        trigger: "mouseenter",
        arrow: "true",
        theme: "light",
        html: statusTooltips.map((tooltip, index) => {
            return <DownlineStatusTooltip {...tooltip} key={index} />;
        }),
        children: statusContents.map((statusContent, index) => {
            return <StatusInfo {...statusContent} key={index} />;
        })
    };

    return (
        <div className="col-xs-12 row-info">
            <div className="col-xs-6 px-0">{levelName} (A/C/S)</div>
            <div className="col-xs-6 col-right px-0">
                <div className="pull-right pointer">
                    <Tooltip {...tooltipProps}>
                        {tooltipProps.children}
                    </Tooltip>
                </div>
            </div>
        </div>
    );
}

export default TotalDownlines; 