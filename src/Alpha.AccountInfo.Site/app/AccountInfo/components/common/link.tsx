import * as React from 'react';

interface ILinkProps {
    url: string,
    text:string
}


export default class Link extends React.Component<ILinkProps, {}> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <a href={this.props.url}>{this.props.text}</a>
      );
    }
}