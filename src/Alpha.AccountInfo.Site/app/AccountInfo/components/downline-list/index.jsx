import React from 'react';
import TotalDownlineList from './total-downline-list';
import DownlineInfo from './downline-info';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import DownlineListContent from './downline-list-content';

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
        this.data == null && this.setState({
            data: this.loadDownlineListFromServer()
        });
    }

    loadDownlineListFromServer() {
        console.log("load downline list");

        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    render() {
        const { blockId, expanded } = this.props;
        const { data } = this.state;

        return (
            <CustomizedAccordionItem
                title="Downline list"
                iconClassname="icon-downline"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>

                {data != null && <DownlineListContent downlineList={data} />}
            </CustomizedAccordionItem>    
        );
    }
}