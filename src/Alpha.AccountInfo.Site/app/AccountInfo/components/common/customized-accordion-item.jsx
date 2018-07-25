import React from 'react';
import {
    AccordionItem,
    AccordionItemTitle,
    AccordionItemBody,
} from 'react-accessible-accordion';

const CustomizedAccordionItem = ({ title, iconClassname, accordionItemBodyClassName,
                            expanded, handleClick, dataId, children = null }) =>
    <AccordionItem expanded={expanded}>
        <AccordionItemTitle onClick={handleClick} data-id={dataId}>
            <span className={iconClassname}></span> {title}
        </AccordionItemTitle>
        <AccordionItemBody className={accordionItemBodyClassName}>
            {children}
        </AccordionItemBody>
    </AccordionItem>

export default CustomizedAccordionItem;