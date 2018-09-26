import * as React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineListContent from './downline-list-content';

const DownlineListDesktop = ({
    data,
    resources,
    handleSearchFromUserLevel,
    expanded,
    blockId,
    handleCollapExpandBlock }) => data === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.DownlineList}
            iconClassname="icon-downline"
            accordionItemBodyClassName="accordion__body downline-list"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <DownlineListContent resources= {resources}  data={data} handleSearchFromUserLevel={handleSearchFromUserLevel} />
        </CustomizedAccordionItem>;

export default DownlineListDesktop;