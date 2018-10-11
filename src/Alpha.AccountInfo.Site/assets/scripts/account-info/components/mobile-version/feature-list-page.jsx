import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import { HeaderFeature } from './header-feature';
import { Redirect } from 'react-router-dom';

export default class FeatureListPage extends React.Component {
    constructor(props) {
        super(props);
        let { addAnimation = true } = this.props;
        this.state = {
            animationClass: addAnimation ? 'translate-from-left' : ''
        };
    }
    componentDidMount() {
        let self = this;
        if (this.state.animationClass !== '') {
            setTimeout(() => {
                self.setState({ animationClass: '' });
            }, 300);
        }
    }

    render() {
        let {
            resources,
            customerInfo,
            actorRoleId,
            redirectToBalanceInfoPage
        } = this.props;

        if (redirectToBalanceInfoPage) {
            return (
                <Redirect
                    to={{
                        pathname: "/balanceInfo",
                        state: { addAnimation: false }
                    }}
                />
            );
        }

        let searchAccountRoleId = customerInfo !== null ? customerInfo.RoleId : actorRoleId - 1;
        return (
            <div className={`${this.state.animationClass} feature-list`}>
                <nav>
                    <HeaderFeature url="/contactInfo" iconClass="icon-contact-info" featureText={resources.ShowContactInfo} />
                    <HeaderFeature url="/balanceInfo" iconClass="icon-balance" featureText={resources.BalanceInfo} />
                    {
                        actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member &&
                        <HeaderFeature url="/downlineList" iconClass="icon-downline" featureText={resources.DownlineList} />

                    }
                    <HeaderFeature url="/status" iconClass="icon-status" featureText={resources.Status} />
                    <HeaderFeature url="/settings" iconClass="icon-settings" featureText={resources.Setting} />
                    <HeaderFeature url="/loginInfo" iconClass="icon-login" featureText={resources.LoginInfo} />
                    <HeaderFeature url="/onlineList" iconClass="icon-onlinelist" featureText={resources.OnlineList} />
                </nav>
            </div>
        );
    }
}