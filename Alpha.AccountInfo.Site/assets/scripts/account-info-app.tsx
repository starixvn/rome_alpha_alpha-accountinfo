import * as Redux from "redux";
import * as React from "react";
import * as ReactDOM from "react-dom"; 
import * as ReactRedux from "react-redux";
import * as _ from "lodash"; 

import * as Models from "./models";
import AccountInfoContainer from "./components/containers/account-info-container";
import reducer from "./reducers/combine-reducers";

let statusStore = Redux.createStore(reducer);

ReactDOM.render(
    <ReactRedux.Provider store={statusStore}>
        <AccountInfoContainer />
    </ReactRedux.Provider>,
    document.getElementById('root')
)