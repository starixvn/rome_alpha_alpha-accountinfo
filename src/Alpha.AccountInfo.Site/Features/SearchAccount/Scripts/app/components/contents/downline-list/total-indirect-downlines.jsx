import React from 'react';
import 'react-tippy/dist/tippy.css';
import { Tooltip } from 'react-tippy';

export const StatusInfo = ({ classNames, value, slash }) => {
    return (
        <span className={classNames}>
            {value}{slash}
        </span>
    );
}

export const DownlineStatusTooltip = ({ title, classNames, value }) => {
    return (
        <div className='nowrap bold'>
            {title}: <span className={classNames}>{value}</span>
        </div>
    );
}

const TotalIndirectDownlines = ({ resources, totalIndirectDownline }) => {
    const activeClass = 'status-active', closedClass = 'status-closed', suspendedClass = 'status-suspended';
    const { TotalActive, TotalClosed, TotalSuspended, LevelName } = totalIndirectDownline;

    const statusTooltips = [
        {
            title: resources.Active,
            classNames: activeClass,
            value: TotalActive
        },
        {
            title: resources.ConfirmClosedViewModelText,
            classNames: closedClass,
            value: TotalClosed
        },
        {
            title: resources.ConfirmSuspendedViewModelText,
            classNames: suspendedClass,
            value: TotalSuspended
        }
    ];

    let statusContents = [
        {
            classNames: activeClass,
            value: TotalActive,
            slash: '/'
        },
        {
            classNames: closedClass,
            value: TotalClosed,
            slash: '/'
        },
        {
            classNames: suspendedClass,
            value: TotalSuspended,
            slash: ''
        }
    ];

    let tooltipHtml =
        <div>
            {
                statusTooltips.map(
                    (tooltip, index) => {
                        return <DownlineStatusTooltip {...tooltip} key={index} />;
                    })
            }
        </div>;

    const tooltipProps = {
        position: "top",
        trigger: "mouseenter",
        arrow: "true",
        theme: "light",
        html: tooltipHtml,
        children: statusContents.map((statusContent, index) => {
            return <StatusInfo {...statusContent} key={index} />;
        })
    };

    return (
        <div className="col-xs-12 row-info">
            <div className="col-xs-6 px-0">{LevelName} (A/C/S)</div>
            <div className="col-xs-6 col-right px-0">
                <div className="pull-right pointer">
                    <Tooltip {...tooltipProps}>
                        {tooltipProps.children}
                    </Tooltip>
                </div>
            </div>
            <div className="col-xs-12"><hr /> </div>
        </div>
    );
}

export default TotalIndirectDownlines; 