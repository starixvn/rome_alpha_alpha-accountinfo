import React from 'react';
import ReactDOM from 'react-dom';
import { shallow } from 'enzyme';
import renderer from 'react-test-renderer';
import CustomizedAccordionItem from 'CustomizedAccordionItem';
import { Resources } from '../helpers/resources-sample';
import { Super } from '../helpers/actor-is-super-sample';
import LoginInfo from 'LoginInfo';

describe('CustomizedAccordionItem', () => {
    let props;
    beforeEach(() => {
        props = {
            title: 'Login Info',
            iconClassname: 'icon-login',
            accordionItemBodyClassName: 'icon-login',
            expanded: true,
            handleClick: jest.fn(),
            blockId: 'status',
            children: <LoginInfo
                resources={Resources}
                loginInfo={Super.ViewMasterInfo.LoginInfo}
                handleOpenIPInfo={jest.fn()}
            />
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(<CustomizedAccordionItem {...props} />, div);
    });

    test('snapshot', () => {
        const component = renderer.create(<CustomizedAccordionItem {...props} />);
        expect(component.toJSON()).toMatchSnapshot();
    });

    test.each([[true, 1],[false, 0]])(
        'correct render `expanded` class',
        (expanded, expectedCallTimes) => {
            props.expanded = expanded;
            const wrapper = shallow(<CustomizedAccordionItem {...props} />);
            expect(wrapper.find('.expanded').length).toBe(expectedCallTimes);
        }
    );
});

