import * as React from 'react';
import ContactInfoContent from '../contents/contact-info-content';
import MobilePage from './mobile-page';

const ContactInfoPage = ({
    customerInfo,
    resources,
    redirectToBalanceInfoPage }) =>
    <MobilePage iconClass="icon-contact-info"
        featureText={resources.ShowContactInfo}
        noInfoText={resources.NoInfo}
        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
        data={customerInfo}
    >
        <ContactInfoContent resources={resources} customerInfo={customerInfo} />
    </MobilePage>;


export default ContactInfoPage;