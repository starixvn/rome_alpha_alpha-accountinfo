import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import DownlineList from 'DownlineList';
import TotalDownlines from 'TotalDownlines';
import { Resources } from '../../helpers/resources-sample';

describe('DownlineList', () => {
    let props;
    let downlineListMount;

    const buildDownlineList = (searchAccountRoleId = 2) => {
        props.searchAccountRoleId= searchAccountRoleId;
        if (!downlineListMount) {
            downlineListMount = mount(
                <DownlineList {...props} />
            );
        }
        return downlineListMount;
    }

    beforeEach(() => {
        props = {
            resources: Resources,
            data: {
                "DownlineList": [
                  {
                    "Username": "ACGUUS3838"
                  },
                  {
                    "Username": "ACGUUS3888"
                  },
                  {
                    "Username": "ACGUUS38ZZ"
                  }
                ],
                "TotalStatusAgent": {
                  "TotalActive": 1,
                  "TotalClosed": 2,
                  "TotalSuspend": 0
                },
                "TotalStatusMember": {
                  "TotalActive": 69,
                  "TotalClosed": 137,
                  "TotalSuspend": 0
                }
              },
            mobileView: false,
            searchAccountRoleId: 1,
            handleSearchFromUserLevel: jest.fn()
        };

        downlineListMount = undefined;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<DownlineList {...props} />, div);
    });

    test.each([[3, 2],  [2, 1]])(
        "renders corret `TotalDownlines` callTimes",
        (searchAccountRoleId, expectedCallTimes) => {
            buildDownlineList(searchAccountRoleId);
            expect(downlineListMount.find(TotalDownlines).length).toBe(expectedCallTimes);
        }
    );
});