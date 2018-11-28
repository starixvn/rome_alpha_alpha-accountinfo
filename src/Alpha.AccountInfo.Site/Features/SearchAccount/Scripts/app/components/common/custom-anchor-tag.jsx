import React from 'react';
import { closeLeftPanelOnMobile } from '../../helpers/accountinfo-helper';

export default class CustomAnchorTag extends React.Component {
    constructor(props) {
        super(props);
        this.handleOnclick = this.handleOnclick.bind(this);
    }

    handleOnclick() {
        if (this.props.mobileView) {
            closeLeftPanelOnMobile();
        }
    }

    render() {
        const { title = null, href, classNames = '', children } = this.props;
        return (
            <a {...{
                title,
                href,
                target: 'main',
                className: classNames ,
                onClick: this.handleOnclick}}
            >
                {children}
            </a>
        );
    }
}
