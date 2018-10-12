import * as React from 'react';
import { Link, Redirect } from 'react-router-dom';

export default class FeatureListPage extends React.Component {
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
        let { contentItems } = this.props;

        if (contentItems.featureListProps.redirectToBalanceInfoPage) {
            return (
                <Redirect
                    to={{
                        pathname: "/balanceInfo",
                        state: { addAnimation: false }
                    }}
                />
            );
        }

        return (
            <div className={`${this.state.animationClass} feature-list`}>
                <nav>
                    {
                        Object.values(contentItems).map((header, index) => {
                            return header.visible &&
                                <Link
                                    to={{
                                        pathname: header.url,
                                        state: { addAnimation: true }
                                    }}
                                    className="feature-item"
                                    key={index}
                                >
                                    <span className={header.iconClass} />
                                    {header.featureText}
                                </Link>;
                        })
                    }
                </nav>
            </div>
        );
    }
}