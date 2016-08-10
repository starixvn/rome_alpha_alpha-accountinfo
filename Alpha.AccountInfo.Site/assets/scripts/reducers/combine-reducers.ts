import * as Redux from "redux";
import { StatusSettingReducer, StatusListReducer } from "./status-setting-reducer";
import { DownlineInfoReducer } from "./downline-info-reducer"; 


export default Redux.combineReducers({
    status: StatusSettingReducer,
    name: DownlineInfoReducer,
    statusList: StatusListReducer
});