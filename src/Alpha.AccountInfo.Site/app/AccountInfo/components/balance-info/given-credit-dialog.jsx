import * as React from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

import { RaisedButton, FlatButton, Dialog } from 'material-ui';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from '../common/cell-contain-number-format';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import { accountInfoAPI } from '../../api-client';
import { fade } from 'material-ui/utils/colorManipulator';

export default class GivenCreditModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false,
            newGivenCredit: 0,
            hideMessages: true,
            updateSuccess: false,
            message: ''
        };

        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOpen (e){
        this.setState({
            open: true,
            newGivenCredit: 0,
            hideMessages: true,
            updateSuccess: false,
            message: ''
        });
    };

    handleClose (e)  {
        this.setState({ open: false });
    };

    handleSubmit(e) {
        var params = {
            SearchAccountId: this.props.custId,
            SearchAccountRoleId: this.props.roleId,
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
                    self.handleClose(e);
                    self.props.fetchCustomerInfoFromServer(e, this.props.username);
                } , 3000);
            }
        });
    };

    render() {
        const rowClasses = "col-xs-12 row-info";
        const { newGivenCredit, updateSuccess, message, hideMessages } = this.state;
        const { resources, balanceInfo } = this.props;

        return (

            <div className={rowClasses}>
                <div className="col-xs-6">{resources.GivenCredit}</div>
                <div className="col-xs-6 col-right given-credit" title={`<= ${balanceInfo.GivenCredit}`} >
                    <NumberFormat
                        className="link"
                        value={balanceInfo.GivenCredit}
                        displayType={'text'}
                         decimalScale="2"
                fixedDecimalScale={true}
                thousandSeparator={true}/>
                    <MuiThemeProvider >
                        <span className="icon-border-color icon-edited" onClick={this.handleOpen} >
                            <Dialog
                                title={resources.GivenCredit}
                                modal={true}
                                open={this.state.open}>
                                <div className={hideMessages && 'hidden'}>
                                    <span className={updateSuccess ? 'msg-success' : 'msg-error'}>  {message} </span>
                                </div>
                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.CurrentGivenCredit</div>
                                    <CellContainNumberFormat title={balanceInfo.GivenCredit} classNames="col-right" />
                                </div>

                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.NewGivenCredit</div>
                                    <NumberFormat thousandSeparator={true} decimalScale="2" onValueChange={(values) => {
                                        const { formattedValue, value, floatValue } = values;
                                        this.setState({ newGivenCredit: (floatValue === undefined ? 0 : floatValue)})
                                    }} />
                                </div>

                                <div className={rowClasses}>
                                    <div className="col-xs-7">resources.Difference</div>
                                    <CellContainNumberFormat title={balanceInfo.GivenCredit - newGivenCredit} classNames="col-right" />
                                </div>

                                <div className="col-xs-12 row-info popup-bottom list-button">
                                    <a className="btn-default with-level" onClick={this.handleClose}>{resources.Cancel}</a>
                                    <a className="btn-default with-level" onClick={this.handleSubmit}>{resources.Submit}</a>
                                </div>
                            </Dialog>
                        </span>
                    </MuiThemeProvider>
                </div>
            </div>
        );
    }
}
