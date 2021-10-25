import React from 'react';

export const RowContent = ({ dataRow, index, handleChange, resources }) => {
    let className, text;
    if (dataRow.IsProtectedMode) {
        className = 'protected-button';
        text = resources.Protected;
    } else {
        className = 'unprotected-button';
        text = resources.Unprotected;
    }

    return (
        <tr>
            <td>{dataRow.Username}</td>
            <td className="text-center">
                <div onClick={() => handleChange(index, !dataRow.IsProtectedMode)} className={className}>
                    {text}
                </div>
            </td>
        </tr>);
};