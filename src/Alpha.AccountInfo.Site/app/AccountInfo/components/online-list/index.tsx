import * as React from 'react';
import HeadingTab from '../common/tab-heading';
import NoInfomation from './no-infomation';
import DownlineList from './downline-list';

interface IOnlineListTabProps {
   
}

interface IOnlineListTabState {
    downlineList: string[]
}

export default class OnlineListTab extends React.Component<IOnlineListTabProps, IOnlineListTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        let content = this.state.downlineList.length > 0 ? <DownlineList usernames={this.state.downlineList} /> : <NoInfomation />
        return (
            <div className="panel panel-default">
                <HeadingTab text="Online List" id="headingOnlineList" iconClassname="icon-onlinelist" tabHref="collapseOnlineList" />
                <div id="collapseOnlineList" className="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOnlineList">
                    {content}
                </div>
            </div>
      );
    }
}