import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import Settings from 'Settings';
import CustomAnchorTag from 'CustomAnchorTag';

describe('Settings', () => {
    let props;
    let settingsShallow;

    const buildsettingsShallow = (data = []) => {
        props.settingData= data;
        if (!settingsShallow) {
            settingsShallow = shallow(<Settings {...props} />);
        }

        return settingsShallow;
    }

    beforeEach(() => {
        props = {
            settingData:  [],
            mobileView: false
        };

        settingsShallow = undefined;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<Settings {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<Settings {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([
        [[], 0],
        [[{
            "Visible": true,
            "Text": "Transfer",
            "DisplayOrder": 2,
            "Url": "/ex-main/_MemberInfo/TransferCondition/EditTransferCondition.aspx?custId=1382145&username=ACGUUS38"
          }], 1]
        ])(
        "correct render `CustomAnchorTag`",
        (settings, expectedCallTimes) => {
            buildsettingsShallow(settings);
            const wrapper = shallow(<Settings {...props} />);
            expect(wrapper.find(CustomAnchorTag).length).toBe(expectedCallTimes);
        }
    );
});
