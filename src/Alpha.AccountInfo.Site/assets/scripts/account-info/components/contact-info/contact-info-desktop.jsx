import * as React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import ContactInfoContent from './contact-info-content';

const ContactInfoDesktop = ({
    customerInfo,
    resources,
    expanded,
    blockId,
    handleCollapExpandBlock }) => customerInfo === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.ShowContactInfo}
            iconClassname="icon-contact-info"
            accordionItemBodyClassName="accordion__body contact-info"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <ContactInfoContent resources={resources} customerInfo={customerInfo} />
        </CustomizedAccordionItem>;

export default ContactInfoDesktop;
