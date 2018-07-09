import * as React from 'react';
import Link from './link';
import Cell from './cell';

interface ICellWithLinkProps {
    classnames?: string;
    hrefLink?: string;
    text: string
}

interface ICellWithLinkState {
    text: string
}

export default class CellWithLink extends React.Component<ICellWithLinkProps, ICellWithLinkState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={this.props.classnames}>
                <Link text={this.props.text} url={this.props.hrefLink} />
            </div>
      );
    }
}