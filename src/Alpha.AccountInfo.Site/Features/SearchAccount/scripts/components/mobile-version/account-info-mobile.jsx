import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route, Redirect } from 'react-router-dom';
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
        let { customerInfo } = this.props;
        let nextCustomerInfo = nextProps.customerInfo;

        this.setState({
            redirectToFeatureList: nextCustomerInfo === null,
            redirectToBalanceInfoPage: nextProps.redirectToBalanceInfoPage ?
                    nextCustomerInfo !== null && nextCustomerInfo !== customerInfo : false
        });
    }

    componentDidUpdate(prevProps, prevState) {
        let self = this;
        let { redirectToFeatureList, redirectToBalanceInfoPage } = this.state;
        if (redirectToFeatureList || redirectToBalanceInfoPage) {
            self.setState({
                redirectToFeatureList: false,
                redirectToBalanceInfoPage: false
            });
        }
    }

    buildRoutes() {
        const { contentItems, customerInfo } = this.props;
        let routeItems = [
            {
                contentItems: contentItems,
                path: '/featureList'
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
                path: ''
            }
        ];

        return routeItems;
    }

    render() {
        const { redirectToBalanceInfoPage, redirectToFeatureList } = this.state;

        return (
            <HashRouter>
                <Switch>
                    {
                        this.buildRoutes().map((route, index) => {
                            return (
                                <Route
                                    key={index}
                                    exact path={route.path}
                                    render={
                                        (props) =>
                                            <ContentMobile
                                                {...route}
                                                {...{ addAnimation: props.location.state !== undefined ? props.location.state.addAnimation : false }}
                                                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                                                redirectToFeatureList={redirectToFeatureList}
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