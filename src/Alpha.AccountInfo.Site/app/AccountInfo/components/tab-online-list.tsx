import * as React from 'react';

interface IOnlineListTabProps {
}

interface IOnlineListTabState {
}

export default class OnlineListTab extends React.Component<IOnlineListTabProps, IOnlineListTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading" role="tab" id="headingOnlineList">
                    <h4 className="panel-title">
                        <a className="collapsed" role="button" data-toggle="collapse" data-parent="#infoTab" href="#collapseOnlineList" aria-expanded="false" aria-controls="collapseOnlineList">
                            <span className="icon-onlinelist"></span> Online List
                            </a>
                    </h4>
                </div>
                <div id="collapseOnlineList" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOnlineList">
                    <div className="panel-body">
                        No information
                        </div>
                </div>
            </div>
      );
    }
}