import * as React from 'react';
import NumberFormat from 'react-number-format';


const CellContainNumberFormat = ({ classNames, title }) => {
    let formatClassNames = title < 0 ? `${classNames} number-red` : `${classNames} number`;
    return (
        <div className={formatClassNames}>
            <NumberFormat
                value={title}
                displayType={'text'}
                decimalScale="2"
                thousandSeparator={true}
                renderText={value => <span>{value}</span>} />
        </div>
    );
}    

export default CellContainNumberFormat;