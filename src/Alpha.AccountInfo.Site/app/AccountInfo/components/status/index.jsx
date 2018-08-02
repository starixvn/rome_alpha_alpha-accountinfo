import React from 'react';
import Status from './status';
import CellContainLink from '../common/cell-contain-link';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class StatusBlock extends React.Component {
    constructor(props) {
        super(props);
    }


    render() {
        const { getCustomerAllStatus, blockId, expanded, statusData } = this.props;

        return (
            <CustomizedAccordionItem
                title="Status"
                iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status"
                expanded={expanded}
                handleClick={getCustomerAllStatus}
                blockId={blockId}>
                {statusData != null && statusData.Settings.map((status, index) => {
                    return status.IsEditOnProductLevel != undefined && !status.IsEditOnProductLevel ?
                        <CellContainLink title={status.Text}
                            statusId={status.DisplayOrder}
                            key={index}
                            classNames="col"
                            iconClassname="icon-ic_list"
                            hrefLink="#" /> :
                        <Status classNames='col checkbox' title={status.Text}
                            checked={status.Checked}
                            key={index}
                            statusId={status.DisplayOrder} /> 
                      
                })}
            </CustomizedAccordionItem>
      );
    }
}