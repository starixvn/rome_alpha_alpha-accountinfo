import React from 'react';
import BlockHeading from '../common/block-heading';
import Cell from '../common/cell';

export default class LoginInfoBlock extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <BlockHeading text="Login Info"
                    id="headingLoginInfo"
                    iconClassname="icon-login"
                    tabHref="collapseLoginInfo" />

                <div id="collapseLoginInfo"
                    className="panel-collapse collapse"
                    role="tabpanel"
                    aria-labelledby="headingLoginInfo">
                    <div className="panel-body login-info">
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
                    </div>
                </div>
            </div>
      );
    }
}