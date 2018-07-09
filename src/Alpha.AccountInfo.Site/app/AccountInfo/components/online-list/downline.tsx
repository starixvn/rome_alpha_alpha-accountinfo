import * as React from 'react';

interface IDownlineProps {
    username: string
}

interface IDownlineState {

}

export default class Downline extends React.Component<IDownlineProps, IDownlineState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel-body">
                {this.props.username}
            </div>
        );
    }
}