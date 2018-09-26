import * as React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import OnlineListContent from './online-list-content';

const OnlineListDesktop = ({
    data,
    resources,
    handleOpenIPInfo,
    expanded,
    blockId,
    handleCollapExpandBlock }) => data === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.OnlineList}
            iconClassname="icon-onlinelist"
            accordionItemBodyClassName="accordion__body online-list"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <OnlineListContent resources={resources} data={data} handleOpenIPInfo={handleOpenIPInfo} />
        </CustomizedAccordionItem>;

export default OnlineListDesktop;
