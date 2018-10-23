import * as React from 'react';
import NumberFormat from 'react-number-format';
import CustomAnchorTag from './custom-anchor-tag';

const CellContainNumberFormat = ({
    classNames = null,
    value = 0,
    mobileView = false,
    hrefLink = null
}) => {
    let formatClassNames = 'number';
    let renderLink = false;

    if (value < 0) {
        formatClassNames = 'number-red';
    } else if (value > 0 && hrefLink !== null && hrefLink !== '') {
        renderLink = true;
        formatClassNames = 'number-link';
    }

    const numberFormatProps = {
        value: value,
        displayType: 'text',
        decimalScale: 2,
        fixedDecimalScale: true,
        thousandSeparator: true,
        renderText: (value) => {
            return renderLink ?
                <CustomAnchorTag href={hrefLink} mobileView={mobileView}>
                    {value}
                </CustomAnchorTag> :
                <span>{value}</span>;
        }
    }

    return (
        <div className={`${classNames} ${formatClassNames}`}>
            <NumberFormat {...numberFormatProps} />
        </div>
    );
}

export default CellContainNumberFormat;