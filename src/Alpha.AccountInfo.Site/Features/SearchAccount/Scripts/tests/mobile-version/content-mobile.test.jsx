import React from 'react';
import { HashRouter, Link, Redirect } from 'react-router-dom';
import ReactDOM from 'react-dom';
import sinon from 'sinon';
import { mount, shallow } from "enzyme";
import renderer from 'react-test-renderer';
import '../../../../../__mocks__/global.mock';
import ContentMobile from 'ContentMobile';
import { CustomLink, ContentPage, FeatureListPage } from 'ContentMobile';
import { AccountInfoProps } from '../helpers/account-info-props-sample';
import BalanceInfo from 'BalanceInfo';
import 'babel-polyfill';

describe('CustomLink', () => {
    let props;
    beforeEach(() => {
        props = {
            path: "/featureList",
            iconClass: "icon-login",
            featureText: "Feature List"
        };
    });

    test('always render a `Link`', () => {
        const wrapper = shallow(<CustomLink {...props} />);
        expect(wrapper.find(Link).length).toBe(1);
    });
});

describe('ContentPage', () => {
    let props;
    beforeEach(() => {
        props = {
            iconClass: "icon-login",
            featureText: "Feature List",
            children: null,
            animationClass: 'animationClass'
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(
            <HashRouter>
                <ContentPage {...props} />
            </HashRouter>,
            div);
    });

    test('snapshot', () => {
        const component = renderer.create(
            <HashRouter>
                <ContentPage {...props} />
            </HashRouter>
        );
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('always render a `CustomLink`', () => {
        const wrapper = shallow(<ContentPage {...props} />);
        expect(wrapper.find(CustomLink).length).toBe(1);
    });
});

describe('FeatureListPage', () => {
    let props;
    beforeEach(() => {
        props = {
            contentItems: AccountInfoProps.contentItems,
            animationClass: 'animationClass'
        };
    });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(
            <HashRouter>
                <FeatureListPage {...props} />
            </HashRouter>,
            div);
    });

    test('snapshot', () => {
        const component = renderer.create(
            <HashRouter>
                <FeatureListPage {...props} />
            </HashRouter>
        );
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('always render at least 7  `CustomLink`', () => {
        const wrapper = shallow(<FeatureListPage {...props} />);
        expect(wrapper.find(CustomLink).length).toBeGreaterThanOrEqual(7);
    });

    test.each([[true, 1], [false, 0]])(
        "correct render `CustomLink` by visible property",
        (visible, expectedCallTimes) => {
            props.contentItems.downlineListProps.visible = visible;
            const wrapper = mount(
                <HashRouter>
                    <FeatureListPage {...props} />
                </HashRouter>);
            expect(wrapper.find('#nav-icon-downline').length).toBe(expectedCallTimes);
        }
    );
});

describe('ContentMobile', () => {
    let props;
    let shouldComponentUpdateSpy;

    beforeEach(() => {
        props = {
            iconClass: '',
            featureText: '',
            children: null,
            contentItems: AccountInfoProps.contentItems
        };
     shouldComponentUpdateSpy = sinon.spy(ContentMobile.prototype, 'shouldComponentUpdate');
    });

    afterEach(() => {
        shouldComponentUpdateSpy.restore();
      });

    test('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render(
            <HashRouter>
                <ContentMobile {...props} />
            </HashRouter>,
            div);
    });

    test('snapshot', () => {
        const component = renderer.create(
            <HashRouter>
                <ContentMobile {...props} />
            </HashRouter>
        );
        expect(component.toJSON()).toMatchSnapshot();
    });

    test('render a `FeatureListPage` when contentItems has data', () => {
        const wrapper = shallow(<ContentMobile {...props} />);
        expect(wrapper.find(FeatureListPage).length).toBe(1);
    });


    test('render a `ContentPage` when contentItems has no data', () => {
        props.contentItems = [];

        const wrapper = shallow(<ContentMobile {...props} />);
        expect(wrapper.find(ContentPage).length).toBe(1);
    });

    test('redirect to `balance info` content when  redirectToBalanceInfoPage is true', () => {
        props.redirectToBalanceInfoPage = true;
        const wrapper = mount(
            <HashRouter>
                <ContentMobile {...props} />
            </HashRouter>);
         expect(wrapper.node.history.location.pathname).toBe("/balanceInfo");
    });

    test('redirect to `Feature List` content when  redirectToFeatureList is  true', () => {
        props.redirectToFeatureList = false;
        props.redirectToFeatureList = true;
        const wrapper = mount(
            <HashRouter>
                <ContentMobile {...props} />
            </HashRouter>);
         expect(wrapper.node.history.location.pathname).toBe("/featureList");
    });
});