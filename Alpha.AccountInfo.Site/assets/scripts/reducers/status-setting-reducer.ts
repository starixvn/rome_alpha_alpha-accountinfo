import * as _ from "lodash"; 

import * as Models from "../models";
import { STATUS_ACTION_NAMES, StatusSettingActions } from "../actions/status-setting-actions";

export const StatusSettingReducer = (state: Models.Status, action) => {
    if (!state) {
        state = new Models.Status("Demo");
    }

    switch (action.type) {
        case STATUS_ACTION_NAMES.UPDATE_STATUS:
            return _.assign(
                {},
                state,
                {
                    name: action.status.name
                });

        default:
            return state;
    }
}

export const StatusListReducer = (state = [], action) => {
    switch (action.type) {
        case STATUS_ACTION_NAMES.UPDATE_STATUS:
            return [
                ...state,
                {
                    name: action.status.name
                }
            ];

        default:
            return state;
    }
}

