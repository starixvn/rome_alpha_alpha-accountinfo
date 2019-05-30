import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import '../../../../../../__mocks__/global.mock';
import BettypeLevelStatus from 'BettypeLevelStatus';
import ConfirmStatus from 'ConfirmStatus';
import ProductLevelStatus from 'ProductLevelStatus';
import Status from 'Status';
import { Resources } from '../../helpers/resources-sample';

describe('Status', () => {
    let props;
    global.accountInfoSiteAppPath = "/accountInfo-site";
    global.top.ageWnd = {
        open: jest.fn()
    };

    beforeEach(() => {
        props = {
            statusData:[],
            resources: Resources,
            mobileView: false,
            fetchCustomerInfoFromServer: jest.fn()
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<Status {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<Status {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([
        [{}, 0], 
        [{
            "Disabled": false,
            "Checked": false,
            "Url": "/site-accountinfo/(S(nycmx2rjfmd5hmyywrpmvhzc))/SearchAccount/UpdateCustomerStatus",
            "JsonParams": "{\"SearchAccountId\":\"1382145\",\"SyncedCasino3rd\":\"True\",\"StatusName\":\"Closed\",\"StatusValue\":\"True\"}",
            "Text": "Closed",
            "DisplayOrder": 2,
            "Visible": true
          }, 0], 
        [{
            "ConfirmMessage": "Are you sure you want to change Close status for this customer?",
            "Disabled": false,
            "Checked": false,
            "Url": "/site-accountinfo/(S(nycmx2rjfmd5hmyywrpmvhzc))/SearchAccount/UpdateCustomerStatus",
            "JsonParams": "{\"SearchAccountId\":\"1382145\",\"SyncedCasino3rd\":\"True\",\"StatusName\":\"Closed\",\"StatusValue\":\"True\"}",
            "Text": "Closed",
            "DisplayOrder": 2,
            "Visible": true
          }, 1]])(
        "correct render ConfirmStatus",
        (status, expectedCallTimes) => {
            props.statusData.push(status);
            const wrapper = shallow(<Status {...props} />);
            expect(wrapper.find(ConfirmStatus).length).toBe(expectedCallTimes);
        }
    );

    test.each([
        [{}, 0], 
        [{
            "Text": "Casino",
            "DisplayOrder": 60,
            "ProductId": 6,
            "Url": "/ProductStatus?productid=6&custId=1382145&custName=ACGUUS38&custRoleId=3",
            "Disabled": false,
            "Visible": true,
            "IsEditOnProductLevel": false,
            "Checked": true
        }, 0], 
        [{
            "Text": "Casino",
            "DisplayOrder": 60,
            "ProductId": 6,
            "Url": "/ProductStatus?productid=6&custId=1382145&custName=ACGUUS38&custRoleId=3",
            "Disabled": false,
            "Visible": true,
            "IsEditOnProductLevel": true,
            "Checked": true
        }, 1]])(
        "correct render ProductLevelStatus",
        (status, expectedCallTimes) => {
            props.statusData.push(status);
            const wrapper = shallow(<Status {...props} />);
            expect(wrapper.find(ProductLevelStatus).length).toBe(expectedCallTimes);
        }
    );
    
    test.each([
        [{
            "Text": "Allbet",
            "DisplayOrder": 210,
            "ProductId": 21,
            "Url": "/site-accountinfo/ProductStatus?productid=21&custId=1382145&custName=ACGUUS38&custRoleId=3",
            "Disabled": false,
            "Visible": true,
            "IsEditOnProductLevel": true,
            "Checked": true
        }, 0], 
        [{
            "Text": "Allbet",
            "DisplayOrder": 210,
            "ProductId": 21,
            "Url": "/site-accountinfo/ProductStatus?productid=21&custId=1382145&custName=ACGUUS38&custRoleId=3",
            "Disabled": false,
            "Visible": true,
            "IsEditOnProductLevel": false,
            "Checked": true
        }, 1]])(
        "correct render BettypeLevelStatus",
        (status, expectedCallTimes) => {
            props.statusData.push(status);
            const wrapper = shallow(<Status {...props} />);
            expect(wrapper.find(BettypeLevelStatus).length).toBe(expectedCallTimes);
        }
    );
});