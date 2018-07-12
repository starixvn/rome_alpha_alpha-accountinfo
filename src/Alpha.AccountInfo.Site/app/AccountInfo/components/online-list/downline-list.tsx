import * as React from 'react';
import Downline from './downline';

interface IDownlineListProps {
    usernames: string[]
}

interface IDownlineListState {
}

export default class DownlineList extends React.Component<IDownlineListProps, IDownlineListState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div >
                {this.props.usernames.map((username, index) => <Downline username={username} key={index}/>)}
            </div>
        );
    }
}