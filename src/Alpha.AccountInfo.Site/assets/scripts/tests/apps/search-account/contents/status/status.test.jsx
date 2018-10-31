import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import sinon from 'sinon';
import renderer from 'react-test-renderer';
import BettypeLevelStatus from 'BettypeLevelStatus';
import ConfirmStatus from 'ConfirmStatus';
import ProductLevelStatus from 'ProductLevelStatus';
import Status from 'Status';
import { Resources } from '../../helpers/resources-sample';
import * as accountInfoHelper from 'AccountInfoHelper';

describe('Status', () => {
    let props;
    global.accountInfoSiteAppPath = "/accountInfo-site";
    global.top.ageWnd = {
        open: jest.fn()
    };

    beforeEach(() => {
        props = {
            statusData:[ 
                {
                "ConfirmMessage": "Are you sure you want to change Close status for this customer?",
                "Disabled": false,
                "Checked": false,
                "Url": "/site-accountinfo/(S(nycmx2rjfmd5hmyywrpmvhzc))/SearchAccount/UpdateCustomerStatus",
                "JsonParams": "{\"SearchAccountId\":\"1382145\",\"SyncedCasino3rd\":\"True\",\"StatusName\":\"Closed\",\"StatusValue\":\"True\"}",
                "Text": "Closed",
                "DisplayOrder": 2,
                "Visible": true
              },{
                "Text": "Casino",
                "DisplayOrder": 60,
                "ProductId": 6,
                "Url": "/ProductStatus?productid=6&custId=1382145&custName=ACGUUS38&custRoleId=3",
                "Disabled": false,
                "Visible": true,
                "IsEditOnProductLevel": true,
                "Checked": true
            },
            {
                "Text": "Allbet",
                "DisplayOrder": 210,
                "ProductId": 21,
                "Url": "/site-accountinfo/ProductStatus?productid=21&custId=1382145&custName=ACGUUS38&custRoleId=3",
                "Disabled": false,
                "Visible": true,
                "IsEditOnProductLevel": false,
                "Checked": true
              }],
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<ProductLevelStatus {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<ProductLevelStatus {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    it('handleOpenPopup() called when clicked', () => {
        const spy = sinon.spy(ProductLevelStatus.prototype, 'handleOpenPopup');
        const wrapper = shallow(<ProductLevelStatus {...props} />)

        wrapper.find('#product-Allbet').simulate('click');
        expect(spy.calledOnce).toBeTruthy();
    });

    test.each([[true, 1], [false, 0]])(
        "correct call accountInfoHelper.resizePopupOnMobile()",
        (mobileView, expectedCallTimes) => {
            props.mobileView = mobileView;
            accountInfoHelper.resizePopupOnMobile = jest.fn();

            const wrapper = shallow(<ProductLevelStatus {...props} />);
            wrapper.find('#product-Allbet').simulate('click');
            expect(accountInfoHelper.resizePopupOnMobile).toBeCalledTimes(expectedCallTimes);
        }
    );
});