import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineListContent from './downline-list-content';
import NoInfomation from '../../components/common/no-infomation';
import { accountInfoAPI } from '../../api-client';

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            custId: this.props.custId,
            data: null
        }
        this.handleClick = this.handleClick.bind(this);
    }

    componentWillReceiveProps(props) {
        if (props.custId != this.state.custId) {
            this.setState({
                custId: props.custId,
                data: null
            });
        }
    }

    componentDidMount() {
    
    }

    handleClick(e) {
        this.props.handleCollapExpandBlock(e);
        this.loadDownlineListFromServer();
    }

    loadDownlineListFromServer() {
        accountInfoAPI.getDownlineList({ custid: this.props.custId }).then((data) => {
            this.setState({ data: data });
        });
    }

    render() {
        const { blockId, expanded, fetchCustomerInfoFromServer } = this.props;
        const { data } = this.state;

        const content = data == null || data.DownlineList.length == 0 ?
            <NoInfomation /> :
            <DownlineListContent downlineList={data} fetchCustomerInfoFromServer={fetchCustomerInfoFromServer} />;
           
        return (
            <CustomizedAccordionItem
                title="Downline list"
                iconClassname="icon-downline"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>
                {content}
            </CustomizedAccordionItem>    
        );
    }
}