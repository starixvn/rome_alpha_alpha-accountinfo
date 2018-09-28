import * as React from 'react';
import { UserLevel } from '../../constants/user-level';
import { HeaderFeature } from './header-feature';
import { Redirect } from 'react-router-dom';

export default class AccountInfoMobile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            firstLoad: true,
            searchAccountRoleId: this.props.actorRoleId - 1
        };
    }

    componentWillReceiveProps(nextProps) {
        let { searchAccountId } = this.props;
        let { firstLoad } = this.state;
        if (firstLoad && nextProps.searchAccountId !== searchAccountId) {
            this.setState({
                firstLoad: false,
                searchAccountRoleId: this.props.actorRoleId - 1
            });
        }
    }

    render() {
        let { resources, actorRoleId, redirectToBalanceInfoPage } = this.props;
        if (redirectToBalanceInfoPage) {
            return <Redirect to='/balanceInfo' />;
        }

        let { firstLoad, searchAccountRoleId } = this.state;

        // TODO: first load feature list css 
        return (
            <div className={`${firstLoad ? "" : ""} feature-list`}> 
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
