import React from 'react';
import ReactDOM from 'react-dom';
import '../../../../../__mocks__/global.mock';
import { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import { ColumnContentByLevel, BodyRowContent, FooterContent } from 'UnsuspendSingleComponent';
import  {resources} from '../unspend-downlines-resources.mock';

describe('UnsuspendSingleComponent', () => {
    describe('ColumnContentByLevel', () => {
        let props;

        beforeEach(() => {
            props = {
                downlines: null,
                isAffected: true,
                downlineRoleId: 2,
                redirectViewListDetail: jest.fn()
            };
        });

        test('renders without crashing', () => {
            const div = document.createElement('div');
            ReactDOM.render(<ColumnContentByLevel {...props} />, div);
        });

        test.each([[0], [49]])(
            'Snapshot', (downlines) => {
                props.downlines = downlines;
                const component = renderer.create(<ColumnContentByLevel {...props} />);
                expect(component.toJSON()).toMatchSnapshot();
            }
        );

        test.each([[0, 0], [49, 1]])(
            'always renders #a callTimes',
            (downlines, expectedCallTimes) => {
                props.downlines = downlines;
                const wrapper = shallow(<ColumnContentByLevel {...props} />);
                expect(wrapper.find('a').length).toBe(expectedCallTimes);
            }
        );

        test('correct call #redirectViewListDetail', () => {
            props.downlines = 49;
            const wrapper = shallow(<ColumnContentByLevel {...props} />);
            const event = Object.assign(jest.fn(), {preventDefault: () => {}});
            wrapper.find('a').simulate('click', event);
            expect(props.redirectViewListDetail).toBeCalledTimes(1);
        });
    });

    describe('BodyRowContent', () => {
        let props;

        beforeEach(() => {
            props = {
                level:{
                    'RoleId': 2,
                    'AffectedDownlines': 2,
                    'UnchangedDownlines': 1
                  },
                resources: resources,
                redirectViewListDetail: jest.fn()
            };
        });

        test('renders without crashing', () => {
            const table = document.createElement('table');
            const body = table.createTBody();
            ReactDOM.render(<BodyRowContent {...props} />, body);
        });

        test('Snapshot', () => {
            const component = renderer.create(<BodyRowContent {...props} />);
            expect(component.toJSON()).toMatchSnapshot();
        });

        test('renders #ColumnContentByLevel component', () => {
            const wrapper = shallow(<BodyRowContent {...props} />)
            expect(wrapper.find(ColumnContentByLevel).length).toBeGreaterThanOrEqual(1);
        });
    });

    describe('FooterContent', () => {
        let props;

        beforeEach(() => {
            props = {
                affectedDownlinesTotal: 5,
                unchangedDownlinesTotal: 10,
                redirectViewListDetail: jest.fn()
            };
        });

        test('renders without crashing', () => {
            const table = document.createElement('table');
            ReactDOM.render(<FooterContent {...props} />, table);
        });

        test('Snapshot', () => {
            const component = renderer.create(<FooterContent {...props} />);
            expect(component.toJSON()).toMatchSnapshot();
        });

        test('renders #ColumnContentByLevel component', () => {
            const wrapper = shallow(<FooterContent {...props} />)
            expect(wrapper.find(ColumnContentByLevel).length).toBeGreaterThanOrEqual(1);
        });

        test('Should render #BodyRowContent component', () => {
            props.affectedDownlinesTotal = 2;
            props.unchangedDownlinesTotal = 2;

            const wrapper = shallow(<FooterContent {...props} />)
            expect(wrapper.find(ColumnContentByLevel).length).toBeGreaterThanOrEqual(1);
        });

        test('Should render #NoData text when affectedDownlinesTotal == 0 and unchangedDownlinesTotal = 0', () => {
            props.affectedDownlinesTotal = 0;
            props.unchangedDownlinesTotal = 0;

            const wrapper = shallow(<FooterContent {...props} />)
            expect(wrapper.find('td').text()).toBe('There is no data.');
        });
    });
});