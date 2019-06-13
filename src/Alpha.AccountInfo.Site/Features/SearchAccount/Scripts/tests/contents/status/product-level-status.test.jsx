import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import sinon from 'sinon';
import '../../../../../../__mocks__/global.mock';
import renderer from 'react-test-renderer';
import ProductLevelStatus from 'ProductLevelStatus';
import { Resources } from '../../helpers/resources-sample';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('ProductLevelStatus', () => {
    let props, wrapper, clock;

    beforeEach(() => {
        props = {
            status: {
                'Text': 'Allbet',
                'DisplayOrder': 210,
                'ProductId': 21,
                'Url': '/site-accountinfo/ProductStatus?productid=21&custId=1382145&custName=ACGUUS38&custRoleId=3',
                'Disabled': false,
                'Visible': true,
                'IsEditOnProductLevel': false,
                'Checked': true
            },
            resources: Resources,
            mobileView: false,
            fetchCustomerAllStatusFromServer: jest.fn()
        };
        clock = sinon.useFakeTimers();
    });

    afterEach(() => {
        props = null;
        wrapper = null;
        clock.restore();
    });

    const buildWrapper = () => {
        if (!wrapper) {
            wrapper = shallow(<ProductLevelStatus {...props} />);
        }

        return wrapper;
    };

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<ProductLevelStatus {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<ProductLevelStatus {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('handleOpenPopup() called when clicked', () => {
        const spy = sinon.spy(ProductLevelStatus.prototype, 'handleOpenPopup');
        buildWrapper();

        wrapper.find('#product-Allbet').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([[true, 1], [false, 0]])(
        'correct call accountInfoHelper.resizePopupOnMobile()',
        (mobileView, expectedCallTimes) => {
            props.mobileView = mobileView;
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            buildWrapper();
            wrapper.find('#product-Allbet').simulate('click');
            expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[true, 0], [false, 1]])(
        'call correct #window.top.ageWnd.open() base on status.Disabled',
        (disabled, expectedCallTimes) => {
            window.top.ageWnd.open = jest.fn();
            props.status.Disabled = disabled;
            buildWrapper();

            wrapper.instance().handleOpenPopup();
            expect(window.top.ageWnd.open).toBeCalledTimes(expectedCallTimes);
        });

    test.each([[true, false, 0], [true, true, 0], [false, true, 1], [false, false, 0]])(
        'correct call accountInfoHelper.resizePopupOnMobile()',
        (disabled, mobileView, expectedCallTimes) => {
            props.status.Disabled = disabled;
            props.mobileView = mobileView;
            global.top.bootboxConfirm = jest.fn();
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            buildWrapper();
            wrapper.instance().handleOpenPopup();
            expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[true, 1], [false, 0]])(
        'call correct #fetchCustomerAllStatusFromServer base on window.top.realoadLeftPanel',
        (realoadLeftPanel, expectedCallTimes) => {
            window.top.ageWnd.open = jest.fn();
            props.status.Disabled = false;
            buildWrapper();

            wrapper.instance().handleOpenPopup();
            window.top.realoadLeftPanel = realoadLeftPanel;
            clock.tick(1000);
            expect(props.fetchCustomerAllStatusFromServer).toHaveBeenCalledTimes(expectedCallTimes);
        });
});