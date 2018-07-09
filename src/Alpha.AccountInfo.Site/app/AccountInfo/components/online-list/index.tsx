import * as React from 'react';
import HeadingTab from '../general/tab-heading';

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
                <HeadingTab text="Online List" id="headingOnlineList" iconClassname="icon-onlinelist" tabHref="collapseOnlineList" />

                <div id="collapseOnlineList" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOnlineList">
                    <div className="panel-body">
                        No information
                        </div>
                </div>
            </div>
      );
    }
}