import React from 'react';
import { Link, Redirect } from 'react-router-dom';

export default class ContentMobile extends React.Component {
    constructor(props) {
        super(props);
        let { addAnimation = true } = this.props;
        this.state = {
            animationClass: addAnimation ? 'translate-from-left' : '',
            featureListPath: '/featureList',
            balanceInfoPath: '/balanceInfo'
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

    shouldComponentUpdate(props, state) {
        let { redirectToBalanceInfoPage, redirectToFeatureList, path } = props;
        let { featureListPath, balanceInfoPath } = state;

        let balanceInfoRedirectBackToItself = path === balanceInfoPath && redirectToBalanceInfoPage;
        let featureListRedirectBackToItself = path === featureListPath && redirectToFeatureList;

        return !balanceInfoRedirectBackToItself && !featureListRedirectBackToItself;
    }

    buildLink(path, iconClass, featureText, index) {
        return (
            <Link
                to={{
                    pathname: path,
                    state: { addAnimation: true }
                }}
                className="feature-item"
                key={index}
            >
                <span className={iconClass} />
                {featureText}
            </Link>
        );
    }

    buildFeatureList(contentItems) {
        return (
            <div className={`${this.state.animationClass} feature-list`}>
                <nav>
                    {
                        Object.values(contentItems).map((content, index) => {
                            return content.visible &&
                                this.buildLink(
                                    content.path,
                                    content.iconClass,
                                    content.featureText,
                                    index);
                        })
                    }
                </nav>
            </div>
        );
    }

    buildContentBlock(iconClass, featureText, children) {
        return (
            <div className={`${this.state.animationClass} feature-detail`}>
                {this.buildLink("featureList", iconClass, featureText)}
                {children}
            </div>
        );
    }

    render() {
        const {
            iconClass,
            featureText,
            data,
            children,
            contentItems = [],
            redirectToBalanceInfoPage = false,
            redirectToFeatureList = false
        } = this.props;

        if (redirectToBalanceInfoPage || redirectToFeatureList) {
            let { featureListPath, balanceInfoPath } = this.state;
            return (
                <Redirect
                    to={{
                        pathname: redirectToBalanceInfoPage ? balanceInfoPath : featureListPath,
                        state: { addAnimation: false }
                    }}
                />
            );
        }

        return contentItems.length === 0 ?
            this.buildContentBlock(iconClass, featureText, children) :
            this.buildFeatureList(contentItems);
    }
}