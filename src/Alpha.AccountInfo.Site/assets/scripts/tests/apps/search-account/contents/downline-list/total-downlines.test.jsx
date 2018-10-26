import React from 'react';
import ReactDOM from 'react-dom';
import { mount } from "enzyme";
import TotalDownlines from 'TotalDownlines';
import { Resources } from '../../helpers/resources-sample';
import { Tooltip } from 'react-tippy';

describe('TotalDownlines', () => {
    let props;
    let totalDownlinesMount;

    const buildTotalDownlines = () => {
        if (!totalDownlinesMount) {
            totalDownlinesMount = mount(
                <TotalDownlines {...props} />
            );
        }
        return totalDownlinesMount;
    }

    beforeEach(() => {
        props = {
            resources: Resources,
            levelName: 'Master',
            totalActive: 100,
            totalClosed: 1,
            totalSuspended: 5
        };

        totalDownlinesMount = undefined;
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<TotalDownlines {...props} />, div);
    });

    test('always render a `Tootltip`', () => {
        buildTotalDownlines();
        expect(totalDownlinesMount.find(Tooltip).length).toBe(1);
    });
});