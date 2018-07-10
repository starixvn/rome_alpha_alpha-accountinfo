import * as React from 'react';
import Button from '../common/button';

//import RowInfo from './row-info';
//import RowInfoModel from '.../models/row-info-model';

//import CellModel from '../models/cell-model';
import Cell from '../common/cell';
import CellContainLink from '../common/cell-contain-link';
import Search from './search';

interface IAccountInfoProps {
}

interface IAccountInfoState {
}

export default class AccountInfoTab extends React.Component<IAccountInfoProps, IAccountInfoState> {
    constructor(props) {
        super(props);
    }

    handleBetListClick = () => { }

    handleStatementClick = () => { }

    render() {
        const rowClasses = "col-xs-12 row-info";
        const firstCellClass = "col-xs-5";
        const secondCellClass = "col-xs-7 col-right";

        return (
            <div className="block-info" id="blockInfo">
                <Search username="Customer01" />

                <div className={rowClasses}>
                    <Cell text="Master" classnames={firstCellClass} />
                    <Cell text="Master01" classnames={secondCellClass + " font-weight-normal"}/>
                </div>

                <div className={rowClasses}>
                    <Cell text="Agent" classnames={firstCellClass} />
                    <CellContainLink text="Agent01"
                        classnames={secondCellClass + " font-weight-normal"}
                        hrefLink="#" />
                </div>

                <div className={rowClasses + " highlight"}>
                    <Cell text="Customer" classnames={firstCellClass} />
                    <CellContainLink text="Agent01"
                        classnames={secondCellClass}
                        hrefLink="#"
                        iconClassname="icon-viewlog" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Name" classnames={firstCellClass} />
                    <Cell text="Thien Nguyen" classnames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Phone" classnames={firstCellClass} />
                    <Cell text="08 0123 456" classnames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Fax" classnames={firstCellClass} />
                    <Cell text="08 0123 456" classnames={secondCellClass} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Fax" classnames={firstCellClass} />
                    <Cell text="08 0123 456" classnames={secondCellClass} />
                </div>

                <div className={rowClasses + " text-center list-button"}>
                    <Button text="Bet List" classnames="btn-default with-level" onClick={this.handleBetListClick} />
                    <Button text="Statement" classnames="btn-default with-level" onClick={this.handleStatementClick} />
                </div>
            </div>
        );
    }
}