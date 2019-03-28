import * as React from 'react';;
import PropTypes from 'prop-types';
import { HashRouter, Switch, Route, Link } from 'react-router-dom';

export default class DebugRouter extends HashRouter {
    constructor(props) {
        super(props);
        console.log('initial history is: ', JSON.stringify(this.history, null, 2));
        this.history.listen((location, action) => {
            console.log(
                `The current URL is ${location.pathname}${location.search}${location.hash}`
            );
            console.log(`The last navigation action was ${action}`, JSON.stringify(this.history, null, 2));
        });
    }
}