import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route } from 'react-router-dom';
import FeatureListPage from './feature-list-page';
import MobilePage from './mobile-page';
import { BuildContentItemsMobile } from './build-content-items-mobile';
import DebugRouter from '../../../helpers/router-debugger';

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
        this.setState({
            redirectToFeatureList: nextProps.customerInfo === null,
            redirectToBalanceInfoPage: nextProps.customerInfo !== null && nextProps.customerInfo !== customerInfo
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

    render() {
        const {
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
            handleOpenIPInfo } = this.props;

        const { redirectToBalanceInfoPage, redirectToFeatureList } = this.state;

        const contentItems = BuildContentItemsMobile(
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
            redirectToBalanceInfoPage,
            redirectToFeatureList);

        const animationProps = { addAnimation: false };
        return (
            <DebugRouter>
                <Switch>
                    <Route
                        key={0}
                        exact path="/featureList"
                        render={
                            (props) => <FeatureListPage
                                contentItems={contentItems}
                                {...{ addAnimation: props.location.state.addAnimation }}
                            />
                        }
                    />

                    <Route key={1} exact path="/contactInfo" render={(props) => <MobilePage {...contentItems.contactInfoProps} />} />

                    <Route
                        key={2}
                        exact path="/balanceInfo"
                        render={
                            (props) => <MobilePage
                                {...contentItems.balanceInfoProps}
                                {...{ addAnimation: props.location.state.addAnimation }}
                            />
                        }
                    />
                    <Route
                        key={3}
                        exact path="/downlineList"
                        render={
                            (props) => <MobilePage {...contentItems.downlineListProps} />
                        }
                    />
                    <Route key={4} exact path="/status" render={(props) => <MobilePage {...contentItems.statusProps} />} />
                    <Route key={5} exact path="/settings" render={(props) => <MobilePage {...contentItems.settingProps} />} />
                    <Route key={6} exact path="/loginInfo" render={(props) => <MobilePage {...contentItems.loginInfoProps} />} />
                    <Route key={6} exact path="/onlineList" render={(props) => <MobilePage {...contentItems.onlineListProps} />} />
                    <Route
                        key={7}
                        exact path="/"
                        render={
                            (props) => customerInfo !== null ?
                                <MobilePage {...contentItems.balanceInfoProps} {...animationProps} /> :
                                <FeatureListPage contentItems={contentItems} {...animationProps} />
                        }
                    />
                </Switch>
            </DebugRouter>
        );
    }
}