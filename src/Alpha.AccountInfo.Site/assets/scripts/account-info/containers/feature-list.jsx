import * as React from 'react';
import { Link } from 'react-router-dom';
import { UserLevel } from '../constants/user-level';

const FeatureList = ({
    resources,
    actorRoleId,
    searchAccountRoleId,
    getCustomerAllStatus,
    fetchDownlineListFromServer,
    fetchOnlineListFromServer
}) => <div className="feature-list">
        <nav>
            <Link to="/contactInfo" className="feature-item">
                <span className="icon-contact-info" />
                {resources.ShowContactInfo}
            </Link>

            <Link to="/balanceInfo" className="feature-item">
                <span className="icon-balance" />
                {resources.BalanceInfo}
            </Link>

            {
                actorRoleId > UserLevel.Agent && searchAccountRoleId > UserLevel.Member &&
                <Link to="/downlineList" className="feature-item" onClick={fetchDownlineListFromServer}>
                    <span className="icon-downline" />
                    {resources.DownlineList}
                </Link>
            }

            <Link to="/status" className="feature-item" onClick={getCustomerAllStatus}>
                <span className="icon-status" />
                {resources.Status}
            </Link>

            <Link to="/settings" className="feature-item" onClick={getCustomerAllStatus}>
                <span className="icon-settings" />
                {resources.Setting}
            </Link>

            <Link to="/loginInfo" className="feature-item">
                <span className="icon-login" />
                {resources.LoginInfo}
            </Link>

            <Link to="/onlineList" className="feature-item" onClick={fetchOnlineListFromServer}>
                <span className="icon-onlinelist" />
                {resources.OnlineList}
            </Link>
        </nav>
    </div>;

export default FeatureList;