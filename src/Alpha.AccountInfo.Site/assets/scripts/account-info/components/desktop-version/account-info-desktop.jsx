import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import CustomizedAccordionItem from './customized-accordion-item';
import { BuildContentItemsDesktop } from './build-content-items-desktop';

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

        const {
            resources,
            mobileView,
            actorRoleId,
            customerInfo,
            downlineList,
            onlineList,
            statusData,
            settingData,
            redirectToBalanceInfoPage,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo } = this.props;

        let accordionItems = BuildContentItemsDesktop(
            resources,
            mobileView,
            actorRoleId,
            customerInfo,
            downlineList,
            onlineList,
            statusData,
            settingData,
            fetchCustomerInfoFromServer,
            handleSearchFromUserLevel,
            handleOpenIPInfo,
            expandedItems,
            this.handleCollapExpandBlock);

        return <div className={`${this.state.animation} accordions`}>
            {
                Object.values(accordionItems).map((item, index) => {
                    return item.visible &&
                        <CustomizedAccordionItem
                            title={item.featureText}
                            iconClassname={item.iconClass}
                            accordionItemBodyClassName={item.accordionItemBodyClassName}
                            expanded={item.expanded}
                            handleClick={item.clickHanlder}
                            blockId={item.blockId}
                            key={index}
                        >
                            {item.children}
                        </CustomizedAccordionItem>;
                })}
        </div>;
    }
}