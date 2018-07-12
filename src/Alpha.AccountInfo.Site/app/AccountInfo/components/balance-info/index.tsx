import * as React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import BlockHeading from '../common/block-heading';


interface IBalanceInfoTabProps {
}

interface IBalanceInfoTabState {
}

export default class BalanceInfoBlock extends React.Component<IBalanceInfoTabProps, IBalanceInfoTabState> {
    constructor(props) {
        super(props);
    }

    handleCancelClick = () => { }

    handleSubmitClick = () => { }

    handleOutStandingChange = () => { }

    handleNewGivenCredit = () => { }

    handleGivenCredit = () => { }

    render() {
        const rowClasses = "col-xs-12 row-info";

        return (
            <div className="panel panel-default">
                <BlockHeading text="Balance Info"
                    id="headingBalanceInfo"
                    iconClassname="icon-balance"
                    tabHref="collapseBalanceInfo" />

                <div id="collapseBalanceInfo"
                    className="panel-collapse collapse in"
                    role="tabpanel"
                    aria-labelledby="headingBalanceInfo">

                    <div className="panel-body">
                        <div className={rowClasses}>
                            <Cell text="Total Cash" classNames="col-xs-6" /> 
                            <CellContainNumberFormat text={-31931.68} classNames="col-xs-6 col-right" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Yesterday Cash" classNames="col-xs-6" />
                            <CellContainNumberFormat text={-31291.44} classNames="col-xs-6 col-right" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Total Balance" classNames="col-xs-6" />
                            <CellContainNumberFormat text={-27715.24} classNames="col-xs-6 col-right" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Yesterday Balance" classNames="col-xs-6" />
                            <CellContainNumberFormat text={-28496.39} classNames="col-xs-6 col-right" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Outstanding" classNames="col-xs-6" />
                            <CellContainInput text={1159.70}
                                classNames="col-xs-6 col-right box-edited"
                                inputClassname="txt-number-edited"
                                onSave={this.handleOutStandingChange} />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Used Credit" classNames="col-xs-6" />
                            <CellContainNumberFormat text={2670903.00} classNames="col-xs-6 col-right" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Given Credit" classNames="col-xs-6" />
                            <CellContainInput text={300000.00}
                                id="txt-given-edit"
                                classNames="col-xs-6 col-right box-edited"
                                inputClassname="txt-number-edited"
                                onSave={this.handleGivenCredit}
                                iconClassname="icon-border-color icon-edited" />

                            <div id="popup-confirm-given-credit" className="popup-confirm">
                                <h1 className="title">Given Credit</h1>

                                <div className={rowClasses}>
                                    <Cell text="Current Given Credit" classNames="col-xs-7" />
                                    <Cell text="300,000,000.00" classNames="col-xs-5 show-value" />
                                </div>

                                <div className={rowClasses}>
                                    <Cell text="New Given Credit" classNames="col-xs-7" />
                                    <CellContainInput text={350000000.00}
                                        classNames="col-xs-5 text-right"
                                        inputClassname="hasBorder txt-newValue"
                                        onSave={this.handleNewGivenCredit} />
                                </div>

                                <div className={rowClasses}>
                                    <Cell text="Difference" classNames="col-xs-7" />
                                    <Cell text="50,000,000.00" classNames="col-xs-5 show-value" />
                                </div>

                                <div className="col-xs-12 row-info popup-bottom list-button">
                                    <Button text="Cancel" classNames="btn-cancel" onClick={this.handleCancelClick} />
                                    <Button text="Submit" classNames="btn-submit" onClick={this.handleSubmitClick} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      );
    }
}