import * as React from 'react';
import Button from '../common/button';

//import RowInfo from './row-info';
//import RowInfoModel from '.../models/row-info-model';

//import CellModel from '../models/cell-model';
import Cell from '../common/cell';
import CellWithLink from '../common/cell-contain-link';

interface IAccountInfoProps {
}

interface IAccountInfoState {
}

export default class AccountInfoTab extends React.Component<IAccountInfoProps, IAccountInfoState> {
    constructor(props) {
        super(props);
    }

    handleBetListClick = () =>
    {
        
    }

    handleStatementClick = () => {

    }

    render() {
        return (
            <div className="block-info" id="blockInfo">
                  <div className="search-box">
                    <input type="text" className="txt-search" placeholder="Username" value="Customer01"/>
                        <span className="icon-search"></span>
                </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Master" classnames="col-xs-5" />
                    <Cell text="Master01" classnames="col-xs-7 col-right font-weight-normal" />
                </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Agent" classnames="col-xs-5" />
                    <CellWithLink text="Agent01" classnames="col-xs-7 col-right font-weight-normal" hrefLink="#" />
                </div>

                    <div className="col-xs-12 row-info highlight">
                        <div className="col-xs-5">Customer</div>
                        <div className="col-xs-7 col-right">
                            <a href="#">Customer01</a>
                            <span className="icon-viewlog"></span>
                        </div>
                    </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Name" classnames="col-xs-5" />
                    <Cell text="Thien Nguyen" classnames="col-xs-7 col-right" />
                </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Phone" classnames="col-xs-5" />
                    <Cell text="08 0123 456" classnames="col-xs-7 col-right" />
                </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Fax" classnames="col-xs-5" />
                    <Cell text="08 0123 456" classnames="col-xs-7 col-right" />
                </div>

                <div className="col-xs-12 row-info">
                    <Cell text="Fax" classnames="col-xs-5" />
                    <Cell text="08 0123 456" classnames="col-xs-7 col-right" />
                </div>

                <div className="col-xs-12 row-info text-center list-button">
                    <Button text="Bet List" classnames="btn-default with-level" onClick={this.handleBetListClick} />
                    <Button text="Statement" classnames="btn-default with-level" onClick={this.handleStatementClick} />
                    </div>
            </div> 
      );
    }
}