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
            contentItems = [],
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

        let buildLink = (path, iconClass, featureText, index) => <Link
            to={{
                pathname: `/${path}`,
                state: { addAnimation: true }
            }}
            className="feature-item"
            key={index}
        >
            <span className={iconClass} />
            {featureText}
        </Link>;

        return contentItems.length === 0 ?
            <div className={`${this.state.animationClass} feature-detail`}>
                {buildLink("featureList", iconClass, featureText)}
                {children}
            </div> :
            <div className={`${this.state.animationClass} feature-list`}>
                <nav>
                    {
                        Object.values(contentItems).map((header, index) => {
                            return header.visible &&
                                buildLink(
                                    header.path,
                                    header.iconClass,
                                    header.featureText,
                                    index);
                        })
                    }
                </nav>
            </div>;
    }
}
