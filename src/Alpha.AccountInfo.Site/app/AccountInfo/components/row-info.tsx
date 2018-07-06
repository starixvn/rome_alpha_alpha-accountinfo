import * as React from 'react';
import Cell from './cell';
import RowInfoModel from '../models/row-info-model';

interface IRowInfoProps {
    classnames: string,
    rowInfoModel: RowInfoModel
}

interface IRowInfoState {
    rowInfoModel: RowInfoModel
}

export default class RowInfo extends React.Component<IRowInfoProps, IRowInfoState> {
    constructor(props) {
        super(props);
        this.state = {
            rowInfoModel: this.props.rowInfoModel
        };
    }
    render() {
        return (
            <div className={this.props.classnames}>
                <Cell cellModel={this.state.rowInfoModel.key} />
                <Cell cellModel={this.state.rowInfoModel.value}/> 
            </div>

        )
    }
}