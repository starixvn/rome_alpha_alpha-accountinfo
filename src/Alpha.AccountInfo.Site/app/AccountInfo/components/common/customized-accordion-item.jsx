import React from 'react';
import Button from '../common/button';

import CellContainNumberFormat from '../common/cell-contain-number-format';
import CellContainInput from '../common/cell-contain-input';
import Cell from '../common/cell';
import {
    AccordionItem,
    AccordionItemTitle,
    AccordionItemBody,
} from 'react-accessible-accordion';

export default class CustomizedAccordionItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expanded: this.props.expanded || false
        }        
    }

    render() {
        const {
            title,
            iconClassname,
            accordionItemBodyClassName,
            expanded,
            handleClick,
            dataId
        } = this.props;

        return (
            <AccordionItem expanded={expanded} onClick={handleClick} data-id={dataId} >
                <AccordionItemTitle>
                    <span className={iconClassname}></span> {title}
                </AccordionItemTitle>
                <AccordionItemBody className={accordionItemBodyClassName}>
                    {this.props.children}
                </AccordionItemBody>
            </AccordionItem>
        );
    }
}