import React from 'react';

export default class Status extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            classname: this.props.classname ? this.props.classname : "checkbox",
            text: this.props.text || ''
        }
    }
    handleChange (e) {
        this.setState({ text: e.target.value });
    }
    render() {
        return (
            <div className={this.state.classname}>
                <input type="checkbox"
                    checked={this.props.checked ? true : false}
                    onChange={this.handleChange} />
                <label className="label-text" htmlFor="showPassword">{this.props.text}</label>
            </div> 
        );
    }
}