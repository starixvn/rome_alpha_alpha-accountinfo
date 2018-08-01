
const ColTable = ({ title, classNames = null, isHeadeTable = false }) => {
    let colContent = '';
    if (isHeadeTable) {
        colContent = <th className={classNames}>{title}</th>;
    } else {
        colContent = <td className={classNames}>{title}</td>;
    }

    return colContent;
} 

export default ColTable;