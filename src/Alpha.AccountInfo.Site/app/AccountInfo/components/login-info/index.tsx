import * as React from 'react';
import BlockHeading from '../common/block-heading';
import Cell from '../common/cell';

interface ILoginInfoBlockProps {
}

interface ILoginInfoBlockState {
}

export default class LoginInfoBlock extends React.Component<ILoginInfoBlockProps, ILoginInfoBlockState> {
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
                            <Cell text="8/5/2016 1:52 AM" classNames="row-info" />
                        </div>
                        <div className="group">
                            <Cell text="Last Login:" classNames="row-info bold" />
                            <Cell text="8/28/2017 10:24 PM" classNames="row-info" />
                        </div>
                        <div className="group">
                            <Cell text="Last Txn:" classNames="row-info bold" />
                        </div>
                        <div className="group">
                            <Cell text="Login IP:" classNames="row-info bold" />
                            <Cell text="202.78.231.34" classNames="row-info" />
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}