import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import SettingContent from './setting-list-content';

export default class SettingBlock extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            custId: this.props.custId,
            url: window.accountInfoSiteAppPath + "accountinfo/getcustomerstatus",
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

    handleClick(e) {
        this.props.handleCollapExpandBlock(e);
        this.loadDataFromServer();
        //this.data == null && this.setState({
        //    data: this.loadDataFromServer()
        //});
    }

    loadDataFromServer() {
        console.log("load customerstatus");

        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data.Setting });
        };
        xhr.send();
    }


    render() {
        const { blockId, expanded } = this.props;

        return (
            <CustomizedAccordionItem
                title="Settings"
                iconClassname="icon-settings"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>
                {this.state.data != null && <SettingContent setting={this.state.data } />}
            </CustomizedAccordionItem>
        );
    }
}