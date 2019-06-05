import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from 'enzyme';
import TotalIndirectDownlines from 'TotalIndirectDownlines';
import { Resources } from '../../helpers/resources-sample';
import { DownlineListData } from '../../helpers/down-line-list-sample';
import { Tooltip } from 'react-tippy';

describe('TotalDownlines', () => {
    let props;
    let totalDownlinesMount;

    const buildTotalDownlines = () => {
        if (!totalDownlinesMount) {
            totalDownlinesMount = mount(
                <TotalIndirectDownlines {...props} />
            );
        }
        return totalDownlinesMount;
    }

    beforeEach(() => {
        props = {
            resources: Resources,
            totalIndirectDownline: DownlineListData.TotalDownlines[0]
        };

        totalDownlinesMount = undefined;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<TotalIndirectDownlines {...props} />, div);
    });

    test('always render a `Tootltip`', () => {
        buildTotalDownlines();
        expect(totalDownlinesMount.find(Tooltip).length).toBe(1);
    });
});