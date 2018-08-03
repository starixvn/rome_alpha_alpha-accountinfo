import React from 'react';

const NoInfomation = ({ classNames = null, title ='No information is available' }) => 
    <div className={classNames}>{title}</div>

export default NoInfomation;