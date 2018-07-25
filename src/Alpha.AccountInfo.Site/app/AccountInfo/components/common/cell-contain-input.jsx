import * as React from 'react';
import Cell from './cell';


export default class CellContainInput extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.title
        }
    }

    handleSubmit(e) {
        const text = e.target.value.trim();
        if (e.which === 13) {
            this.props.onSave(text);
           
        }
    }

    handleChange(e) {
        this.setState({ text: e.target.value });
    }

    handleBlur(e) {
       
    }
    render() {
        const inputClassname = "txt-number-edited";
        return (
            <div className={this.props.classNames}>
                <input type="text"
                    className={inputClassname}
                    value={this.props.title.toLocaleString('en-us', { minimumFractionDigits: 2 })} 
                    onBlur={this.handleBlur}
                    onChange={this.handleChange}
                    onKeyDown={this.handleSubmit} />
                <span className={this.props.iconClassname}></span>
            </div>
      );
    }
}