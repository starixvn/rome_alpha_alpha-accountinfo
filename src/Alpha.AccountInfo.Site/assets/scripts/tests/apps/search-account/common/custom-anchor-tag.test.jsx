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

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CustomAnchorTag />, div);
    });

    it('snapshot', () => {
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

        describe("accountInfoHelper.closeLeftPanelOnMobile()", () => {
            accountInfoHelper.closeLeftPanelOnMobile = jest.fn();

            it('not call if `mobileView` is false', () => {
                props.mobileView = false;
    
                const wrapper = shallow(<CustomAnchorTag {...props} />)
                wrapper.find('a').simulate('click');
                expect(accountInfoHelper.closeLeftPanelOnMobile).toBeCalledTimes(0);
            });
    
            it('call if `mobileView` is true', () => {
                props.mobileView = true;
    
                const wrapper = shallow(<CustomAnchorTag {...props} />)
                wrapper.find('a').simulate('click');
                expect(accountInfoHelper.closeLeftPanelOnMobile).toBeCalledTimes(1);
            });
        });
    });
});