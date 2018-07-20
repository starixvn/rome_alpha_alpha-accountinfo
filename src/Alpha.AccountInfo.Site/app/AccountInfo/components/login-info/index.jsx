import React from 'react';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item'

export default class LoginInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <CustomizedAccordionItem title="Login Info" iconClassname="icon-login"
                accordionItemBodyClassName="accordion__body block-login-info">
                <div className="group">
                    <Cell text="Created Date:" classNames="row-info bold" />
                    <Cell text={this.props.loginInfo.CreatedDate} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell text="Last Login:" classNames="row-info bold" />
                    <Cell text={this.props.loginInfo.LastLogin} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell text="Last Txn:" classNames="row-info bold" />
                    <Cell text={this.props.loginInfo.LastTxn} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell text="Login IP:" classNames="row-info bold" />
                    <Cell text={this.props.loginInfo.LoginIP} classNames="row-info" />
                </div>
            </CustomizedAccordionItem>
      );
    }
}