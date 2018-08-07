import React from 'react';
import CellContainLink from '../common/cell-contain-link';
import CheckBox from '../common/checkbox';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import {FlatButton, Dialog } from 'material-ui';

const styles = theme => ({
    root: {
        width: '100%',
        maxWidth: 360,
        background: theme.palette.background.paper,
    },
    dialog: {
        width: 475,
        maxHeight: 220,
    },
});


export default class StatusOnProduct extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false,
            value : ''
        }
        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);

    }

    handleChange (e) {
        this.setState({ value: e.target.value });
    }

    handleOpen(e) {
        this.setState({
            open: true
        });
    };

    handleClose(e) {
        this.setState({ open: false });
    };
    handleSubmit(e) {
        this.handleClose(e); 
    };
    render() {
        const { disabled, statusId, title, confirmMessage, url } = this.props;

        const styles = {
            dialogPaper: {
                height: "220px",
                width: "475px",
            },
        };
        const dialogPaper = {
            height: 220,
            width: 475
        };

        return (
           
            <MuiThemeProvider>
                <div onClick={this.handleOpen}>
                    <CellContainLink title={title}
                        statusId={statusId}
                        classNames="col"
                        iconClassname="icon-ic_list"
                        hrefLink="#" />
                    <Dialog
                        classes={{ paper: styles.dialogPaper }}
                        title={confirmMessage == null && `${title}`}
                        modal={false}
                        open={this.state.open}
                        onRequestClose={this.handleClose}>
                          <div className="col-xs-12 row-info popup-bottom list-button">
                            {
                                confirmMessage != null &&
                                <div>
                                    {confirmMessage}
                                    <div>
                                        <a className="btn-default with-level" onClick={this.handleClose}>Cancel</a>
                                        <a className="btn-default with-level" onClick={this.handleSubmit}>Submit</a>
                                    </div>
                                </div>
                            }

                            {
                                confirmMessage == null &&
                                <iframe width="475" height="220" src={window.accountInfoSiteAppPath + url} frameborder="0" allowfullscreen></iframe>
                            }
                        </div>
                    </Dialog>
                </div>
                </MuiThemeProvider>
            
        );
    }
}