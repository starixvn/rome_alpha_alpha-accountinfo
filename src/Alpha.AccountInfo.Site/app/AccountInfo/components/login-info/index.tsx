import * as React from 'react';
import HeadingTab from '../general/tab-heading';
import Cell from '../general/cell';

interface ILoginInfoTabProps {
}

interface ILoginInfoTabState {
}

export default class LoginInfoTab extends React.Component<ILoginInfoTabProps, ILoginInfoTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <HeadingTab text="Login Info" id="headingLoginInfo" iconClassname="icon-login" tabHref="collapseLoginInfo" />

                <div id="collapseLoginInfo" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingLoginInfo">
                    <div className="panel-body login-info">
                        <div className="group">
                            <Cell text="Created Date:" classnames="row-info bold" />
                            <Cell text="8/5/2016 1:52 AM" classnames="row-info" />
                        </div>
                        <div className="group">
                            <Cell text="Last Login:" classnames="row-info bold" />
                            <Cell text="8/28/2017 10:24 PM" classnames="row-info" />
                        </div>
                        <div className="group">
                            <Cell text="Last Txn:" classnames="row-info bold" />
                        </div>
                        <div className="group">
                            <Cell text="Login IP:" classnames="row-info bold" />
                            <Cell text="202.78.231.34" classnames="row-info" />
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}