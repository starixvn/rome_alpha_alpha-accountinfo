import React from 'react';

export default class CustomAnchorTag extends React.Component {
    constructor(props) {
        super(props);
        this.handleOnclick = this.handleOnclick.bind(this);
    }

    handleOnclick(e) {
        if (this.props.mobileView) {
            window.parent.document.getElementById('closeIconOnMobile').click();
        }
    }

    render() {
        const { title = null, href, classNames = '', children } = this.props;
        return (
            <a
                title={title}
                className={classNames}
                href={href}
                target="main"
                onClick={this.handleOnclick}
            >
                {children}
            </a>
        );
    }
}
