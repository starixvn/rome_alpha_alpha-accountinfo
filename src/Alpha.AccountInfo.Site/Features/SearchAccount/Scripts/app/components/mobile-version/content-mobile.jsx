import React from 'react';
import { Link, Redirect } from 'react-router-dom';

export const CustomLink = ({ pathname, iconClass, featureText, backText = '' }) =>
    <Link
        id={`nav-${iconClass}`}
        to={{
            pathname,
            state: { addAnimation: true }
        }}
        className="feature-item"
    >
        <span className={iconClass} />
        {featureText}
        <span className="feature-item-nav">{backText}</span>
    </Link>;

export const ContentPage = ({
    iconClass,
    featureText,
    children,
    animationClass,
    fetchDataFromServer,
    backText
}) => {
    if (fetchDataFromServer !== undefined) {
        fetchDataFromServer();
    }

    return (
        <div className={`${animationClass} feature-detail`}>
            <CustomLink {... { iconClass, featureText, pathname: '/featureList', backText }} />
            {children}
        </div>
    );
};

export const FeatureListPage = ({ contentItems, animationClass, handleOpenShowQRCode, showQRCodeUrl, resources }) =>
    <div className={`${animationClass} feature-list`}>
        <nav>
            {
                Object.values(contentItems).map((content, index) => content.visible && <CustomLink {...content} key={index} />)
            }
        </nav>
        {
            showQRCodeUrl !== '' && showQRCodeUrl !== undefined &&
            <nav>
                <div className='accordion-item'>
                    <div className='feature-item-qr' onClick={() => { handleOpenShowQRCode(showQRCodeUrl) }}>
                        <span className='icon-qr-code'></span>{resources.FirstLoginQRCode}
                    </div>
                </div>
            </nav>
        }
    </div>;

export default class ContentMobile extends React.Component {
    constructor(props) {
        super(props);
        const { addAnimation = true } = this.props;
        this.state = {
            animationClass: addAnimation ? 'translate-from-left' : '',
            featureListPath: '/featureList',
            balanceInfoPath: '/balanceInfo'
        };
    }

    componentDidMount() {
        const self = this;
        if (this.state.animationClass !== '') {
            setTimeout(() => {
                self.setState({ animationClass: '' });
            }, 300);
        }
    }

    shouldComponentUpdate(props, state) {
        const { redirectToBalanceInfoPage, redirectToFeatureList, path } = props;
        const { featureListPath, balanceInfoPath } = state;

        const balanceInfoRedirectBackToItself = path === balanceInfoPath && redirectToBalanceInfoPage;
        const featureListRedirectBackToItself = path === featureListPath && redirectToFeatureList;

        return !balanceInfoRedirectBackToItself && !featureListRedirectBackToItself;
    }

    render() {
        const {
            iconClass,
            featureText,
            children,
            fetchDataFromServer,
            contentItems = [],
            handleOpenShowQRCode,
            showQRCodeUrl,
            redirectToBalanceInfoPage = false,
            redirectToFeatureList = false,
            backText,
            resources
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
            <ContentPage {...{ iconClass, animationClass, featureText, children, fetchDataFromServer, backText }} /> :
            <FeatureListPage {...{ resources, contentItems, animationClass, handleOpenShowQRCode, showQRCodeUrl }} />;
    }
}