import * as React from 'react';

export default class AccountInfoDesktop extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expandedItems: this.initialExpandedItems(),
            collapseAll: false
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

    initialExpandedItems() {
        return {
            contactInfo: false,
            balanceInfo: true,
            downlineList: false,
            status: false,
            setting: false,
            loginInfo: false,
            onlineList: false
        };
    }

    resetColllapseAll(expandedItems) {
        const emptyObject = {};
        const keys = Object.keys(expandedItems);
        for (let i = 0; i < keys.length; i++) {
            emptyObject[keys[i]] = false;
        }

        return emptyObject;
    }

    updateState(blockId) {
        const { expandedItems } = this.state;
        const currentStatus = expandedItems[blockId];
        const emptyObject = this.resetColllapseAll(expandedItems);

        const newExpandedItems = Object.assign({}, emptyObject, { [blockId]: !currentStatus });
        const collapseAll = Object.keys(expandedItems).filter(key => newExpandedItems[key] === true).length === 0;

        this.setState({
            expandedItems: newExpandedItems,
            collapseAll
        });
    }

    handleCollapExpandBlock(e) {
        e.preventDefault();
        const blockId = e.currentTarget.dataset.blockid;

        this.updateState(blockId);
    }

    render() {
        const { expandedItems, collapseAll } = this.state;

        const { contents } = this.props;

        let accordionItems = {
            contactInfoProps: {
                ...contents.contactInfoProps,
                accordionItemBodyClassName: "contact-info",
                expanded: expandedItems.contactInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "contactInfo"
            },
            balanceInfoProps: {
                ...contents.balanceInfoProps,
                accordionItemBodyClassName: "balance-info",
                expanded: expandedItems.balanceInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "balanceInfo"
            },
            downlineListProps: {
                ...contents.downlineListProps,
                accordionItemBodyClassName: "downline-list",
                expanded: expandedItems.downlineList,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "downlineList"
            },
            statusProps: {
                ...contents.statusProps,
                accordionItemBodyClassName: "status",
                expanded: expandedItems.status,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "status"
            },
            settingProps: {
                ...contents.settingProps,
                accordionItemBodyClassName: "settings",
                expanded: expandedItems.setting,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "setting"
            },
            loginInfoProps: {
                ...contents.loginInfoProps,
                accordionItemBodyClassName: "login-info",
                expanded: expandedItems.loginInfo,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "loginInfo",
                visible: true
            },
            onlineListProps: {
                ...contents.onlineListProps,
                accordionItemBodyClassName: "online-list",
                expanded: expandedItems.onlineList,
                clickHanlder: this.handleCollapExpandBlock,
                blockId: "onlineList"
            }
        };

        return (
            <div className={`${this.state.animation} accordions`}>
                {
                    Object.values(accordionItems).map((item, index) => {
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