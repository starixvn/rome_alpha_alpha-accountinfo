import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import { Button, Modal } from 'react-bootstrap';

export default class LoginInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        const {
            resources,
            handleCollapExpandBlock,
            blockId,
            expanded
        } = this.props;

        return (
            <CustomizedAccordionItem
                title="resources.LoginInfo"
                iconClassname="icon-login"
                accordionItemBodyClassName="accordion__body block-login-info"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>

                <div className="group">
                    <div className="row-info bold">{resources.CreatedDate}:</div>
                    <div className="row-info">{this.props.loginInfo.CreatedDate}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LastLogin}:</div>
                    <div className="row-info">{this.props.loginInfo.LastLogin}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LastTxn}:</div>
                    <div className="row-info">{this.props.loginInfo.LastTxn}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">{resources.LoginIP}:</div>
                    <div className="row-info">{this.props.loginInfo.LoginIP}</div>
                </div>
            </CustomizedAccordionItem>
      );
    }
}