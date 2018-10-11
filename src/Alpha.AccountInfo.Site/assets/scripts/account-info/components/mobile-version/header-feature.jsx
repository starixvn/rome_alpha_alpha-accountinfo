import * as React from 'react';
import { Link } from 'react-router-dom';
import { Redirect } from 'react-router-dom';

export const HeaderFeature = ({ url, iconClass, featureText, redirectToBalanceInfoPage = false }) => {
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

    return (
        <Link
            to={{
                pathname: url,
                state: { addAnimation: true }
            }}
            className="feature-item"
        >
            <span className={iconClass} />
            {featureText}
        </Link>
    );
};