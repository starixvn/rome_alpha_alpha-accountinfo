import React from 'react';
import CheckBox from '../common/checkbox';

export default class Status extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value : ''
        }
    }

    handleChange (e) {
        this.setState({ value: e.target.value });
    }

    render() {
        const { classNames, checked, title, id } = this.props;

        return (
            <CheckBox classNames={classNames} checked={checked} onChange={this.handleChange} title={title} id={id} />
        );
    }
}