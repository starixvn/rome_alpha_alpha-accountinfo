import * as Models from "../models";

export const STATUS_ACTION_NAMES =
    {
        UPDATE_STATUS: "UPDATE_STATUS"
    };

export const StatusSettingActions = {
    updateStatus: (status: Models.Status) => {
        return {
            type: STATUS_ACTION_NAMES.UPDATE_STATUS,
            status: status
        };
    }
}