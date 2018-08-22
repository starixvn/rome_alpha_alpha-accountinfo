import * as React from 'react';
import ReactDOM from 'react-dom';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from './components/common/cell-contain-number-format';
import { accountInfoAPI } from './api-client';

window.renderGivenCredit = function (searchAccountId, searchAccountRoleId, currentGivenCredit, resources) {
    ReactDOM.render(
        <GivenCredit searchAccountId={searchAccountId}
            searchAccountRoleId={searchAccountRoleId}
            currentGivenCredit={currentGivenCredit}
            resources={resources}/>,
        document.getElementById('given-credit')
    );
};

export default class GivenCredit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            newGivenCredit: this.props.currentGivenCredit,
            hideMessages: true,
            updateSuccess: false,
            message: ''
        };

        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleClose(e) {
        window.top.closeGivenCreditPopup = true;
        window.top.ageWnd.close();
    };

    handleSubmit(e) {
        $.blockUI();
        var params = {
            SearchAccountId: this.props.searchAccountId,
            SearchAccountRoleId: this.props.searchAccountRoleId,
            CreditAmount: this.state.newGivenCredit
        };

        accountInfoAPI.updateGivenCredit(params).then((data) => {
            let self = this;
            if (data.IsSuccess) {
                $.unblockUI();
                self.setState({
                    hideMessages: false,
                    message: data.Message,
                    updateSuccess: data.IsSuccess
                });

                setTimeout(() => {
                    window.top.closeGivenCreditPopup = true;
                    window.top.ageWnd.close();
                }, 2000);
            }
        });
    };

    render() {
        const rowClasses = "col-xs-12 form-group";
        const { newGivenCredit, updateSuccess, message, hideMessages } = this.state;
        const { resources, currentGivenCredit} = this.props;

        return (

            <div>
                <div className={hideMessages && 'hidden'}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>
                <div className={rowClasses}>
                    <div className="col-xs-7">{resources.CurrentGivenCredit}</div>
                    <CellContainNumberFormat title={currentGivenCredit} classNames="value col-xs-5" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-7">{resources.NewGivenCredit}</div>
                    <NumberFormat className="textbox col-xs-5" value={this.state.newGivenCredit} thousandSeparator={true} decimalScale="2" onValueChange={(values) => {
                        const { formattedValue, value, floatValue } = values;
                        this.setState({ newGivenCredit: (floatValue === undefined ? 0 : floatValue) })
                    }} />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-7">{resources.Difference}</div>
                    <CellContainNumberFormat title={newGivenCredit - currentGivenCredit} classNames="value col-xs-5" />
                </div>
                <div className="col-xs-12 row-info popup-bottom list-button">                    
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Submit}</button>
                    <button className="btn-cancel" onClick={this.handleClose}>{resources.Cancel}</button>
                </div>
            </div>
        );
    }
}