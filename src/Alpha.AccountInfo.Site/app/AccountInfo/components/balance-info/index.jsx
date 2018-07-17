import React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import CustomizedAccordionItem from '../common/customized-accordion-item';

export default class BalanceInfoBlock extends React.Component{
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
        const { headingText, iconClassname } = this.props; 

        return (
            <CustomizedAccordionItem title="Balance Info" iconClassname="icon-balance" accordionItemBodyClassName="accordion__body balance-info">
                <div className={rowClasses}>
                    <Cell text="Total Cash" classNames="col-xs-6" />
                    <CellContainNumberFormat text={this.props.balanceInfo.TotalCash} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Yesterday Cash" classNames="col-xs-6" />
                    <CellContainNumberFormat text={this.props.balanceInfo.YesterdayCash} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Total Balance" classNames="col-xs-6" />
                    <CellContainNumberFormat text={this.props.balanceInfo.TotalBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Yesterday Balance" classNames="col-xs-6" />
                    <CellContainNumberFormat text={this.props.balanceInfo.YesterdayBalance} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Outstanding" classNames="col-xs-6" />
                    <CellContainInput text={this.props.balanceInfo.Outstanding}
                        classNames="col-xs-6 col-right box-edited"
                        inputClassname="txt-number-edited"
                        onSave={this.handleOutStandingChange} />
                </div>

                <div className={rowClasses}>
                    <Cell text="Used Credit" classNames="col-xs-6" />
                    <CellContainNumberFormat text={this.props.balanceInfo.UsedCredit} classNames="col-xs-6 col-right" />
                </div>

                <div className={rowClasses}>
                    <Cell text="Given Credit" classNames="col-xs-6" />
                    <CellContainInput text={this.props.balanceInfo.GivenCredit}
                        id="txt-given-edit"
                        classNames="col-xs-6 col-right box-edited"
                        inputClassname="txt-number-edited"
                        onSave={this.handleGivenCredit}
                        iconClassname="icon-border-color icon-edited" />

                    <div id="popup-confirm-given-credit" className="popup-confirm">
                        <h1 className="title">Given Credit</h1>

                        <div className={rowClasses}>
                            <Cell text="Current Given Credit" classNames="col-xs-7" />
                            <Cell text={this.props.balanceInfo.CurrentGivenCredit.toLocaleString('en-us', { minimumFractionDigits: 2 })} classNames="col-xs-5 show-value" />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="New Given Credit" classNames="col-xs-7" />
                            <CellContainInput text={this.props.balanceInfo.NewGivenCredit}
                                classNames="col-xs-5 text-right"
                                inputClassname="hasBorder txt-newValue"
                                onSave={this.handleNewGivenCredit} />
                        </div>

                        <div className={rowClasses}>
                            <Cell text="Difference" classNames="col-xs-7" />
                            <Cell text={this.props.balanceInfo.Difference.toLocaleString('en-us', { minimumFractionDigits: 2 })} classNames="col-xs-5 show-value" />
                        </div>

                        <div className="col-xs-12 row-info popup-bottom list-button">
                            <Button text="Cancel" classNames="btn-cancel" onClick={this.handleCancelClick} />
                            <Button text="Submit" classNames="btn-submit" onClick={this.handleSubmitClick} />
                        </div>
                    </div>
                </div>
            </CustomizedAccordionItem>
      );
    }
}