import React from 'react';

const CustomizedAccordionItem = ({ title, iconClassname, accordionItemBodyClassName, expanded, handleClick, blockId, children }) => {
    let expandedAccordionClass = expanded ? "accordion-item expanded" : "accordion-item";
    return (
        <div className={expandedAccordionClass} expanded={expanded}>
            <div className="feature-item" onClick={handleClick} data-blockid={blockId}>
                <span className={iconClassname} /> {title}
            </div>
            <div className={`feature-detail ${accordionItemBodyClassName}`}>
                {children}
            </div>
        </div>
    );
}

export default CustomizedAccordionItem;