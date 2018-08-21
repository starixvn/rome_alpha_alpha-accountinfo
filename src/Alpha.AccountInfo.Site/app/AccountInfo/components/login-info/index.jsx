import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';

const LoginInfoBlock = ({ resources, handleCollapExpandBlock, blockId, expanded, loginInfo }) =>
    <CustomizedAccordionItem
        title={resources.LoginInfo}
        iconClassname="icon-login"
        accordionItemBodyClassName="accordion__body block-login-info"
        expanded={expanded}
        handleClick={handleCollapExpandBlock}
        blockId={blockId}>

        <div className="group">
            <div className="row-info bold">{resources.CreatedDate}:</div>
            <div className="row-info">{loginInfo.CreatedDate}</div>
        </div>
        <div className="group">
            <div className="row-info bold">{resources.LastLogin}:</div>
            <div className="row-info">{loginInfo.LastLogin}</div>
        </div>
        <div className="group">
            <div className="row-info bold">{resources.LastTxn}:</div>
            <div className="row-info">{loginInfo.LastTxn}</div>
        </div>
        <div className="group">
            <div className="row-info bold">{resources.LoginIP}:</div>
            <div className="row-info">{loginInfo.LoginIP}</div>
        </div>
    </CustomizedAccordionItem>

export default LoginInfoBlock;
