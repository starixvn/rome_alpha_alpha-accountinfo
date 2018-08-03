import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class LoginInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        const {
            handleCollapExpandBlock,
            blockId,
            expanded
        } = this.props;

        return (
            <CustomizedAccordionItem
                title="Login Info"
                iconClassname="icon-login"
                accordionItemBodyClassName="accordion__body block-login-info"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>

                <div className="group">
                    <div className="row-info bold">Created Date:</div>
                    <div className="row-info">{this.props.loginInfo.CreatedDate}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">Last Login:</div>
                    <div className="row-info">{this.props.loginInfo.LastLogin}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">Last Txn:</div>
                    <div className="row-info">{this.props.loginInfo.LastTxn}</div>
                </div>
                <div className="group">
                    <div className="row-info bold">Login IP:</div>
                    <div className="row-info">{this.props.loginInfo.LoginIP}</div>
                </div>
            </CustomizedAccordionItem>
      );
    }
}