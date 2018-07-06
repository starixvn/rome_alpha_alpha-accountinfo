import * as React from 'react';

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
                <div className="panel-heading" role="tab" id="headingLoginInfo">
                    <h4 className="panel-title">
                        <a className="collapsed" role="button" data-toggle="collapse" data-parent="#infoTab" href="#collapseLoginInfo" aria-expanded="false" aria-controls="collapseLoginInfo">
                            <span className="icon-login"></span> Login Info
                            </a>
                    </h4>
                </div>
                <div id="collapseLoginInfo" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingLoginInfo">
                    <div className="panel-body login-info">
                        <div className="group">
                            <div className="row-info bold">Created Date:</div>
                            <div className="row-info">8/5/2016 1:52 AM</div>
                        </div>
                        <div className="group">
                            <div className="row-info bold">Last Login:</div>
                            <div className="row-info">8/28/2017 10:24 PM</div>
                        </div>
                        <div className="group">
                            <div className="row-info bold">Last Txn:</div>
                        </div>
                        <div className="group">
                            <div className="row-info bold">Login IP:</div>
                            <div className="row-info">202.78.231.34</div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}