import React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { HeaderFeature } from './header-feature';

export default class MobilePage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            animationClass: ''
        };
    }

    render() {
        const {
            iconClass,
            featureText,
            noInfoText,
            data,
            redirectToBalanceInfoPage = false,
            children } = this.props;

        const noInfo =
            <div className="no-information">
                {noInfoText}
            </div>;

        return <div className="feature-detail translate-from-left">
            <HeaderFeature url="/featureList"
                iconClass={iconClass}
                featureText={featureText}
                redirectToBalanceInfoPage={redirectToBalanceInfoPage}
            />
            {data === null ? noInfo : children}
        </div>;

    }
}
