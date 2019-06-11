import React from 'react';
import ReactDOM from 'react-dom';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from './components/common/cell-contain-number-format';
import { accountInfoAPI } from './accountinfo-api';
import { UserLevel } from './constants';
import { decodeResources } from './helpers/accountinfo-helper';

class GivenCredit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            newGivenCredit: this.props.currentGivenCredit,
            hideMessages: true,
            updateSuccess: false,
            message: '',
            resources: this.props.resources
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

    handleClose() {
        window.top.closedPopup = true;
        window.top.ageWnd.close();
    }

    showErrorMessage(message) {
        this.setState(() => ({
            hideMessages: false,
            message: message,
            updateSuccess: false
        }));
    }

    handleSubmit() {
        const self = this, { resources, searchAccountId, searchAccountRoleId } = this.props,
            currentGivenCredit = parseFloat(this.props.currentGivenCredit),
            givenCreditMin = parseFloat(this.props.givenCreditMin),
            givenCreditMax = parseFloat(this.props.givenCreditMax),
            newGivenCredit = parseFloat(this.state.newGivenCredit);

        if (newGivenCredit < givenCreditMin || newGivenCredit > givenCreditMax) {
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

        accountInfoAPI.updateGivenCredit(params).then(data => {
            self.setState({
                hideMessages: false,
                message: data.IsSuccess ? resources.UpdateGivenCreditSuccessfully : data.Message.trim(),
                updateSuccess: data.IsSuccess
            });

            if (data.IsSuccess) {
                setTimeout(() => {
                    window.top.realoadLeftPanel = true;
                    window.top.closedPopup = true;
                    window.top.ageWnd.close();
                }, 1500);
            }

            $.unblockUI();
        });
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
        const { searchAccountRoleId, givenCreditMin, givenCreditMax } = this.props;
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

        const memberGivenCreditLimit = <div className="content"> {resources.NewGivenCredit} ≤ {buildNumberFormat(givenCreditMax)} </div>;

        const masterAgentGivenCreditLimit =
            <div className="content">
                {buildNumberFormat(givenCreditMin)} ≤  {resources.NewGivenCredit} ≤ {buildNumberFormat(givenCreditMax)}
            </div>;

        const noted = parseInt(searchAccountRoleId, 10) === UserLevel.Member ? memberGivenCreditLimit : masterAgentGivenCreditLimit;

        return (
            <div className="note">
                <span className="text-red">*</span>
                <strong>{resources.Note}</strong>:
                  {noted}
            </div>
        );
    }

    render() {
        const rowClasses = "col-xs-12 form-group";
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
                        decimalScale={2}
                        onKeyPress={this.handleKeyPress}
                        onValueChange={values => {
                            const { formattedValue, value, floatValue } = values;
                            this.setState({ newGivenCredit: floatValue === undefined ? 0 : floatValue });
                        }}
                    />
                </div>

                {this.buildRow(resources.Difference, newGivenCredit - currentGivenCredit)}

                <div className="col-xs-12 row-info popup-bottom list-button">
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Submit}</button>
                    <button className="btn-cancel" onClick={this.handleClose}>{resources.Cancel}</button>
                </div>

                {this.buildGivenCreditLimitNote()}
            </div>
        );
    }
}

export default GivenCredit;

export const renderGivenCredit = (searchAccountId,
    searchAccountRoleId,
    currentGivenCredit,
    givenCreditMin,
    givenCreditMax,
    resources
) => {
    ReactDOM.render(
        <GivenCredit searchAccountId={searchAccountId}
            searchAccountRoleId={searchAccountRoleId}
            currentGivenCredit={currentGivenCredit}
            givenCreditMin={givenCreditMin}
            givenCreditMax={givenCreditMax}
            resources={decodeResources(resources)}
        />,
        document.getElementById('given-credit')
    );
};

window.renderGivenCredit = renderGivenCredit;
