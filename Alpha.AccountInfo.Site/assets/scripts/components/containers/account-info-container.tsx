import * as React from "react";

import { StatusSettingConnect } from "./status-setting-container";
import { DownlineInfoConnect } from "./downline-info-container";

export default class AccountInfoContainer extends React.Component<{}, {}>{
    public render(): JSX.Element {
        return (
            <div>
                <DownlineInfoConnect />
                <StatusSettingConnect />
            </div>
        );
    }
}