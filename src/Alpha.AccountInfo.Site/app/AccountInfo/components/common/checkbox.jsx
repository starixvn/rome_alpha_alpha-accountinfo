import React from 'react';

const CheckBox = ({ checked, title, id, handleChecked=f=>f }) => {
    return (
        <div>
            <input type="checkbox"
                checked={checked ? true : false}
                onChange={handleChecked} id={id} />
            <label className="label-text" htmlFor={id}>{title}</label>
        </div>
    )
}    

export default CheckBox;