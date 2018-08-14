import React from 'react';

const CheckBox = ({ classNames = null, checked = false, disabled = false, title = '', id = null, handleChecked = f => f }) => {
    const checkBoxClass = disabled ? `${classNames} disabled` : classNames;

    return (
        <div className={checkBoxClass}>
            <input type="checkbox"
                disabled={disabled}
                checked={checked ? true : false}
                onChange={handleChecked} id={id} />
            <label className="label-text" htmlFor={id}>{title}</label>
        </div>
    )
}    

export default CheckBox;