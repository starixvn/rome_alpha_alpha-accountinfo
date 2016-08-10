import * as React from "react";
import * as Models from "../../models";

const DownlineInfoStateToProps = (state) => {
    return {
        name: state.name
    };
}

export interface StatusSettingContainerProps {
    name: string
}

export class DownlineInfoContainer extends React.Component<StatusSettingContainerProps, {}>{
    public render(): JSX.Element {
        return (
            <div className="downline-info">
                {this.props.name}
            </div>
        );
    }
}

export const DownlineInfoConnect = ReactRedux.connect(DownlineInfoStateToProps)(DownlineInfoContainer);