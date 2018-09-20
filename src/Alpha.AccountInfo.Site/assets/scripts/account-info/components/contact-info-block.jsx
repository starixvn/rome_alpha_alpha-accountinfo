import * as React from 'react';
import { NoInformFeature } from '../components/common/no-inform-feature';
import { HeaderFeature } from '../components/common/header-feature';

const ContactInfoBlock = ({
    customerInfo,
    resources,
    rowClasses = "col-xs-12 row-info",
    firstCellClass = "col-xs-5",
    secondCellClass = "col-xs-7 col-right" }) => {

    if (customerInfo !== null) {
        return (
            <div className="feature-detail">
                <HeaderFeature url="/" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} />
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
        return <NoInformFeature url="/" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} noInform={resources.NoInfo} />
    }
};

export default ContactInfoBlock;