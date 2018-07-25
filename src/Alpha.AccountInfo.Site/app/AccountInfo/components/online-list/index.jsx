import React from 'react';
import NoInfomation from './no-infomation';
import DownlineList from './downline-list';
import CustomizedAccordionItem from '../common/customized-accordion-item';

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

        const { handleClick, dataId, expanded } = this.props;

        return (
            <CustomizedAccordionItem
                title="Online List"
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={handleClick}
                dataId={dataId}>
                {content}
            </CustomizedAccordionItem>
      );
    }
}