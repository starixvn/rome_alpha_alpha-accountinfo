import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from 'enzyme';
import renderer from 'react-test-renderer';
import CellContainNumberFormat from 'CellContainNumberFormat';
import BalanceInfoRow from 'BalanceInfoRow';

describe('BalanceInfoRow', () => {
    let props;
    beforeEach(() => {
        props = {
            value: undefined,
            mobileView: undefined,
            classNames: undefined,
            hrefLink: undefined
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<BalanceInfoRow />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<BalanceInfoRow {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('always render a `CellContainNumberFormat`', () => {
        const balanceInfoRow = mount(<BalanceInfoRow {...props} />);
        expect(balanceInfoRow.find(CellContainNumberFormat).length).toBe(1);
    });
});