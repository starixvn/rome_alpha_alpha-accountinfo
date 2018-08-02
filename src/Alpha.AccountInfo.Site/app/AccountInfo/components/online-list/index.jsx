import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import OnlineListContent from './online-list-content';

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
        const onlineListData = data.length > 0 ? data : null;
        
        return (
            <CustomizedAccordionItem
                title="Online List"
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>

                <table className="tblRpt tblRpt-bordered width-100per online-list">
                    <thead>
                        <th>#</th>
                        <th>IP</th>
                        <th>Login Time</th>
                    </thead>
                    <OnlineListContent onlineListData={onlineListData} />
                </table>
            </CustomizedAccordionItem>
      );
    }
}