import React from 'react';

const CustomizedAccordionItem = ({
    featureText,
    iconClass,
    accordionItemBodyClassName,
    expanded,
    fetchDataFromServer,
    handleClick,
    blockId,
    children }) =>
    <div className={`accordion-item ${expanded ? 'expanded' : ''}`}>
        <div
            id={blockId}
            className="feature-item"
            onClick={() => { handleClick(blockId, fetchDataFromServer)}}
        >
            <span className={iconClass} /> {featureText}
        </div>
        <div className={`feature-detail ${accordionItemBodyClassName}`}>
            {children}
        </div>
    </div>;

export default CustomizedAccordionItem;