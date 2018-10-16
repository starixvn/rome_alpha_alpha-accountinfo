import * as React from 'react';
import ReactDOM from 'react-dom';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from './components/common/cell-contain-number-format';
import { accountInfoAPI } from './api-client';
import { base64Helper } from '../helpers/accountinfo-helper';

window.renderGivenCredit = function (searchAccountId, searchAccountRoleId, currentGivenCredit, resources) {
    ReactDOM.render(
        <GivenCredit searchAccountId={searchAccountId}
            searchAccountRoleId={searchAccountRoleId}
            currentGivenCredit={currentGivenCredit}
            resources={resources}
        />,
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
            message: '',
            resources: base64Helper.decodeResources(this.props.resources)
        };

        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleClose(e) {
        window.top.closedPopup = true;
        window.top.ageWnd.close();
    }

    handleSubmit(e) {
        $.blockUI();
        var params = {
            SearchAccountId: this.props.searchAccountId,
            SearchAccountRoleId: this.props.searchAccountRoleId,
            CreditAmount: this.state.newGivenCredit
        };

        accountInfoAPI.updateGivenCredit(params).then((data) => {
            let self = this;
            let { resources } = this.props;
            self.setState({
                hideMessages: false,
                message: data.IsSuccess ? resources.UpdateGivenCreditSuccessfully : data.Message,
                updateSuccess: data.IsSuccess
            });

            if (data.IsSuccess) {
                setTimeout(() => {
                    window.top.realoadLeftPanel = true;
                    window.top.closedPopup = true;
                    window.top.ageWnd.close();
                }, 3000);
            }

            $.unblockUI();
        });
    }

    render() {
        const rowClasses = "col-xs-12 form-group";
        const { newGivenCredit, updateSuccess, message, hideMessages, resources } = this.state;
        const { currentGivenCredit} = this.props;

        let row = (title, value) =>
            <div className={rowClasses}>
                <div className="col-xs-7">{title}</div>
                <CellContainNumberFormat value={value} classNames="value col-xs-5" />
            </div>;

        return (

            <div>
                <div className={hideMessages ?  `hidden` : ``}>
                    <span className={updateSuccess ? 'col-xs-12 msg msg-success' : 'col-xs-12 msg msg-error'}>  {message} </span>
                </div>

                {row(resources.CurrentGivenCredit, currentGivenCredit)}

                <div className={rowClasses}>
                    <div className="col-xs-7">{resources.NewGivenCredit}</div>
                    <NumberFormat className="textbox col-xs-5"
                        value={newGivenCredit}
                        fixedDecimalScale={true}
                        thousandSeparator={true}
                        decimalScale={2}
                        onValueChange={(values) => {
                            const { formattedValue, value, floatValue } = values;
                            this.setState({ newGivenCredit: floatValue === undefined ? 0 : floatValue });
                        }}
                    />
                </div>

                {row(resources.Difference, newGivenCredit - currentGivenCredit)}

                <div className="col-xs-12 row-info popup-bottom list-button">                    
                    <button className="btn-submit" onClick={this.handleSubmit}>{resources.Submit}</button>
                    <button className="btn-cancel" onClick={this.handleClose}>{resources.Cancel}</button>
                </div>

                <div className="note">
                    <span className="text-red">*</span> <strong>Note</strong>: <span className="number">93,731,950.00</span> ≤ New Value ≤ <span className="number">88,671,518,500.00</span>
                </div>
            </div>
        );
    }
}