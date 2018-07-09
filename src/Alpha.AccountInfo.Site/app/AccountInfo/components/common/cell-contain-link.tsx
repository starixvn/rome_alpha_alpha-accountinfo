import * as React from 'react';
import Link from './link';
import Cell from './cell';

interface ICellContainLinkProps {
    classnames?: string;
    hrefLink?: string;
    text: string
}

interface ICellContainLinkState {
    text: string
}

export default class CellContainLink extends React.Component<ICellContainLinkProps, ICellContainLinkState> {
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