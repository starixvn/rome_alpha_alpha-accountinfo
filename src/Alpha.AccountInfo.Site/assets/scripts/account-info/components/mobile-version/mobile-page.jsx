import React from 'react';
import { Link, Redirect } from 'react-router-dom';

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
            data,
            children,
            redirectToBalanceInfoPage = false,
            redirectToFeatureList = false } = this.props;

        if (redirectToBalanceInfoPage || redirectToFeatureList) {
            return (
                <Redirect
                    to={{
                        pathname: redirectToBalanceInfoPage ? "/balanceInfo" : "/featureList",
                        state: { addAnimation: false }
                    }}
                />
            );
        }

        return (
            <div className={`${this.state.animationClass} feature-detail`}>
                <Link
                    to={{
                        pathname: "/featureList",
                        state: { addAnimation: true }
                    }}
                    className="feature-item"
                >
                    <span className={iconClass} />
                    {featureText}
                </Link>
                {children}
            </div>
        );
    }
}
