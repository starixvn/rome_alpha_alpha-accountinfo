import React from 'react';
import { HashRouter, Switch, Route } from 'react-router-dom';
import ContentMobile from './content-mobile';

export default class AccountInfoMobile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirectToBalanceInfoPage: false,
            redirectToFeatureList: false
        };
    }

    componentWillReceiveProps(nextProps) {
        const { customerInfo } = this.props;
        const nextCustomerInfo = nextProps.customerInfo;

        this.setState({
            redirectToFeatureList: nextCustomerInfo === null,
            redirectToBalanceInfoPage: nextProps.redirectToBalanceInfoPage ?
                    nextCustomerInfo !== null && nextCustomerInfo !== customerInfo : false
        });
    }

    componentDidUpdate() {
        const self = this;
        const { redirectToFeatureList, redirectToBalanceInfoPage } = this.state;
        if (redirectToFeatureList || redirectToBalanceInfoPage) {
            self.setState({
                redirectToFeatureList: false,
                redirectToBalanceInfoPage: false
            });
        }
    }

    render() {
        const { redirectToBalanceInfoPage, redirectToFeatureList } = this.state;
        const { contentItems, customerInfo, handleOpenShowQRCode, resources } = this.props;

        const routePropsArray = [
            {
                contentItems: contentItems,
                pathname: '/featureList'
            },
            { ...contentItems.contactInfoProps },
            { ...contentItems.balanceInfoProps },
            { ...contentItems.downlineListProps },
            { ...contentItems.statusProps },
            { ...contentItems.settingProps },
            { ...contentItems.loginInfoProps },
            { ...contentItems.onlineListProps },
            {
                ...{ addAnimation: false },
                ...contentItems.balanceInfoProps,
                contentItems: customerInfo !== null ? [] : contentItems,
                pathname: ''
            }
        ];

        const showQRCodeUrl = customerInfo !== null ? customerInfo.ShowQRCodeUrl : '';
        return (
            <HashRouter>
                <Switch>
                    {
                        routePropsArray.map((routeProps, index) => {
                            return (
                                <Route
                                    key={index}
                                    exact path={routeProps.pathname}
                                    render={
                                        props =>
                                            <ContentMobile
                                                {...{
                                                    ...routeProps,
                                                    redirectToBalanceInfoPage,
                                                    redirectToFeatureList,
                                                    handleOpenShowQRCode,
                                                    showQRCodeUrl,
                                                    addAnimation: props.location.state !== undefined ? props.location.state.addAnimation : false,
                                                    backText: contentItems.contactInfoProps.backText,
                                                    resources
                                                }}
                                            />
                                    }
                                />
                            );
                        })
                    }
                </Switch>
            </HashRouter>
        );
    }
}