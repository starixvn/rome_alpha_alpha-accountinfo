import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from "enzyme";
import renderer from 'react-test-renderer';
import UnsuspendViewList from 'UnsuspendViewList';
import * as axios from "axios";

describe('UnsuspendViewList', () => {
    jest.mock("axios");
    let props;
    axios.get.mockImplementation(() => Promise.resolve({
        data: {
            "SearchAccountId": 36204210,
            "SearchAccountName": "davidtest13May01",
            "IsAffected": true,
            "Data": {
                "Levels": [
                    {
                        "DownlineUsernames": [
                            "davidtest13May0101",
                            "davidtest13May0102",
                            "davidtest13May0103"
                        ],
                        "RoleId": 2
                    },
                    {
                        "DownlineUsernames": [
                            "davidtest13May0101001",
                            "davidtest13May0101002",
                            "davidtest13May0101003"
                        ],
                        "RoleId": 1
                    }
                ],
                "TotalRows": 6
            },
            "AccountInfoUrl": "/site-accountinfo/(S(dzdoumiv1x2xajfceulo1rgn))/"
        }}));
    beforeEach(() => {
        props = {
            customerInfo: Super.ViewMasterInfo,
            resources: Resources
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<UnsuspendViewList {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<UnsuspendViewList {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });
});