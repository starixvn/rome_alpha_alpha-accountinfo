import React from 'react';
import { closeLeftPanelOnMobile } from '../../helpers/accountinfo-helper';

export default class CustomAnchorTag extends React.Component {
    constructor(props) {
        super(props);
        this.handleOnclick = this.handleOnclick.bind(this);
    }

    handleOnclick(e) {
        e.preventDefault();
        if (this.props.mobileView) {
            closeLeftPanelOnMobile();
        }

        const mainFrame = window.top.$('#main');
        if (!window.top.alpha.core.isIe()) {
            mainFrame.contents().find('head').append($('#AssetCommonCssLink').val());
            mainFrame.contents().find('body').empty().block();
        }

        mainFrame.attr('src', this.props.href);
    }

    render() {
        const { title = null, classNames = '', children } = this.props;
        return (
            <a {...{
                title,
                href: '#',
                target: 'main',
                className: classNames ,
                onClick: this.handleOnclick}}
            >
                {children}
            </a>
        );
    }
}
