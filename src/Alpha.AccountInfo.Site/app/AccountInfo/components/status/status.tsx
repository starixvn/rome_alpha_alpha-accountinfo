import * as React from 'react';

interface IStatusProps {
    text: string,
    checked?: boolean,
    classname?: string
  //  onclick(): void
}

interface IStatusState {
    classname: string,
    text: string
}

export default class Status extends React.Component<IStatusProps, IStatusState> {
    constructor(props) {
        super(props);
        this.state = {
            classname: this.props.classname ? this.props.classname : "row-info checkbox",
            text: this.props.text || ''
        }
    }
    handleChange = (e) => {
        this.setState({ text: e.target.value });
    }
    render() {
        return (
            <div className={this.state.classname}>
                <label>
                    <input type="checkbox"
                        checked={this.props.checked ? true : false}
                        onChange={this.handleChange}/>

                    {this.props.text}
                </label>
            </div> 
        );
    }
}