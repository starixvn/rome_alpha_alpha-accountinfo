import * as React from 'react';

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

    render() {
        const { expandedItems } = this.state;
        const { contentItems } = this.props;

        let accordionItems = [
            {
                ...contentItems.contactInfoProps,
                accordionItemBodyClassName: "contact-info",
                blockId: "contactInfo",
                expanded: expandedItems.contactInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.balanceInfoProps,
                accordionItemBodyClassName: "balance-info",
                blockId: "balanceInfo",
                expanded: expandedItems.balanceInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.downlineListProps,
                accordionItemBodyClassName: "downline-list",
                blockId: "downlineList",
                expanded: expandedItems.downlineList,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.statusProps,
                accordionItemBodyClassName: "status",
                blockId: "status",
                expanded: expandedItems.status,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.settingProps,
                accordionItemBodyClassName: "settings",
                blockId: "settings",
                expanded: expandedItems.settings,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.loginInfoProps,
                accordionItemBodyClassName: "login-info",
                blockId: "loginInfo",
                expanded: expandedItems.loginInfo,
                clickHanlder: this.handleCollapExpandBlock
            },
            {
                ...contentItems.onlineListProps,
                accordionItemBodyClassName: "online-list",
                blockId: "onlineList",
                expanded: expandedItems.onlineList,
                clickHanlder: this.handleCollapExpandBlock
            }
        ];

        return (
            <div className={`${this.state.animation} accordions`}>
                {
                    accordionItems.map((item, index) => {
                        return item.visible &&
                            <div
                                className={item.expanded ? `accordion-item expanded` : `accordion-item`}
                                expanded={item.expanded.toString()}
                                key={index}
                            >
                                <div
                                    className="feature-item"
                                    onClick={item.clickHanlder}
                                    data-blockid={item.blockId}
                                >
                                    <span className={item.iconClass} /> {item.featureText}
                                </div>
                                <div className={`feature-detail ${item.accordionItemBodyClassName}`}>
                                    {item.children}
                                </div>
                            </div>;
                    })}
            </div>
        );
    }
}