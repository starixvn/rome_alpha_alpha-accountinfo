import * as React from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

import { RaisedButton, FlatButton, Dialog } from 'material-ui';

/**
 * A modal dialog can only be closed by selecting one of the actions.
 */



export default class DialogExampleModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false
        };
        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
    }

    handleOpen (e){
        this.setState({ open: true });
    };

    handleClose (e)  {
        this.setState({ open: false });
    };

    render() {
        const actions = [
            <FlatButton
                label="Cancel"
                primary={true}
                onClick={this.handleClose}
            />,
            <FlatButton
                label="Submit"
                primary={true}
                disabled={true}
                onClick={this.handleClose}
            />,
        ];

        return (
          

            <MuiThemeProvider >
                <div>
                    <RaisedButton label="Modal Dialog" onClick={this.handleOpen} />
                    <Dialog
                        title="Dialog With Actions"
                        actions={actions}
                        modal={true}
                        open={this.state.open}>
                        Only actions can close this dialog.
                     </Dialog>
                    </div>
                </MuiThemeProvider>
 
           
        );
    }
}
