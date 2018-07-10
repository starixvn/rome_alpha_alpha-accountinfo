import * as React from 'react';

interface IStatusProps {
    text: string,
    id?: string,
    checked?: boolean
  //  onclick(): void
}

interface IStatusState {
}

export default class Status extends React.Component<IStatusProps, IStatusState> {
    constructor(props) {
        super(props);
    }
    handleChange = (e) => {
        this.setState({ text: e.target.value });
    }
    render() {
        return (
            <div className="row-info checkbox">
                <label>
                    <input type="text"
                        checked={this.props.checked ? true : false} 
                        id={this.props.id}
                        onChange={this.handleChange}/>

                    {this.props.text}
                </label>
            </div> 
        );
    }
}