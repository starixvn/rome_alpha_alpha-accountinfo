import * as React from 'react';
import { Link } from 'react-router-dom';
import { NoInfo } from '../components/common/no-info';

const ContactInfoBlock = ({
    customerInfo,
    resources,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {
    const header = <Link to="/" className="feature-item">
        <span className="icon-contact-info" />
        {resources.ShowContactInfo}
    </Link>;

    if (customerInfo !== null) {
        return (
            <div>
                {header}
                <div className={rowClasses}>
                    <div className={firstCellClass}>{resources.Name}</div>
                    <div className={secondCellClass}>{customerInfo.FullName}</div>
                </div>

                <div className={rowClasses}>
                    <div className={firstCellClass}>{resources.Phone}</div>
                    <div className={secondCellClass}>{customerInfo.Phone}</div>
                </div>

                <div className={rowClasses}>
                    <div className={firstCellClass}>{resources.Fax}</div>
                    <div className={secondCellClass}>{customerInfo.Fax}</div>
                </div>
            </div>);
    }
    else {
        return (
            <div>
                {header}
                <NoInfo text={resources.NoInfo} />
            </div>);

    }
};

export default ContactInfoBlock;