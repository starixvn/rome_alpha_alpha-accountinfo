import React from 'react';
import ReactDOM from 'react-dom';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from './components/common/cell-contain-number-format';
import { handleError } from '../../../Shared/Scripts/api-client';
import { accountInfoAPI } from './accountinfo-api';
import { UserLevel } from './constants';
import { decodeResources, formatNumber } from './helpers/accountinfo-helper';
import { Tooltip } from 'react-tippy';

class GivenCredit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            newGivenCredit: this.props.currentGivenCredit,
            hideMessages: true,
            updateSuccess: false,
            message: '',
            resources: this.props.resources,
        };

        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    handleKeyPress(e) {
        if (e.key === 'Enter') {
            this.handleSubmit();
        }
    }

    handleClose(updateSuccessfully = false) {
        if (window.top.updateGivenCreditPopup
            && window.top.updateGivenCreditPopup.onClosing
            && typeof window.top.updateGivenCreditPopup.onClosing === 'function') {
            window.top.updateGivenCreditPopup.onClosing(
                this.props.searchAccountName, // use for reload left panel
                updateSuccessfully,
                this.state.newGivenCredit, // use for update value on html Show QR code form
                this.props.searchAccountId); // use for update value on html Credit Balance form
        }
    }

    showErrorMessage(message) {
        this.setState(() => ({
            hideMessages: false,
            message: message,
            updateSuccess: false
        }));
    }

    handleSubmit() {
        const { resources, searchAccountId, searchAccountRoleId } = this.props,
            currentGivenCredit = parseFloat(this.props.currentGivenCredit),
            givenCreditMin = parseFloat(this.props.givenCreditMin),
            givenCreditMax = parseFloat(this.props.givenCreditMax),
            newGivenCredit = parseFloat(this.state.newGivenCredit);

        if (newGivenCredit < 0 || newGivenCredit < givenCreditMin || newGivenCredit > givenCreditMax) {
            this.showErrorMessage(resources.InvalidValue);
            return;
        }

        if (newGivenCredit === currentGivenCredit) {
            this.showErrorMessage(resources.ThereIsNoChange);
            return;
        }

        $.blockUI();

        const params = {
            SearchAccountId: searchAccountId,
            SearchAccountRoleId: searchAccountRoleId,
            CreditAmount: newGivenCredit
        };

        accountInfoAPI.updateGivenCredit(params)
            .then(data => {
                this.setState({
                    hideMessages: false,
                    message: data.IsSuccess ? resources.UpdateGivenCreditSuccessfully : data.Message.trim(),
                    updateSuccess: data.IsSuccess
                });

                if (data.IsSuccess) {
                    this.handleClose(data.IsSuccess);
                }

                $.unblockUI();
            }).catch(handleError);
    }

    buildRow(title, value) {
        return (
            <div className="col-xs-12 form-group">
                <div className="col-xs-7">{title}</div>
                <CellContainNumberFormat value={value} classNames="value col-xs-5" />
            </div>
        );
    }

    buildGivenCreditLimitNote() {
        const { searchAccountRoleId, givenCreditMin, givenCreditMax, memberMaxCredit, availableCredit } = this.props;
        const { resources } = this.state;
        const buildNumberFormat = value =>
            <NumberFormat
                className="number"
                value={value}
                displayType={'text'}
                decimalScale={2}
                fixedDecimalScale={true}
                thousandSeparator={true}
            />;

        const renderTooltip = () => {
            if (searchAccountRoleId == UserLevel.Member && memberMaxCredit > 0) {
                var value = `${resources.CreditHint.replace("{0}", formatNumber(availableCredit))}<br>${resources.MemberMaxCreditHint.replace("{0}", formatNumber(memberMaxCredit))}`;
                return <Tooltip title={value} position="left" trigger="mouseenter" arrow={true}><span className="icon-credit"></span></Tooltip>;
            }

            return null;
        }

        return (
            <div className="col-xs-12 form-group note" >
                <div className="value">
                    {resources.Min}: {buildNumberFormat(givenCreditMin)} - {resources.Max}: {buildNumberFormat(givenCreditMax)}{renderTooltip()}
                </div>
            </div>
        );
    }

    render() {
        const rowClasses = 'col-xs-12 form-group';
        const { newGivenCredit, updateSuccess, message, hideMessages, resources } = this.state;
        const { currentGivenCredit } = this.props;

        return (
            <div>
                <div className={hideMessages ? `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                {this.buildRow(resources.CurrentGivenCredit, currentGivenCredit)}

                <div className={rowClasses}>
                    <div className="col-xs-7">{resources.NewGivenCredit}</div>
                    <NumberFormat className="textbox col-xs-5"
                        value={newGivenCredit}
                        fixedDecimalScale={true}
                        thousandSeparator={true}
                        allowNegative={false}
                        decimalScale={2}
                        onKeyPress={this.handleKeyPress}
                        onValueChange={values => {
                            const { floatValue } = values;
                            this.setState({ newGivenCredit: floatValue === undefined ? 0 : floatValue });
                        }}
                    />
                </div>

                {this.buildGivenCreditLimitNote()}

                {this.buildRow(resources.Difference, newGivenCredit - currentGivenCredit)}

                <div className="col-xs-12 row-info popup-bottom list-button">
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Submit}</button>
                    <button className="btn-cancel" onClick={this.handleClose}>{resources.Cancel}</button>
                </div>

            </div>
        );
    }
}

export default GivenCredit;

export const renderGivenCredit = (searchAccountId,
    searchAccountName,
    searchAccountRoleId,
    currentGivenCredit,
    givenCreditMin,
    givenCreditMax,
    resources,
    memberMaxCredit,
    availableCredit
) => {
    ReactDOM.render(
        <GivenCredit searchAccountId={searchAccountId}
            searchAccountName={searchAccountName}
            searchAccountRoleId={searchAccountRoleId}
            currentGivenCredit={currentGivenCredit}
            givenCreditMin={givenCreditMin}
            givenCreditMax={givenCreditMax}
            resources={decodeResources(resources)}
            memberMaxCredit={memberMaxCredit}
            availableCredit={availableCredit}
        />,
        document.getElementById('given-credit')
    );
};

window.renderGivenCredit = renderGivenCredit;