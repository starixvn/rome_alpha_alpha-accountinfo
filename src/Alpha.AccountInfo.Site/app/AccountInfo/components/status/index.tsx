import * as React from 'react';
import HeadingTab from '../common/tab-heading';
import Status from './status';

interface IStatusTabProps {
}

interface IStatusTabState {
}

export default class StatusTab extends React.Component<IStatusTabProps, IStatusTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <HeadingTab text="Status" id="headingStatus" iconClassname="icon-status" tabHref="collapseStatus" />

                <div id="collapseStatus" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingStatus">
                    <div className="panel-body">
                        <div className="col-xs-6 no-p-x">
                            <Status text="Suspended" checked={true} id="chkSuspended" />
                            <Status text="Auto PT" checked={true}/>
                            <Status text="Outright" checked={true} />
                            <Status text="Outright" checked={true} />
                            <Status text="Double Comm"/>

                            <div className="row-info">
                                <a href="#" id="enableKeno"><span className="icon-ic_list"></span> Enable Keno</a>
                            </div>

                            <Status text="Colossus Bets" checked={true} />
                        </div>
                        <div className="col-xs-6 no-p-x">
                            <div className="row-info checkbox">
                                <label><input type="checkbox" id="chkClosed" /> Closed</label>
                            </div>

                            <div className="row-info checkbox">
                                <label><input type="checkbox" /> Virtual Sports</label>
                            </div>
                            <div className="row-info checkbox">
                                <label><input type="checkbox" /> HR Fixed Odds</label>
                            </div>
                            <div className="row-info">
                                <a href="#" id="enableRacing"><span className="icon-ic_list"></span> Enable Racing</a>
                                <div className="popup-racing" id="racingPopup">
                                    <ul className="racing">
                                        <li className="group">Racing</li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" checked />UK/Irish Tote</label>
                                        </li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" checked />SA Tote</label>
                                        </li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" />Aust Tote</label>
                                        </li>
                                        <li className="group">Greyhound</li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" />UK/Irish Tote</label>
                                        </li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" checked />Aust Tote</label>
                                        </li>
                                        <li className="group">Harness</li>
                                        <li className="checkbox">
                                            <label><input type="checkbox" />Aust Tote</label>
                                        </li>
                                    </ul>
                                    <div className="popup-bottom list-button">
                                        <button className="btn-cancel">Cancel</button>
                                        <button className="btn-refresh">Update</button>
                                    </div>
                                </div>
                            </div>
                            <div className="row-info checkbox">
                                <label><input type="checkbox" checked /> Allbet</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}