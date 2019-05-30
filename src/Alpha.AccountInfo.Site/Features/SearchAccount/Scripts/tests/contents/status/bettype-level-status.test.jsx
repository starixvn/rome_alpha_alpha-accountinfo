import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import BettypeLevelStatus from 'BettypeLevelStatus';
import { Resources } from '../../helpers/resources-sample';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('BettypeLevelStatus', () => {
    let props;
    global.accountInfoSiteAppPath = "/accountInfo-site";
    global.top.ageWnd = {
        open: jest.fn()
    };

    beforeEach(() => {
        props = {
            status: {
                "Text": "Casino",
                "DisplayOrder": 60,
                "ProductId": 6,
                "Url": "/ProductStatus?productid=6&custId=1382145&custName=ACGUUS38&custRoleId=3",
                "Disabled": false,
                "Visible": true,
                "IsEditOnProductLevel": true,
                "Checked": true
            },
            resources: Resources,
            mobileView: false
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<BettypeLevelStatus {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<BettypeLevelStatus {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    it('handleOpenPopup() called when clicked', () => {
        const spy = sinon.spy(BettypeLevelStatus.prototype, 'handleOpenPopup');
        const wrapper = shallow(<BettypeLevelStatus {...props} />)

        wrapper.find('#product-Casino').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([[true, 1], [false, 0]])(
        "correct call accountInfoHelper.resizePopupOnMobile()",
        (mobileView, expectedCallTimes) => {
            props.mobileView = mobileView;
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            const wrapper = shallow(<BettypeLevelStatus {...props} />);
            wrapper.find('#product-Casino').simulate('click');
            expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[true, 1], [false, 0]])(
        "correct call accountInfoHelper.mobileScale()",
        (mobileView, expectedCallTimes) => {
            props.mobileView = mobileView;
            accountInfoHelper.mobileScale = jest.fn();

            const wrapper = shallow(<BettypeLevelStatus {...props} />);
            wrapper.find('#product-Casino').simulate('click');
            expect(accountInfoHelper.mobileScale).toBeCalledTimes(expectedCallTimes);
        }
    );

    test.each([[8, 530], [22, 200], [21, 300], [33, 400], [15, 200]])(
        "correct popup height",
        (productId, expectedHeight) => {
            props.status.ProductId = productId;
            const wrapper = shallow(<BettypeLevelStatus {...props} />);
            expect(wrapper.state('height')).toEqual(expectedHeight);
        }
    );
});