import * as React from 'react';
import { NoInformFeature } from '../../components/common/no-inform-feature';
import { HeaderFeature } from '../../components/common/header-feature';
import ContactInfoContent from '../contents/contact-info-content';

const ContactInfoPage = ({ customerInfo, resources }) => customerInfo === null ?
    <NoInformFeature url="/" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} noInform={resources.NoInfo} /> :
    <div className="feature-detail">
        <HeaderFeature url="/" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} />
        <ContactInfoContent resources={resources} customerInfo={customerInfo} />
    </div>;

export default ContactInfoPage;
