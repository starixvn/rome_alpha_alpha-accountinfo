import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import CustomAnchorTag from 'CustomAnchorTag';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('CustomAnchorTag', () => {
    let props;
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

    describe("handleClick()" , () => {
        it('called when clicked', () => {
            const spy = sinon.spy(CustomAnchorTag.prototype, 'handleOnclick');
            const wrapper = shallow(<CustomAnchorTag />)
    
            wrapper.find('a').simulate('click')
            expect(spy.calledOnce).toBeTruthy();
        })


        test.each([[true, 1], [false, 0]])(
            "correct call accountInfoHelper.closeLeftPanelOnMobile()",
            (mobileView, expectedCallTimes) => {
                accountInfoHelper.closeLeftPanelOnMobile = jest.fn();
                props.mobileView = mobileView;

                const wrapper = shallow(<CustomAnchorTag {...props} />)
                wrapper.find('a').simulate('click');
                expect(accountInfoHelper.closeLeftPanelOnMobile).toBeCalledTimes(expectedCallTimes);
            }
        );
    });
});