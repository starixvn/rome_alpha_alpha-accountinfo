import React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item';

import Cash from './cash';

export default class BalanceInfoContent extends React.Component{
    constructor(props) {
        super(props);
    }

    handleCancelClick () { }

    handleSubmitClick ()  { }

    handleOutStandingChange () { }

    handleNewGivenCredit () { }

    handleGivenCredit () { }

    render() {
        const rowClasses = "col-xs-12 row-info";
        //<div id="popup-confirm-given-credit" className="popup-confirm">
        //    <h1 className="title">Given Credit</h1>

        //    <div className={rowClasses}>
        //        <Cell title="Current Given Credit" classNames="col-xs-7" />
        //        <Cell title={this.props.balanceInfo.CurrentGivenCredit.toLocaleString('en-us', { minimumFractionDigits: 2 })} classNames="col-xs-5 show-value" />
        //    </div>

        //    <div className={rowClasses}>
        //        <Cell title="New Given Credit" classNames="col-xs-7" />
        //        <CellContainInput title={this.props.balanceInfo.NewGivenCredit}
        //            classNames="col-xs-5 text-right"
        //            inputClassname="hasBorder txt-newValue"
        //            onSave={this.handleNewGivenCredit} />
        //    </div>

        //    <div className={rowClasses}>
        //        <Cell title="Difference" classNames="col-xs-7" />
        //        <Cell title={this.props.balanceInfo.Difference.toLocaleString('en-us', { minimumFractionDigits: 2 })} classNames="col-xs-5 show-value" />
        //    </div>

        //    <div className="col-xs-12 row-info popup-bottom list-button">
        //        <Button title="Cancel" classNames="btn-cancel" onClick={this.handleCancelClick} />
        //        <Button title="Submit" classNames="btn-submit" onClick={this.handleSubmitClick} />
        //    </div>
        //</div>
        return (
            <div>
                {
                    this.props.roleId > 1
                    && <Cash TotalCash={this.props.balanceInfo.TotalCash} YesterdayCash={this.props.balanceInfo.YesterdayCash} />
                }

                <div className={rowClasses}>
                    <Cell title="Total Balance" classNames="col-xs-6" />
                    <CellContainNumberFormat title={this.props.balanceInfo.TotalBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell title="Yesterday Balance" classNames="col-xs-6" />
                    <CellContainNumberFormat title={this.props.balanceInfo.YesterdayBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell title="Outstanding" classNames="col-xs-6" />
                    <CellContainInput title={this.props.balanceInfo.Outstanding}
                        classNames="col-xs-6 col-right box-edited"
                        inputClassname="txt-number-edited"
                        onSave={this.handleOutStandingChange} />
                </div>

                <div className={rowClasses}>
                    <Cell title="Used Credit" classNames="col-xs-6" />
                    <CellContainNumberFormat title={this.props.balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell title="Given Credit" classNames="col-xs-6" />
                    <CellContainInput title={this.props.balanceInfo.GivenCredit}
                        id="txt-given-edit"
                        classNames="col-xs-6 col-right box-edited"
                        inputClassname="txt-number-edited"
                        onSave={this.handleGivenCredit}
                        iconClassname="icon-border-color icon-edited" /> 
                </div>
            </div>
      );
    }
}