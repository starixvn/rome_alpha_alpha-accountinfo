import React from 'react';
import Status from './status';
import CellContainLink from '../common/cell-contain-link';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class StatusBlock extends React.Component {
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
    }

    loadDataFromServer() {
        console.log("load customerstatus");

        const xhr = new XMLHttpRequest();
        xhr.open('get', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data.Status.Settings });
        };
        xhr.send();
    }
    render() {
        const { handleCollapExpandBlock, blockId, expanded } = this.props;

        <CellContainLink title="Enable Keno01"
            classNames="row-info"
            iconClassname="icon-ic_list"
            hrefLink="#" />
        return (
            <CustomizedAccordionItem
                title="Status"
                iconClassname="icon-status"
                accordionItemBodyClassName="accordion__body block-status"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>
                {this.state.data != null &&  this.state.data.map((status, index) => {
                    return status.IsEditOnProductLevel != undefined && !status.IsEditOnProductLevel ?
                        <CellContainLink title={status.Text}
                            statusId={status.DisplayOrder}
                            key={index}
                            classNames="row-info"
                            iconClassname="icon-ic_list"
                            hrefLink="#" /> :
                        <Status title={status.Text}
                            checked={status.Checked}
                            key={index}
                            statusId={status.DisplayOrder} /> 
                      
                })}
            </CustomizedAccordionItem>
      );
    }
}