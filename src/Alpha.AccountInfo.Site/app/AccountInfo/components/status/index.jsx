import React from 'react';
import Status from './status';
import CellContainLink from '../common/cell-contain-link';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expanded: this.props.expanded || false
        }
    }
    render() {
        const { handleCollapExpandBlock, blockId, expanded } = this.props;

        return (
            <CustomizedAccordionItem
                title="Status"
                iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>

                <Status title="Suspended" checked={true} statusId={1} />
                <Status title="Auto PT" checked={true} statusId={2}  />
                <Status title="Outright" checked={true} statusId={3}  />
                <Status title="Outright" checked={true} statusId={4}  />
                <Status title="Double Comm" statusId={5}  />

                <CellContainLink title="Enable Keno01"
                    classNames="row-info"
                    iconClassname="icon-ic_list"
                    hrefLink="#" />

                <Status title="Colossus Bets" checked={true} statusId={6}  />
                <Status title="Closed" statusId={7} />
                <Status title="Virtual Sports" statusId={8}  />
                <Status title="HR Fixed Odds" statusId={9}  />
                <Status title="Allbet" statusId={10}  />
            </CustomizedAccordionItem>
      );
    }
}