import * as React from 'react';
import Cell from './cell';
//import * as NumberFormat from '..../node_modules/react-number-format/src/number_format';

interface ICellContainInputProps {
    classnames?: string;
    id?: string,
    inputClassname?: string;
    text: number;
    onSave(text: string): void,
    iconClassname?: string
}

interface ICellContainInputState {
    text: number
}

export default class CellContainInput extends React.Component<ICellContainInputProps, ICellContainInputState> {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.text
        }
    }

    handleSubmit = (e) => {
        const text = e.target.value.trim();
        if (e.which === 13) {
            this.props.onSave(text);
           
        }
    }

    handleChange = (e) => {
        this.setState({ text: e.target.value });
    }

    handleBlur = (e) => {
       
    }
    render() {
        const inputClassname = "txt-number-edited";
        return (
            <div className={this.props.classnames}>
                <input type="text"
                    className={inputClassname}
                    value={this.props.text.toLocaleString('en-us', { minimumFractionDigits: 2 })} 
                    onBlur={this.handleBlur}
                    onChange={this.handleChange}
                    onKeyDown={this.handleSubmit} />
                <span className={this.props.iconClassname}></span>
            </div>
      );
    }
}