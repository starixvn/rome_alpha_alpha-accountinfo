import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import OnlineListContent from './online-list-content';

export default class OnlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            custId: this.props.custId,
            url: window.accountInfoSiteAppPath + "accountinfo/getonlinelist",
            data: null
        }

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(e) {
        this.props.handleCollapExpandBlock(e);
        this.loadOnlineListFromServer();
    }

    loadOnlineListFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('post', this.state.url + "?custid=" + this.props.custId);
        xhr.onload = () => {
            this.setState({ data: JSON.parse(xhr.responseText).Result });
        };
        xhr.send();
    }

    render() {
        const { expanded, handleCollapExpandBlock, blockId } = this.props;
        const { data } = this.state;
        
        return (
            <CustomizedAccordionItem
                title="Online List"
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={this.handleClick}
                blockId={blockId}>

                {
                    <table className="tblRpt tblRpt-bordered width-100per online-list">
                        <thead>
                            <th>#</th>
                            <th>IP</th>
                            <th>Login Time</th>
                        </thead>
                        <OnlineListContent data={data} />
                    </table>
                }
            </CustomizedAccordionItem>
      );
    }
}