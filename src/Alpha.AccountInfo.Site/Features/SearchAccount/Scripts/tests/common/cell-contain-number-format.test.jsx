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

    const cellContainNumberFormat = (value = 0, hrefLink = "192.168.1.1") => {
        props.value = value;
        props.hrefLink = hrefLink;

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

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CellContainNumberFormat {...props} />, div);
    });

    test("always renders a div", () => {
        const divs = cellContainNumberFormat().find("div");
        expect(divs.length).toBeGreaterThan(0);
    });

    test("always render a `NumberFormat`", () => {
        expect(cellContainNumberFormat().find(NumberFormat).length).toBe(1);
    });

    test.each([
        [-1, "192.168.1.1", 1],
        [-1, "", 0],
        [-1, null, 0],
        [0, "192.168.1.1", 1],
        [0, "", 0],
        [0, null, 0],
        [1, "192.168.1.1", 1],
        [1, "", 0],
        [1, null, 0]])(
        "renders correct `CustomAnchorTag`",
        (value, hrefLink, expectedCallTimes) => {
            const component = cellContainNumberFormat(value, hrefLink);
            expect(component.find(CustomAnchorTag).length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [-1, "192.168.1.1", 0],
        [-1, "", 1],
        [-1, null, 1],
        [0, "192.168.1.1", 0],
        [0, "", 1],
        [0, null, 1],
        [1, "192.168.1.1", 0],
        [1, "", 1],
        [1, null, 1]])(
        "renders correct a span contain value",
        (value, hrefLink, expectedCallTimes) => {
            const component = cellContainNumberFormat(value, hrefLink);
            expect(component.find('span').length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [-1, "192.168.1.1", 0],
        [-1, "", 0],
        [-1, null, 0],
        [0, "192.168.1.1", 1],
        [0, "", 1],
        [0, null, 1],
        [1, "192.168.1.1", 0],
        [1, "", 1],
        [1, null, 1]])(
        "renders correct `.number` class'",
        (value, hrefLink, expectedCallTimes) => {
            const component = cellContainNumberFormat(value, hrefLink);
            expect(component.find('.number').length).toBe(expectedCallTimes);
        }
    );


    test.each([
        [-1, "192.168.1.1", 0],
        [-1, "", 0],
        [-1, null, 0],
        [0, "192.168.1.1", 0],
        [0, "", 0],
        [0, null, 0],
        [1, "192.168.1.1", 1],
        [1, "", 0],
        [1, null, 0]])(
        "renders correct `.number-link` class'",
        (value, hrefLink, expectedCallTimes) => {
            const component = cellContainNumberFormat(value, hrefLink);
            expect(component.find('.number-link').length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [-1, "192.168.1.1", 1],
        [-1, "", 1],
        [-1, null, 1],
        [0, "192.168.1.1", 0],
        [0, "", 0],
        [0, null, 0],
        [1, "192.168.1.1", 0],
        [1, "", 0],
        [1, null, 0]])(
        "renders correct `.number-red` class'",
        (value, hrefLink, expectedCallTimes) => {
            const component = cellContainNumberFormat(value, hrefLink);
            expect(component.find('.number-red').length).toBe(expectedCallTimes);
        }
    );
});