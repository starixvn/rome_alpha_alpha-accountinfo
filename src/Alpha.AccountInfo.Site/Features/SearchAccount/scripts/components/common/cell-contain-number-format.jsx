import * as React from 'react';
import NumberFormat from 'react-number-format';
import CustomAnchorTag from './custom-anchor-tag';

const CellContainNumberFormat = ({
    classNames = null,
    value = 0,
    mobileView = false,
    hrefLink = null
}) => {
    let formatClassNames = '';

    if (value > 0 && hrefLink !== null && hrefLink !== '') {
        formatClassNames = 'number-link';
    }
    else if (value < 0) {
        formatClassNames = 'number-red';
    }

    return (
        <div className={`${classNames} ${formatClassNames}`}>
            <NumberFormat
                value={value}
                displayType={'text'}
                decimalScale={2}
                fixedDecimalScale={true}
                thousandSeparator={true}
                renderText={
                    value => hrefLink === null || hrefLink === '' ?
                    <span>{value}</span> :
                    <CustomAnchorTag
                        href={hrefLink}
                        mobileView={mobileView}
                    >
                        {value}
                        </CustomAnchorTag>
                }
            />
        </div>
    );
}

export default CellContainNumberFormat;