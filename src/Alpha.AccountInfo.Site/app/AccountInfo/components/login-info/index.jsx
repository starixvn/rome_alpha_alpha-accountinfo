import React from 'react';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item'

export default class LoginInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        const {
            handleClick,
            dataId,
            expanded
        } = this.props;

        return (
            <CustomizedAccordionItem
                title="Login Info"
                iconClassname="icon-login"
                accordionItemBodyClassName="accordion__body block-login-info"
                expanded={expanded}
                handleClick={handleClick}
                dataId={dataId}>

                <div className="group">
                    <Cell title="Created Date:" classNames="row-info bold" />
                    <Cell title={this.props.loginInfo.CreatedDate} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell title="Last Login:" classNames="row-info bold" />
                    <Cell title={this.props.loginInfo.LastLogin} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell title="Last Txn:" classNames="row-info bold" />
                    <Cell title={this.props.loginInfo.LastTxn} classNames="row-info" />
                </div>
                <div className="group">
                    <Cell title="Login IP:" classNames="row-info bold" />
                    <Cell title={this.props.loginInfo.LoginIP} classNames="row-info" />
                </div>
            </CustomizedAccordionItem>
      );
    }
}