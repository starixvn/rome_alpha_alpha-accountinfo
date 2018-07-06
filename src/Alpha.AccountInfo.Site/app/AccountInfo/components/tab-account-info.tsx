import * as React from 'react';
import Button from './button';
import RowInfo from './row-info';
import RowInfoModel from '../models/row-info-model';
import CellModel from '../models/cell-model';

interface IAccountInfoProps {
}

interface IAccountInfoState {
}

export default class AccountInfoTab extends React.Component<IAccountInfoProps, IAccountInfoState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="block-info" id="blockInfo">
                  <div className="search-box">
                    <input type="text" className="txt-search" placeholder="Username" value="Customer01"/>
                        <span className="icon-search"></span>
                </div>
                <RowInfo
                    classnames="col-xs-12 row-info"
                    rowInfoModel={
                        {
                            key: { text: 'Master', classnames: 'col-xs-5' },
                            value: { text: 'Master01', classnames: 'col-xs-7 col-right font-weight-normal' }
                        } as RowInfoModel} />
                <RowInfo
                    classnames="col-xs-12 row-info"
                    rowInfoModel={
                        {
                            key: { text: 'Agent', classnames: 'col-xs-5' },
                            value: { text: 'Agent01', classnames: 'col-xs-7 col-right font-weight-normal', hrefLink: '#' } as CellModel
                        } as RowInfoModel} />

                    <div className="col-xs-12 row-info highlight">
                        <div className="col-xs-5">Customer</div>
                        <div className="col-xs-7 col-right">
                            <a href="#">Customer01</a>
                            <span className="icon-viewlog"></span>
                        </div>
                    </div>

                <RowInfo
                    classnames="col-xs-12 row-info"
                    rowInfoModel={
                        {
                            key: { text: 'Name', classnames: 'col-xs-5' },
                            value: { text: 'Thien Nguyen', classnames: 'col-xs-7 col-right' }
                        } as RowInfoModel} />

                <RowInfo
                    classnames="col-xs-12 row-info"
                    rowInfoModel={
                        {
                            key: { text: 'Phone', classnames: 'col-xs-5' },
                            value: { text: '08 0123 456', classnames: 'col-xs-7 col-right' }
                        } as RowInfoModel} />


                <RowInfo
                    classnames="col-xs-12 row-info"
                    rowInfoModel={
                        {
                            key: { text: 'Fax', classnames: 'col-xs-5' },
                            value: { text: '08 0123 456', classnames: 'col-xs-7 col-right' }
                        } as RowInfoModel} />

                    <div className="col-xs-12 row-info text-center list-button">
                        <Button text="Bet List" classnames="btn-default with-level" />
                        <Button text="Statement" classnames="btn-default with-level" />
                    </div>
            </div> 
      );
    }
}