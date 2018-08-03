import React from 'react';
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
            blockId
        } = this.props;

        return (
            <AccordionItem expanded={expanded}>
                <AccordionItemTitle onClick={handleClick} data-blockid={blockId}>
                    <span className={iconClassname}></span> {title}
                </AccordionItemTitle>
                <AccordionItemBody className={accordionItemBodyClassName}>
                    {this.props.children}
                </AccordionItemBody>
            </AccordionItem>
        );
    }
}