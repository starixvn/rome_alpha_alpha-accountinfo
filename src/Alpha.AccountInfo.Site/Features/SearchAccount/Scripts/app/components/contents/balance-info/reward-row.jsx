import React from 'react';
import NumberFormat from 'react-number-format';
import { Tooltip } from 'react-tippy';

const RewardRow = ({
    title,
    value,
    currency,
    tooltip
}) => {
    const numberFormatProps = {
        value: value,
        displayType: 'text',
        decimalScale: 2,
        fixedDecimalScale: true,
        thousandSeparator: true
    };
    const tooltipProps = {
        title: tooltip,
        position: 'bottom',
        trigger: 'mouseenter',
        arrow: 'true'
    };

    return <div className='row-info'>
        <div className='col'>{title}
            <Tooltip className='icon-information-outline' {...tooltipProps}></Tooltip>
        </div>
        <div className='col col-right number'>
            <span className='currency'>{currency}</span>
            <NumberFormat {...numberFormatProps} />
        </div>
    </div>;
}

export default RewardRow;