import React from 'react';
import { Link, Redirect } from 'react-router-dom';

const CustomLink = ({ path, iconClass, featureText }) => {
    return (
        <Link
            to={{
                pathname: path,
                state: { addAnimation: true }
            }}
            className="feature-item"
        >
            <span className={iconClass} />
            {featureText}
        </Link>
    );
}

const ContentPage = ({ iconClass, featureText, children, animationClass }) => {
    let customLinkProps = {
        path: "/featureList",
        iconClass: iconClass,
        featureText: featureText
    };

    return (
        <div className={`${animationClass} feature-detail`}>
            <CustomLink {...customLinkProps}/>
            {children}
        </div>
    );
}

const FeatureListPage = ({ contentItems, animationClass }) => {
    return (
        <div className={`${animationClass} feature-list`}>
            <nav>
                {
                    Object.values(contentItems).map((content, index) => {
                        let customLinkProps = {
                            path: content.path,
                            iconClass: content.iconClass,
                            featureText: content.featureText
                        };

                        return content.visible && <CustomLink {...customLinkProps} key={index} />;
                    })
                }
            </nav>
        </div>
    );
}

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

        const { featureListPath, balanceInfoPath, animationClass } = this.state;

        if (redirectToBalanceInfoPage || redirectToFeatureList) {
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
            <ContentPage
                iconClass={iconClass}
                animationClass={animationClass}
                featureText={featureText}
                children={children}
            /> :
            <FeatureListPage
                contentItems={contentItems}
                animationClass={animationClass}
            />;
    }
}