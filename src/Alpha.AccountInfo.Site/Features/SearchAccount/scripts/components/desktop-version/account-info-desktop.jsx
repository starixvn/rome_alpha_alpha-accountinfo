import * as React from 'react';
import CustomizedAccordionItem from './customized-accordion-item';

export default class AccountInfoDesktop extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: this.initialExpandedItems()
        };

        this.handleCollapExpandBlock = this.handleCollapExpandBlock.bind(this);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.redirectToBalanceInfoPage) {
            this.setState({
                expandedItems: this.initialExpandedItems()
            });
        }
    }

    initialExpandedItems(expandedBalanceInfo = true) {
        return {
            contactInfo: false,
            balanceInfo: expandedBalanceInfo,
            downlineList: false,
            status: false,
            settings: false,
            loginInfo: false,
            onlineList: false
        };
    }

    updateState(blockId) {
        const { expandedItems } = this.state;
        const currentStatus = expandedItems[blockId];

        this.setState({
            expandedItems: Object.assign({}, this.initialExpandedItems(false), { [blockId]: !currentStatus })
        });
    }

    handleCollapExpandBlock(e) {
        e.preventDefault();
        this.updateState(e.currentTarget.dataset.blockid);
    }

    buildAccordionItems() {
        const { expandedItems } = this.state;
        const { contentItems } = this.props;

        let accordionItems = [
            {
                ...contentItems.contactInfoProps,
                blockId: "contactInfo",
                expanded: expandedItems.contactInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.balanceInfoProps,
                blockId: "balanceInfo",
                expanded: expandedItems.balanceInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.downlineListProps,
                blockId: "downlineList",
                expanded: expandedItems.downlineList,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.statusProps,
                blockId: "status",
                expanded: expandedItems.status,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.settingProps,
                blockId: "settings",
                expanded: expandedItems.settings,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.loginInfoProps,
                blockId: "loginInfo",
                expanded: expandedItems.loginInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.onlineListProps,
                blockId: "onlineList",
                expanded: expandedItems.onlineList,
                clickHanlder: this.handleCollapExpandBlock
            }
        ];

        return accordionItems;
    }

    render() {
        return (
            <div className={`${this.state.animation} accordions`}>
                {
                    this.buildAccordionItems().map((item, index) => {
                        let accordionProps = {
                            title: item.featureText ,
                                iconClassname: item.iconClass ,
                                accordionItemBodyClassName: item.accordionItemBodyClassName ,
                                expanded: item.expanded ,
                                handleClick: item.clickHanlder ,
                                blockId: item.blockId ,
                                key: index 
                        };

                        return item.visible &&
                            <CustomizedAccordionItem {...accordionProps}> {item.children} </CustomizedAccordionItem>;
                    })}
            </div>
        );
    }
}