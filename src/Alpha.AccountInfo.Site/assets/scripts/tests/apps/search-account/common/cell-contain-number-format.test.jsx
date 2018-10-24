import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import renderer from 'react-test-renderer';
import NumberFormat from 'react-number-format';
import CellContainNumberFormat from 'CellContainNumberFormat';
import CustomAnchorTag from 'CustomAnchorTag';

describe('CellContainNumberFormat', () => {
    let props;
    let mountCellContainNumberFormat;

    const cellContainNumberFormat = () => {
        if (!mountCellContainNumberFormat) {
            mountCellContainNumberFormat = mount(
                <CellContainNumberFormat {...props} />
            );
        }
        return mountCellContainNumberFormat;
    }

    const renderSnapshot = () => {
        const component = renderer.create(<CellContainNumberFormat {...props} />);
        return component.toJSON();
    }

    beforeEach(() => {
        props = {
            value: undefined,
            mobileView: true,
            classNames: "col col-right",
            hrefLink: undefined
        };

        mountCellContainNumberFormat = undefined;
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CellContainNumberFormat {...props} />, div);
    });

    it("always renders a div", () => {
        const divs = cellContainNumberFormat().find("div");
        expect(divs.length).toBeGreaterThan(0);
    });

    it("renders a `NumberFormat`", () => {
        expect(cellContainNumberFormat().find(NumberFormat).length).toBe(1);
    });

    describe("when `value` equal 0", () => {
        beforeEach(() => {
            props.value = 0;
        });

        it('snapshot', () => {
            expect(renderSnapshot()).toMatchSnapshot();
        });

        it('render class `number`', () => {
            props.value = 0;
            expect(cellContainNumberFormat().find('.number').length).toBe(1);
        });

        it("does not renders a `CustomAnchorTag` ", () => {
            expect(cellContainNumberFormat().find(CustomAnchorTag).length).toBe(0);
        });

        it("renders a span contain value ", () => {
            expect(cellContainNumberFormat().find('span').length).toBe(1);
        });
    });

    describe("when `value` greater than 0", () => {
        beforeEach(() => {
            props.value = 50;
        });

        describe("And when `href` is null", () => {
            beforeEach(() => {
                props.hrefLink = null;
            });
    
            it('snapshot', () => {
                expect(renderSnapshot()).toMatchSnapshot();
            });

            it('render class `number`', () => {
                expect(cellContainNumberFormat().find('.number').length).toBe(1);
            });

            it("renders a span contain value ", () => {
                expect(cellContainNumberFormat().find('span').length).toBe(1);
            });

            it("does not renders a `CustomAnchorTag` ", () => {
                expect(cellContainNumberFormat().find(CustomAnchorTag).length).toBe(0);
            });    
        });

        describe("And when `href` is empty", () => {
            beforeEach(() => {
                props.hrefLink = '';
            });
    
            it('snapshot', () => {
                expect(renderSnapshot()).toMatchSnapshot();
            });

            it('render class `number`', () => {
                expect(cellContainNumberFormat().find('.number').length).toBe(1);
            });

            it("renders a span contain value ", () => {
                expect(cellContainNumberFormat().find('span').length).toBe(1);
            });

            it("does not renders a `CustomAnchorTag` ", () => {
                expect(cellContainNumberFormat().find(CustomAnchorTag).length).toBe(0);
            });    
        });

        describe("And when `href` is not null/empty", () => {
            beforeEach(() => {
                props.hrefLink = "192.168.1.1";
            });

            it('snapshot', () => {
                expect(renderSnapshot()).toMatchSnapshot();
            });
    
            it("renders a `CustomAnchorTag`", () => {
                expect(cellContainNumberFormat().find(CustomAnchorTag).length).toBe(1);
            });
    
            it('render class `number-link`', () => {
                expect(cellContainNumberFormat().find('.number-link').length).toBe(1);
            });

            it("does not renders span ", () => {
                expect(cellContainNumberFormat().find('span').length).toBe(0);
            });
    
        });
    });

    describe("when `value` less than 0", () => {
        beforeEach(() => {
            props.value = -23;
        });

        it('snapshot', () => {
            expect(renderSnapshot()).toMatchSnapshot();
        });

        it('render class `number-red`', () => {
            expect(cellContainNumberFormat().find('.number-red').length).toBe(1);
        });

        it("does not renders a `CustomAnchorTag` ", () => {
            expect(cellContainNumberFormat().find(CustomAnchorTag).length).toBe(0);
        });

        it("renders a span contain value ", () => {
            expect(cellContainNumberFormat().find('span').length).toBe(1);
        });
    });
});