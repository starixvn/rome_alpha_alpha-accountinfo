import * as React from 'react';
import { Link } from 'react-router-dom';
import { Redirect } from 'react-router-dom';

export const HeaderFeature = ({ url, iconClass, featureText, onClick = null, redirectToBalanceInfoPage}) => {
    if (redirectToBalanceInfoPage) {
        return <Redirect to='/balanceInfo' />;
    }

    return <Link to={url} className="feature-item" onClick={onClick}>
        <span className={iconClass} />
        {featureText}
    </Link>;
};