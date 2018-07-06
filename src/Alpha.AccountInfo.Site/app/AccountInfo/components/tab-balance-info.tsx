import * as React from 'react';

interface IBalanceInfoTabProps {
}

interface IBalanceInfoTabState {
}

export default class BalanceInfoTab extends React.Component<IBalanceInfoTabProps, IBalanceInfoTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading" role="tab" id="headingBalanceInfo">
                    <h4 className="panel-title">
                        <a className="" role="button" data-toggle="collapse" data-parent="#infoTab" href="#collapseBalanceInfo" aria-expanded="true" aria-controls="collapseBalanceInfo">
                            <span className="icon-balance"></span> Balance Info
                            </a>
                    </h4>
                </div>
                <div id="collapseBalanceInfo" className="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingBalanceInfo">
                    <div className="panel-body">
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Total Cash</div>
                            <div className="col-xs-6 col-right"><span className="number-red">-31,931.68</span></div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Yesterday Cash</div>
                            <div className="col-xs-6 col-right"><span className="number-red">-31,291.44</span></div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Total Balance</div>
                            <div className="col-xs-6 col-right"><span className="number-red">-27,715.24</span></div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-7">Yesterday Balance</div>
                            <div className="col-xs-5 col-right"><span className="number-red">-28,496.39</span></div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Outstanding</div>
                            <div className="col-xs-6 col-right box-edited">
                                <input type="text" className="txt-number-edited" value="1,159.70" />
                            </div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Used Credit</div>
                            <div className="col-xs-6 col-right"><span className="number">2,670,903.00</span></div>
                        </div>
                        <div className="col-xs-12 row-info">
                            <div className="col-xs-6">Given Credit</div>
                            <div className="col-xs-6 col-right box-edited" id="txt-given-edit">
                                <input type="text" className="txt-number-edited" value="300,000.00" />
                                <span className="icon-border-color icon-edited"></span>
                            </div>
                            <div id="popup-confirm-given-credit" className="popup-confirm">
                                <h1 className="title">Given Credit</h1>
                                <div className="col-xs-12 row-info">
                                    <div className="col-xs-7">Current Given Credit</div>
                                    <div className="col-xs-5 show-value">300,000,000.00</div>
                                </div>
                                <div className="col-xs-12 row-info">
                                    <div className="col-xs-7">New Given Credit</div>
                                    <div className="col-xs-5 text-right">
                                        <input type="text" className="hasBorder txt-newValue" value="350,000,000.00" />
                                    </div>
                                </div>
                                <div className="col-xs-12 row-info">
                                    <div className="col-xs-7">Difference</div>
                                    <div className="col-xs-5 show-value">50,000,000.00</div>
                                </div>
                                <div className="col-xs-12 row-info popup-bottom list-button">
                                    <button className="btn-cancel" id="closePopup">Cancel</button>
                                    <button className="btn-submit">Submit</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}