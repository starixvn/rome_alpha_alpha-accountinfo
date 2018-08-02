import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineListContent from './downline-list-content';
import NoInfomation from '../../components/common/no-infomation'

export default class DownlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            custId: this.props.custId,
            url: window.accountInfoSiteAppPath + "accountinfo/getdownlinelist",
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
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            this.setState({ data: JSON.parse(xhr.responseText).Result });
        };
        xhr.send();
    }

    render() {
        const { blockId, expanded, fetchCustomerInfoFromServer } = this.props;
        const { data } = this.state;

        const content = data == null || data.DownlineList.length == 0 ?
            <NoInfomation title="No information is available" /> :
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