import React from 'react';
import CustomizedAccordionItem from '../common/customized-accordion-item';
import LoginInfoContent from './login-info-content';

const LoginInfoDesktop = ({
    customerInfo,
    resources,
    handleOpenIPInfo,
    expanded,
    blockId,
    handleCollapExpandBlock }) => customerInfo === null ?
        <div className="no-information">{resources.NoInfo}</div> :
        <CustomizedAccordionItem
            title={resources.LoginInfo}
            iconClassname="icon-login"
            accordionItemBodyClassName="accordion__body login-info"
            expanded={expanded}
            handleClick={handleCollapExpandBlock}
            blockId={blockId}
        >
            <LoginInfoContent resources={resources} loginInfo={customerInfo.LoginInfo} handleOpenIPInfo={handleOpenIPInfo} />
        </CustomizedAccordionItem>;

export default LoginInfoDesktop;
