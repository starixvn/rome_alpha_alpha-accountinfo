import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import { RowContent, ColumnContent } from 'UnsuspendMultipleComponent';
import { resources } from '../unspend-downlines-resources.mock';
import * as _ from 'lodash';

const Data = {
    UnsuspendMultipleMaster: {
        "SearchAccountId": 36204210,
        "Username": "davidtest13May01",
        "Suspended": true,
        "Downlines": [
            {
                "SearchAccountId": 36204210,
                "DownlineRoleId": 2,
                "AffectedDownlines": 2,
                "UnchangedDownlines": 1
            },
            {
                "SearchAccountId": 36204210,
                "DownlineRoleId": 1,
                "AffectedDownlines": 0,
                "UnchangedDownlines": 3
            }
        ]
    },
    UnsuspendMultipleAgent: {
        "SearchAccountId": 36204213,
        "Username": "davidtest13May0101",
        "Suspended": true,
        "Downlines": [
            {
                "SearchAccountId": 36204213,
                "DownlineRoleId": 1,
                "AffectedDownlines": 0,
                "UnchangedDownlines": 3
            }
        ]
    },
    UnsuspendMultipleMember: {
        "SearchAccountId": 36204216,
        "Username": "davidtest13May0101001",
        "Suspended": false,
        "Downlines": []
    }
}

describe('UnsuspendMultipleComponent', () => {
    describe("RowContent", () => {
        let props;

        beforeEach(() => {
            props = {
                originaldataRow: _.cloneDeep(Data.UnsuspendMultipleMaster),
                dataRow: _.cloneDeep(Data.UnsuspendMultipleMaster),
                searchAccountRoleId: 2,
                index: 1,
                handleChange: jest.fn(),
                redirectViewListDetail: jest.fn(),
                resources
            };
        });

        test('renders without crashing', () => {
            const table = document.createElement('table');
            const body = table.createTBody();
            ReactDOM.render(<RowContent {...props} />, body);
        });

        test('Snapshot', () => {
            const component = renderer.create(<RowContent {...props} />);
            expect(component.toJSON()).toMatchSnapshot();
        });

        test.each([[true, 'Suspended', '.suspend-button'], [false, 'Unsuspended', '.unsuspend-button']])(
            "always renders correct text and button class and call #handleChange when click button",
            (currentSuspended, expectedText, expectedButtonClass) => {
                props.dataRow.Suspended = currentSuspended;
                const wrapper = shallow(<RowContent {...props} />);
                expect(wrapper.find(expectedButtonClass).length).toBe(1);
                expect(wrapper.find(expectedButtonClass).text()).toBe(expectedText);

                const event = Object.assign(jest.fn(), {preventDefault: () => {}});
                wrapper.find(expectedButtonClass).simulate('click', event);
                expect(props.handleChange).toBeCalledTimes(1);
            }
        );

        test.each([
            [3, Data.UnsuspendMultipleMaster, 4],
            [2, Data.UnsuspendMultipleAgent, 4],
            [1, Data.UnsuspendMultipleMember, 2]])(
                "always renders correct numerous of columns",
                (searchAccountRoleId, data, expectedColumns) => {
                    props.searchAccountRoleId = searchAccountRoleId;
                    props.originaldataRow = props.dataRow = data;
                    const wrapper = shallow(<RowContent {...props} />);
                    expect(wrapper.find('td').length).toBe(expectedColumns);
                }
            );

        test('Show #ColumnContent When change from suspened from unsuspended', () => {
            props.dataRow.Suspended = !props.originaldataRow.Suspended;
            const wrapper = shallow(<RowContent {...props} />);
            expect(wrapper.find(ColumnContent).length).toBeGreaterThanOrEqual(1);
        });

        test('Show #All text When change from unsuspended from suspened', () => {
            props.originaldataRow.Suspended = false;
            props.dataRow.Suspended = !props.originaldataRow.Suspended;
            const wrapper = shallow(<RowContent {...props} />);
            expect(wrapper.find('td').at(2).text()).toBe('All');
        });
    });

    describe("ColumnContent", () => {
        let props;

        beforeEach(() => {
            props = {
                dataRow: Data.UnsuspendMultipleMaster,
                levels: 2,
                isAffected: true,
                redirectViewListDetail: jest.fn()
            };
        });

        test('renders without crashing', () => {
            const table = document.createElement('table');
            const tbody = table.createTBody();
            const td = tbody.appendChild(document.createElement('td'));
            ReactDOM.render(<ColumnContent {...props} />, td);
        });

        test('Snapshot', () => {
            const component = renderer.create(<ColumnContent {...props} />);
            expect(component.toJSON()).toMatchSnapshot();
        });

        test.each([[true, '2'], [false, '1']])(
            "always renders correct affected/remainUnchanged downlines and call #redirectViewListDetail when click href",
            (isAffected, expectedValue) => {
                props.isAffected = isAffected;
                const wrapper = shallow(<ColumnContent {...props} />);
                expect(wrapper.find('a').at(0).text()).toBe(expectedValue);
                
                const event = Object.assign(jest.fn(), {preventDefault: () => {}});
                wrapper.find('a').at(0).simulate('click', event);
                expect(props.redirectViewListDetail).toBeCalledTimes(1);
            }
        );

        test('Show #a href When downlines greater than 0', () => {
            const wrapper = shallow(<ColumnContent {...props} />);
            expect(wrapper.find('a').length).toBe(1);
        });

        test('Show #text only When downlines equal 0', () => {
            const wrapper = shallow(<ColumnContent {...props} />);
            expect(wrapper.contains('a')).toBeFalsy();
            expect(wrapper.find('span').at(0).text()).toContain('2');
        });
    });
});