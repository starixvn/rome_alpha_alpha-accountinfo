import React from 'react';
import { accountInfoAPI } from '../../api-client';

export default class StatusCheckbox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: ''
        }
        this.handleOpen = this.handleOpen.bind(this);
    }

    handleOpen(e) {
        var url = this.props.url;
        this.props.confirmMessage == undefined ? window.top.ageWnd.open(this.props.url) : window.top.bootboxConfirm(this.props.confirmMessage, e, function () { accountInfoAPI.updateStatus(url) });
    };

    render() {
        const { resources, classNames, checked, disabled, title, id, confirmMessage, url } = this.props;
        const checkBoxClass = disabled ? `${classNames} disabled` : classNames;

        return (
            <div>
                <div className={checkBoxClass} onClick={this.handleOpen}>
                    <input id={id}
                        type="checkbox"
                        disabled={disabled}
                        checked={checked}
                        onChange={this.handleOpen} />
                    <label className="label-text" htmlFor={id}>{title}</label>
                </div>
            </div>
        );
    }
}