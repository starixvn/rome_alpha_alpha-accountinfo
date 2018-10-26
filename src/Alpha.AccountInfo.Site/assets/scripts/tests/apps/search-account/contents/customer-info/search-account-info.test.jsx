import React from 'react';
import ReactDOM from 'react-dom';
import renderer from 'react-test-renderer';
import { shallow } from 'enzyme';
import { MasterLevelInfo } from '../../helpers/master-level-info-sample';
import { Resources } from '../../helpers/resources-sample';
import SearchAccountInfo from 'SearchAccountInfo';
import CustomAnchorTag from 'CustomAnchorTag';
import { Tooltip } from 'react-tippy';

describe('SearchAccountInfo', () => {
    let props;
    beforeEach(() => {
        props = {
            customerInfo: MasterLevelInfo,
            resources: Resources,
            mobileView: false
        };
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<SearchAccountInfo {...props} />, div);
    });

    it('snapshot', () => {
        const component = renderer.create(<SearchAccountInfo {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    describe("User was closed", () => {
        beforeEach(() => {
            props.customerInfo.Closed = true;
        });

        it('return correct Css and Tooltip for closed account by Other reasons', () => {
            props.customerInfo.ClosedReason = 0;
            
            const component = shallow(<SearchAccountInfo {...props} />);
            expect(component.state('Css')).toEqual("closed-wrongPwdOthers");
            expect(component.state('Tootltip')).toEqual("This account was closed by failed login or account expired.");
        });

        it('return correct Css and Tooltip for closed account by Admin', () => {
            props.customerInfo.ClosedReason = 1;
            
            const component = shallow(<SearchAccountInfo {...props} />);
            expect(component.state('Css')).toEqual("closed-byAdmin");
            expect(component.state('Tootltip')).toEqual("This account was closed by admin.");
        });

        it('return correct Css and Tooltip for closed account by Upline', () => {
            props.customerInfo.ClosedReason = 2;
            
            const component = shallow(<SearchAccountInfo {...props} />);
            expect(component.state('Css')).toEqual("closed-byUpline");
            expect(component.state('Tootltip')).toEqual("This account was closed by upline.");
        });

        it('return correct Css and Tooltip for closed account by Wrong OTP/SecurityCode', () => {
            props.customerInfo.ClosedReason = 3;
            
            const component = shallow(<SearchAccountInfo {...props} />);
            expect(component.state('Css')).toEqual("closed-wrongOTPSC");
            expect(component.state('Tootltip')).toEqual("This account was closed by false input of OTP/Security Code.");
        });
    });

    it('return correct Css and Tooltip for suspended account', () => {
        props.customerInfo.Closed = false;
        props.customerInfo.Suspended = true;
        
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.state('Css')).toEqual("suspended");
        expect(component.state('Tootltip')).toEqual("This account was suspended.");
    });

    it('return correct Css and Tooltip for disabled account', () => {
        props.customerInfo.Closed = false;
        props.customerInfo.Suspended = false;
        props.customerInfo.Enabled = false;
        
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.state('Css')).toEqual("disabled");
        expect(component.state('Tootltip')).toEqual("This account was disabled.");
    });

    it('return correct Css and Tooltip for enable account', () => {
        props.customerInfo.Closed = false;
        props.customerInfo.Suspended = false;
        props.customerInfo.Enabled = true;
        
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.state('Css')).toEqual("enabled");
        expect(component.state('Tootltip')).toEqual("");
    });

    it("renders a `CustomAnchorTag` ", () => {
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.find(CustomAnchorTag).length).toBe(1);
    });

    it("renders a `Tooltip` if tooltip when User is not enabled", () => {
        props.customerInfo.Suspended = true;
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.find(Tooltip).length).toBe(1);
    });

    it(" does not renders a `Tooltip` when User is enabled ", () => {
        props.customerInfo.Closed = false;
        props.customerInfo.Suspended = false;
        props.customerInfo.Enabled = true;
        const component = shallow(<SearchAccountInfo {...props} />);
        expect(component.find(Tooltip).length).toBe(0);
    });
});