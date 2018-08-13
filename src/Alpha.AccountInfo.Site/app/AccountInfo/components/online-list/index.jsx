import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import OnlineListContent from './online-list-content';
import { accountInfoAPI } from '../../api-client';

export default class OnlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            custId: this.props.custId,
            data: null
        }

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(e) {
        this.props.handleCollapExpandBlock(e);
        this.loadOnlineListFromServer();
    }

    loadOnlineListFromServer() {
        accountInfoAPI.getOnlineList({ custid: this.props.custId }).then((data) => {
            this.setState({ data: data });
        });
    }

    render() {
        const { resources, expanded, handleCollapExpandBlock, blockId } = this.props;
        const { data } = this.state;
        
        return (
            <CustomizedAccordionItem
                title={resources.OnlineList}
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>

                {
                    <table className="tblRpt tblRpt-bordered width-100per online-list">
                        <thead>
                            <th>{resources.NumberOrder}</th>
                            <th>{resources.IP}</th>
                            <th>{resources.LoginTime}</th>
                        </thead>
                        <OnlineListContent resources={resources} data={data} />
                    </table>
                }
            </CustomizedAccordionItem>
      );
    }
}