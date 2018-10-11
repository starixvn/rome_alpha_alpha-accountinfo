import React from 'react';
import { Link, Redirect } from 'react-router-dom';
import { HeaderFeature } from './header-feature';

export default class MobilePage extends React.Component {
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

        return (
            <div className={`${this.state.animationClass} feature-detail`}>
                <HeaderFeature
                    url="/featureList"
                    iconClass={iconClass}
                    featureText={featureText}
                    redirectToBalanceInfoPage={redirectToBalanceInfoPage}
                />
                {data === null ? noInfo : children}
            </div>
        );
    }
}
