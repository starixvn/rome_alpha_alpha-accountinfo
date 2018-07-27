import React from 'react';
import NoInfomation from '../common/no-infomation';
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
        const { handleCollapExpandBlock, blockId, expanded } = this.props;
        const { downlineList } = this.state;

        let content = downlineList.length > 0 ?
            <DownlineList usernames={downlineList} /> : <NoInfomation title="This user is not online" />;

        return (
            <CustomizedAccordionItem
                title="Online List"
                iconClassname="icon-onlinelist"
                expanded={expanded}
                handleClick={handleCollapExpandBlock}
                blockId={blockId}>
                {content}
            </CustomizedAccordionItem>
      );
    }
}