import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import DownlineList from 'DownlineList';
import TotalIndirectDownlines from 'TotalIndirectDownlines';
import { Resources } from '../../helpers/resources-sample';
import { DownlineListData } from '../../helpers/down-line-list-sample';

describe('DownlineList', () => {
    let props;
    let downlineListMount;

    const buildDownlineList = (searchAccountRoleId = true) => {
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
            downlineListData: DownlineListData,
            mobileView: false,
            showTotalIndirectDownlines: false,
            fetchDownlineListFromServer:jest.fn(),
            fetchCustomerInfoFromServer: jest.fn()
        };

        downlineListMount = undefined;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<DownlineList {...props} />, div);
    });

    test.each([[3, 1],  [2, 0]])(
        "renders corret `TotalDownlines` callTimes",
        (searchAccountRoleId, expectedCallTimes) => {
            buildDownlineList(searchAccountRoleId);
            expect(downlineListMount.find(TotalIndirectDownlines).length).toBe(expectedCallTimes);
        }
    );
});