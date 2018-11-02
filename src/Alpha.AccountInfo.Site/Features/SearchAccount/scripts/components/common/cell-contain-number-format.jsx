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
    let numberFormatText = <span>{value}</span>;

    if (value > 0 && hrefLink !== null && hrefLink !== '') {
        let customAnchorTag = {
            href: hrefLink,
            mobileView: mobileView,
            children: value
        };

        formatClassNames = 'number-link';
        numberFormatText = <CustomAnchorTag {...customAnchorTag} />;
    }
    else if (value < 0) {
        formatClassNames = 'number-red';
    }

    let numberFormatProps = {
        value: value,
        displayType: 'text',
        decimalScale: 2,
        fixedDecimalScale: true,
        thousandSeparator: true,
        renderText: () => { return numberFormatText; }
    }

    return (
        <div className={`${classNames} ${formatClassNames}`}>
            <NumberFormat {...numberFormatProps} />
        </div>
    );
}

export default CellContainNumberFormat;