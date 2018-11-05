import React from 'react';

const CustomizedAccordionItem = ({
    title,
    iconClassname,
    accordionItemBodyClassName,
    expanded,
    handleClick,
    blockId,
    children }) =>
    <div className={`accordion-item ${expanded ? 'expanded' : ''}`}>
        <div id={blockId} className="feature-item" onClick={handleClick} data-blockid={blockId}>
            <span className={iconClassname} /> {title}
        </div>
        <div className={`feature-detail ${accordionItemBodyClassName}`}>
            {children}
        </div>
    </div>;

export default CustomizedAccordionItem;