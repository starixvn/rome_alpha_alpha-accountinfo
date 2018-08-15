import * as React from 'react';
import ReactDOM from 'react-dom';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from './components/common/cell-contain-number-format';
import CustomizedAccordionItem from './components/common/customized-accordion-item';
import { accountInfoAPI } from './api-client';
import $ from 'jquery';

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

        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOpen(e) {

    };

    handleClose(e) {
        window.top.ageWnd.close();
    };

    handleSubmit(e) {
        var params = {
            SearchAccountId: this.props.searchAccountId,
            SearchAccountRoleId: this.props.searchAccountRoleId,
            CreditAmount: this.state.newGivenCredit
        };

        accountInfoAPI.updateGivenCredit(params).then((data) => {
            let self = this;
            this.setState({
                hideMessages: false,
                message: data.Message,
                updateSuccess: data.Code == 0
            });

            if (data.Code == 0) {
                setTimeout(() => {
                    var iframe = parent.document.getElementById("accountinfo-content");
                    iframe.src = iframe.src;
                    window.top.ageWnd.close();
                }, 3000);
            }
        });
    };

    render() {
        const rowClasses = "col-xs-12 row-info";
        const { newGivenCredit, updateSuccess, message, hideMessages } = this.state;
        const { resources, currentGivenCredit} = this.props;

        return (

            <div className={rowClasses}>
                <div className={hideMessages && 'hidden'}>
                    <span className={updateSuccess ? 'msg-success' : 'msg-error'}>  {message} </span>
                </div>
                <div className={rowClasses}>
                    <div className="col-xs-7">resources.CurrentGivenCredit</div>
                    <CellContainNumberFormat title={currentGivenCredit} classNames="col-right" />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-7">resources.NewGivenCredit</div>
                    <NumberFormat value={this.state.newGivenCredit} thousandSeparator={true} decimalScale="2" onValueChange={(values) => {
                        const { formattedValue, value, floatValue } = values;
                        this.setState({ newGivenCredit: (floatValue === undefined ? 0 : floatValue) })
                    }} />
                </div>

                <div className={rowClasses}>
                    <div className="col-xs-7">resources.Difference</div>
                    <CellContainNumberFormat title={newGivenCredit - currentGivenCredit} classNames="col-right" />
                </div>
                <div className="col-xs-12 row-info popup-bottom list-button">
                    <a className="btn-default with-level" onClick={this.handleClose}>{resources.Cancel}</a>
                    <a className="btn-default with-level" onClick={this.handleSubmit}>{resources.Submit}</a>
                </div>
            </div>
        );
    }
}