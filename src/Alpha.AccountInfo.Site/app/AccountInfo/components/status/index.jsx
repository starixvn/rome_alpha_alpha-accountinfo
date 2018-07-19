import React from 'react';
import Status from './status';
import CellContainLink from '../common/cell-contain-link';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <CustomizedAccordionItem title="Status" iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status">
                <div className="col-xs-6 px-0">
                    <Status text="Suspended" checked={true} />
                    <Status text="Auto PT" checked={true} />
                    <Status text="Outright" checked={true} />
                    <Status text="Outright" checked={true} />
                    <Status text="Double Comm" />

                    <CellContainLink text="Enable Keno01"
                        classNames="row-info"
                        iconClassname="icon-ic_list"
                        hrefLink="#" />

                    <Status text="Colossus Bets" checked={true} />
                </div>
                <div className="col-xs-6 px-0">
                    <Status text="Closed" />
                    <Status text="Virtual Sports" />
                    <Status text="HR Fixed Odds" />
                    <Status text="Allbet" />
                </div>
            </CustomizedAccordionItem>
      );
    }
}