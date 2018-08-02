import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import Table from '../common/table/index';
import HeadTable from '../common/table/head-table';
import BodyTable from '../common/table/body-table';
import ColTable from '../common/table/col-table';
import RowNoInformation from '../common/table/row-no-infomation';
import RowTable from '../common/table/row-table';

export default class OnlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            data: [
                { id: '1', ip: '125.227.140.97', dateTime: '27 Jul 02:31 AM' },
                { id: '2', ip: '125.227.140.97', dateTime: '28 Jul 02:31 AM' }
            ]
        }
    }
    render() {
        const { expanded, handleCollapExpandBlock, blockId } = this.props;
        const { data } = this.state;

        return (
            <CustomizedAccordionItem
                title="Online List"
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>

                <Table classNames="tblRpt tblRpt-bordered width-100per online-list">
                    <HeadTable>
                        <RowTable>
                            <ColTable title='#' isHeadeTable={true} />
                            <ColTable title='IP' isHeadeTable={true} />
                            <ColTable title='Login Time' isHeadeTable={true} />
                        </RowTable>                        
                    </HeadTable>
                    <BodyTable>

                        {data.length === 0 && <RowNoInformation classNames="text-center" title="No information" colspan="3" />}

                        {data.length !== 0 &&
                            data.map(rowObj =>
                                <RowTable key={rowObj.id}>
                                    <ColTable classNames="text-center" title={rowObj.id} />
                                    <ColTable title={rowObj.ip} />
                                    <ColTable classNames="text-center" title={rowObj.dateTime} />
                                </RowTable>
                            )
                        }
                    </BodyTable>
                </Table>
            </CustomizedAccordionItem>
      );
    }
}