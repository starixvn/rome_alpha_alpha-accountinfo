import * as React from 'react';

interface IDownlineProps {
    username: string
}

export default class Downline extends React.Component<IDownlineProps, {}> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="two-col">
                <a href="#">{this.props.username}</a>
            </div>
        );
    }
}