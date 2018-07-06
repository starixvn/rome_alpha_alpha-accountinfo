import * as React from 'react';
import CellModel from '../models/cell-model';
import Link from './link';

interface ICellProps {
    cellModel: CellModel
}

interface ICellState {
    text: string
}

export default class Cell extends React.Component<ICellProps, ICellState> {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.cellModel.text || ''
        };
    }
    render() {
        const hrefElm = this.props.cellModel.hrefLink != null? <Link text={this.props.cellModel.text} url={this.props.cellModel.hrefLink} /> :this.state.text;
        return (
            <div className={this.props.cellModel.classnames}>{hrefElm}</div>
      );
    }
}