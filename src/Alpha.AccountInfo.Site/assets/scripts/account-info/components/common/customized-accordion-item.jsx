import React from 'react';
import {
    AccordionItem,
    AccordionItemTitle,
    AccordionItemBody
} from 'react-accessible-accordion';

const CustomizedAccordionItem = ({ title, iconClassname, accordionItemBodyClassName, expanded, handleClick, blockId, children }) =>
    <AccordionItem expanded={expanded}>
        <AccordionItemTitle onClick={handleClick} data-blockid={blockId}>
            <span className={iconClassname} /> {title}
        </AccordionItemTitle>
        <AccordionItemBody className={accordionItemBodyClassName}>
            {children}
        </AccordionItemBody>
    </AccordionItem>;

export default CustomizedAccordionItem;