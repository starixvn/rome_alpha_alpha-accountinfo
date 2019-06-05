import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from 'enzyme';
import DownlineList from 'DownlineList';
import TotalIndirectDownlines from 'TotalIndirectDownlines';
import { Resources } from '../../helpers/resources-sample';
import { DownlineListData } from '../../helpers/down-line-list-sample';

describe('DownlineList', () => {
    let props;
    let downlineListMount;

    const buildDownlineList = (searchAccountRoleId = true) => {
        props.searchAccountRoleId = searchAccountRoleId;
        if (!downlineListMount) {
            downlineListMount = mount(
                <DownlineList {...props} />
            );
        }
        return downlineListMount;
    };

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
        'renders correct #TotalDownlines callTimes',
        (searchAccountRoleId, expectedCallTimes) => {
            buildDownlineList(searchAccountRoleId);
            expect(downlineListMount.find(TotalIndirectDownlines).length).toBe(expectedCallTimes);
        }
    );

    test('content is null when device is desktop and downlineListData is null', () => {
        props.mobileView = false;
        props.downlineListData = null;
        buildDownlineList(3);
        expect(downlineListMount.html() === null).toBeTruthy();
    });

    test('content is noinfo when device is desktop and downlineListData is null', () => {
        props.mobileView = true;
        props.downlineListData = null;
        buildDownlineList(3);
        expect(downlineListMount.find('.no-information').length).toBe(1);
    });

    test('content is noinfo when TotalDownlines is 0', () => {
        props.mobileView = true;
        props.downlineListData = {
            TotalDownlines: []
        };
        buildDownlineList(3);
        expect(downlineListMount.find('.no-information').length).toBe(1);
    });

});