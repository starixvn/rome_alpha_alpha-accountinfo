import React from 'react';
import NumberFormat from 'react-number-format';
import CustomAnchorTag from './custom-anchor-tag';

const CellContainNumberFormat = ({
    classNames = null,
    value = 0,
    mobileView = false,
    hrefLink = null
}) => {
    let formatClassNames = 'number';

    if (value < 0) {
        formatClassNames = 'number-red';
    }
    else if (value > 0 && hrefLink !== null && hrefLink !== '') {
        formatClassNames = 'number-link';
    }

    return <div className={`${classNames} ${formatClassNames}`}>
        <NumberFormat
            value={value}
            displayType={'text'}
            decimalScale={2}
            fixedDecimalScale={true}
            thousandSeparator={true}
            renderText={
                displayedValue => hrefLink === null || hrefLink === '' ?
                    <span>{displayedValue}</span> :
                    <CustomAnchorTag {...{ mobileView, href: hrefLink }}>{displayedValue}</CustomAnchorTag>
            }
        />
    </div>;
};

export default CellContainNumberFormat;