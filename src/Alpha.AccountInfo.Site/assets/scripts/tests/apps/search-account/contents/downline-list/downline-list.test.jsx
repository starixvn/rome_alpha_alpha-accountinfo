import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import DownlineList from 'DownlineList';
import TotalDownlines from 'TotalDownlines';
import { Resources } from '../../helpers/resources-sample';
import { DownlineListData } from '../../helpers/down-line-list-sample';

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
            data: DownlineListData,
            mobileView: false,
            searchAccountRoleId: 1,
            fetchCustomerInfoFromServer: jest.fn()
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