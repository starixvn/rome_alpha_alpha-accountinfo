import * as React from 'react';
import NumberFormat from 'react-number-format';

const CellContainNumberFormat = ({ classNames = null, title = '', hrefLink = null, target = null }) => {
    let formatClassNames = null;
    if (title < 0) {
        formatClassNames = `${classNames} number-red`;
    } else if (title >= 0 && hrefLink != null) {
        formatClassNames = `${classNames} number-link`;
    } else {
        formatClassNames = `${classNames} number`;
    }

    return (
        <div className={formatClassNames}>
            <NumberFormat                
                value={title}
                displayType={'text'}
                decimalScale="2"
                fixedDecimalScale={true}
                thousandSeparator={true}
                renderText={value => hrefLink == null ?
                    (<span>{value}</span>) :
                    (<a href={hrefLink} target={target}>{value}</a>)} />
        </div>
    );
}    

export default CellContainNumberFormat;