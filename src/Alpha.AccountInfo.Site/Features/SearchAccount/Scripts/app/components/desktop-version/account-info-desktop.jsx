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

    handleCollapExpandBlock(blockId, fetchDataFromServer) {
        const { expandedItems } = this.state;
        const currentStatus = expandedItems[blockId];

        if (fetchDataFromServer !== undefined) {
            fetchDataFromServer();
        }

        this.setState({
            expandedItems: Object.assign({}, this.initialExpandedItems(false), { [blockId]: !currentStatus })
        });
    }

    buildAccordionItems() {
        const { expandedItems } = this.state;
        const { contentItems } = this.props;

        const accordionItems = [
            {
                ...contentItems.contactInfoProps,
                blockId: 'contactInfo',
                expanded: expandedItems.contactInfo
            },
            {
                ...contentItems.balanceInfoProps,
                blockId: 'balanceInfo',
                expanded: expandedItems.balanceInfo
            },
            {
                ...contentItems.downlineListProps,
                blockId: 'downlineList',
                expanded: expandedItems.downlineList
            },
            {
                ...contentItems.statusProps,
                blockId: 'status',
                expanded: expandedItems.status
            },
            {
                ...contentItems.settingProps,
                blockId: 'settings',
                expanded: expandedItems.settings
            },
            {
                ...contentItems.loginInfoProps,
                blockId: 'loginInfo',
                expanded: expandedItems.loginInfo
            },
            {
                ...contentItems.onlineListProps,
                blockId: 'onlineList',
                expanded: expandedItems.onlineList
            }
        ];

        return accordionItems;
    }

    render() {
        return (
            <div className='accordions'>
                {
                    this.buildAccordionItems().map((item, index) => {
                        return item.visible &&
                            <CustomizedAccordionItem {...item} key={index} handleClick={this.handleCollapExpandBlock}>
                                {item.children}
                            </CustomizedAccordionItem>;
                    })}
            </div>
        );
    }
}