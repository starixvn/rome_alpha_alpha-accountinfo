import * as React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Switch, Route } from 'react-router-dom';
import MobilePage from './mobile-page';
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
        const { contents, resources, customerInfo } = this.props;
        const { redirectToBalanceInfoPage, redirectToFeatureList } = this.state;
        const animationProps = { addAnimation: false };

        let contentItems = [
            {
                contentItems: contents,
                path: "featureList"
            },
            { ...contents.contactInfoProps },
            { ...contents.balanceInfoProps },
            { ...contents.downlineListProps },
            { ...contents.statusProps },
            { ...contents.settingProps },
            { ...contents.loginInfoProps },
            { ...contents.onlineListProps },
            {
                ...animationProps,
                ...contents.balanceInfoProps,
                path: "",
                contentItems: customerInfo !== null ? [] : contents
            }
        ];

        return (
            <DebugRouter>
                <Switch>
                    {
                        contentItems.map((route, index) => {
                            return <Route
                                key={index}
                                exact path={`/${route.path}`}
                                render={
                                    (props) => <MobilePage
                                        {...route}
                                        {...{ addAnimation: props.location.state !== undefined ? props.location.state.addAnimation : false }}
                                        redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                                        redirectToFeatureList={redirectToFeatureList}
                                    />
                                }
                            />;
                        })
                    }
                </Switch>
            </DebugRouter>
        );
    }
}