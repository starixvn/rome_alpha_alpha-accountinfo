import React from 'react';

const CheckBox = ({ classNames = null, checked = false, disabled = false, title = '', id = null, handleChecked = f => f }) => {
    return (
        <div className={classNames}>
            <input type="checkbox"
                disabled={disabled ? true : false}
                checked={checked ? true : false}
                onChange={handleChecked} id={id} />
            <label className="label-text" htmlFor={id}>{title}</label>
        </div>
    )
}    

export default CheckBox;