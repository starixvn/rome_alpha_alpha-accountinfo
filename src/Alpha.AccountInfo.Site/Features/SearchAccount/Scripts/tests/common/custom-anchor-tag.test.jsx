import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import CustomAnchorTag from 'CustomAnchorTag';
import * as accountInfoHelper from 'AccountInfoHelper';
import { JSDOM } from 'jsdom';
import $ from 'jquery';

describe('CustomAnchorTag', () => {
    let props;
    global.$ = global.jQuery = $;
    global.$.fn.block =jest.fn();
    global.$.unblock =jest.fn();
    global._AssetCommonCssLink = '';
    global.top.alpha = {
        core: {
            isIe: jest.fn()
        }
    };



//     const jsdom = new JSDOM('<!doctype html><html><body><iframe id="main"><html><head></head><body></body></html></iframe></div></body></html>', {
//         url: "http://test.com"
//       });
//       const { window } = jsdom;

//       global.document = window.document;
// global.window = window;
// global.navigator = {
//   userAgent: 'node.js',
// };

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

    describe("handleClick()" , () => {
        it('called when clicked', () => {
            const spy = sinon.spy(CustomAnchorTag.prototype, 'handleOnclick');
            const wrapper = shallow(<CustomAnchorTag />)
            const event = Object.assign(jest.fn(), {preventDefault: () => {}});
            wrapper.find('a').simulate('click', event);
            expect(spy.calledOnce).toBeTruthy();
        })


        test.each([[true, 1], [false, 0]])(
            "correct call accountInfoHelper.closeLeftPanelOnMobile()",
            (mobileView, expectedCallTimes) => {
                accountInfoHelper.closeLeftPanelOnMobile = jest.fn();
                props.mobileView = mobileView;

                const wrapper = shallow(<CustomAnchorTag {...props} />)
                const event = Object.assign(jest.fn(), {preventDefault: () => {}});
                wrapper.find('a').simulate('click', event);
                expect(accountInfoHelper.closeLeftPanelOnMobile).toBeCalledTimes(expectedCallTimes);
            }
        );
    });
});