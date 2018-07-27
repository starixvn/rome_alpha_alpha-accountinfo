import * as React from 'react';
import Cell from './cell';
import NumberFormat from 'react-number-format';

export default class CellContainInput extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            text: this.props.title
        }
    }

    //handleSubmit(e) {
    //    const text = e.target.value.trim();
    //    if (e.which === 13) {
    //        this.props.onSave(text);
    //    }
    //}

    handleChange(e) {
        this.setState({ text: e.target.value });
    }

    //handleBlur(e) {
       
    //}

    render() {
        const { classNames, title, iconClassname } = this.props;

        const inputClassname = "txt-number-edited";
        return (
            <div className={classNames}>
                <NumberFormat
                    className={inputClassname}
                    value={title}
                    thousandSeparator={true}
                    decimalScale="2"
                    onValueChange={this.handleChange} />

                <span className={iconClassname}></span>                
            </div>
      );
    }
}