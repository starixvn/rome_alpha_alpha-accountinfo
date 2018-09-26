import * as React from 'react';
import { NoInformFeature } from './no-inform-feature';
import { HeaderFeature } from './header-feature';
import ContactInfoContent from '../contents/contact-info-content';

const ContactInfoPage = ({ customerInfo, resources, redirectToBalanceInfoPage }) =>
    customerInfo === null ?
        <NoInformFeature url="/featureList" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} noInform={resources.NoInfo} /> :
        <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} redirectToBalanceInfoPage={redirectToBalanceInfoPage} />
            <ContactInfoContent resources={resources} customerInfo={customerInfo} />
        </div>;

export default ContactInfoPage;
