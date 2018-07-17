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
    }

    render() {
        const { title, iconClassname, accordionItemBodyClassName } = this.props;

        return (
            <AccordionItem>
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