import * as React from "react";
import * as Models from "../../models";
import { StatusSettingActions } from "../../actions/status-setting-actions";

const StatusSettingStateToProps = (state) => {
    return {
        status: state.status,
        statusList: state.statusList
    };
}

const StatusSettingDispatchToProps = (dispatch) => {
    return {
        changeStatus: (status: Models.Status) => {
            //// Call Ajax
            dispatch(StatusSettingActions.updateStatus(status));
        }
    }
};

export interface StatusSettingContainerProps {
    status: Models.Status,
    statusList: Models.Status[],
    changeStatus(status: Models.Status): void
}

export class StatusSettingContainer extends React.Component<StatusSettingContainerProps, {}>{
    public render(): JSX.Element {
        let statusTextbox: HTMLInputElement;

        return (
            <div className="status-setting">
                Current Status: {this.props.status.name} <br />

                <button onClick={() => {
                    this.props.changeStatus(new Models.Status(statusTextbox.value));
                } }>
                    Add Status
                </button>

                <input type="text" ref={(input) => statusTextbox = input} />

                <ul>
                    {
                        this.props.statusList.map(status => (<li>{status.name}</li>))
                    }
                </ul>
            </div>
        );
    }
}

export const StatusSettingConnect  = ReactRedux.connect(StatusSettingStateToProps, StatusSettingDispatchToProps)(StatusSettingContainer);