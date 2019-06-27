import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import CustomAnchorTag from 'CustomAnchorTag';
import * as accountInfoHelper from 'AccountInfoHelper';
import '../../../../../__mocks__/global.mock';

describe('CustomAnchorTag', () => {
    let props;
    global._AssetCommonCssLink = '';

    global.HTMLElement = window.HTMLElement;
    beforeEach(() => {
        props = {
            title: undefined,
            href: undefined,
            classNames: undefined,
            children: undefined,
            mobileView: undefined
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CustomAnchorTag />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<CustomAnchorTag {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    describe('handleClick()', () => {
        test.each([[true, 0], [false, 1]])(
            'correct callblock main iframe by browser',
            (isIE, expectedCallTimes) => {
                accountInfoHelper.closeLeftPanelOnMobile = jest.fn();
                window.top.alpha.core.isIe.mockReturnValueOnce(isIE);
                const wrapper = shallow(<CustomAnchorTag {...props} />);
                const event = Object.assign(jest.fn(), { preventDefault: () => { } });
                wrapper.find('a').simulate('click', event);

                expect(global.$.fn.block).toBeCalledTimes(expectedCallTimes);
            });

        test.each([[true, 1], [false, 0]])(
            'correct call accountInfoHelper.closeLeftPanelOnMobile()',
            (mobileView, expectedCallTimes) => {
                accountInfoHelper.closeLeftPanelOnMobile = jest.fn();
                props.mobileView = mobileView;

                const wrapper = shallow(<CustomAnchorTag {...props} />);
                const event = Object.assign(jest.fn(), { preventDefault: () => { } });
                wrapper.find('a').simulate('click', event);
                expect(accountInfoHelper.closeLeftPanelOnMobile).toBeCalledTimes(expectedCallTimes);
            }
        );
    });
});