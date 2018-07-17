import React from 'react';
import BlockHeading from '../common/block-heading';
import NoInfomation from './no-infomation';
import DownlineList from './downline-list';

export default class OnlineListBlock extends React.Component{
    constructor(props) {
        super(props);
        this.state = {
            downlineList: ["master1", "master2"]
        };
    }
    render() {
        let content = this.state.downlineList.length > 0 ?
            <DownlineList usernames={this.state.downlineList} /> : <NoInfomation />;

        return (
            <div className="panel panel-default">
                <BlockHeading
                    text="Online List"
                    id="headingOnlineList"
                    iconClassname="icon-onlinelist"
                    tabHref="collapseOnlineList" />
                <div id="collapseOnlineList"
                    className="panel-collapse collapse"
                    role="tabpanel"
                    aria-labelledby="headingOnlineList">
                    {content}
                </div>
            </div>
      );
    }
}