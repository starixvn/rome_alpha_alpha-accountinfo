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
    render() {
        let isCheck = this.props.checked ? "checked" : "";
        return (
            <div className="row-info checkbox">
                <label>
                    <input type="checkbox" id={this.props.id} {...isCheck} />
                    {this.props.text}
                </label>
            </div> 
        );
    }
}