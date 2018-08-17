import React from 'react';
import { accountInfoAPI } from '../../api-client';

export default class StatusCheckbox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checked: this.props.checked
        }
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        var url = this.props.url;
        var self = this;

        this.props.confirmMessage == undefined ? window.top.ageWnd.open(self.props.url) :
            window.top.bootboxConfirm(self.props.confirmMessage, e,
                function () {
                    accountInfoAPI.updateStatus(url, JSON.parse(self.props.param)).then((data) => {
                        self.setState(previousState => ({
                            checked: !previousState.checked
                        }));

                        //if (data.IsSuccess) {
                        //    self
                        //    setTimeout(() => {
                        //        var iframe = parent.document.getElementById("accountinfo-content");
                        //        iframe.src = iframe.src;
                        //        window.top.ageWnd.close();
                        //    }, 3000);
                        //}
                    });
                });
    };

    render() {
        const { resources, classNames, disabled, title, id, confirmMessage, url } = this.props;
        const checkBoxClass = disabled ? `${classNames} disabled` : classNames;

        return (
            <div>
                <div className={checkBoxClass} onClick={this.handleOpen}>
                    <input id={id}
                        type="checkbox"
                        disabled={disabled}
                        checked={this.state.checked}
                        onChange={this.handleOpen} />
                    <label className="label-text" htmlFor={id}>{title}</label>
                </div>
            </div>
        );
    }
}