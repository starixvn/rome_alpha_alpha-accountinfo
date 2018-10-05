import * as React from 'react';
import NumberFormat from 'react-number-format';
import CustomAnchorTag from './custom-anchor-tag';

const CellContainNumberFormat = ({ mobileView = false, classNames = null, value = 0, hrefLink = null, target = null }) => {
    let formatClassNames = null;
    if (value < 0) {
        formatClassNames = `${classNames} number-red`;
    } else if (value >= 0 && hrefLink !== null && hrefLink !== '') {
        formatClassNames = `${classNames} number-link`;
    } else {
        formatClassNames = `${classNames} number`;
    }

    return (
        <div className={formatClassNames}>
            <NumberFormat
                value={value}
                displayType={'text'}
                decimalScale={2}
                fixedDecimalScale={true}
                thousandSeparator={true}
                renderText={value => hrefLink === null || hrefLink === '' ?
                    <span>{value}</span> :
                    <CustomAnchorTag href={hrefLink} mobileView={mobileView} >{value} </CustomAnchorTag>}
            />
        </div>
    );
};   

export default CellContainNumberFormat;