import React, { PureComponent } from 'react';
import { changeDisplayLanguage } from './profile-picture-helper';

export default class DisplayLanguage extends PureComponent {
    constructor(props) {
        super(props);

        const { displayLanguage } = this.props;
        this.state = {
            openDisplayLanguagePanel: true,
            openLanguageDropdown: false,
            displayLanguage: displayLanguage
        };

        this.handleOpenDisplayLanguagePanel = this.handleOpenDisplayLanguagePanel.bind(this);
        this.handleOpenLanguageDropdown = this.handleOpenLanguageDropdown.bind(this);
        this.handleChangeDisplayLanguage = this.handleChangeDisplayLanguage.bind(this);
        this.handleOnSubmitChangeDisplayLanguage = this.handleOnSubmitChangeDisplayLanguage.bind(this);
        this.handleWindowClick = this.handleWindowClick.bind(this);
    }

    handleOpenDisplayLanguagePanel() {
        this.setState((prevState) => ({
            openDisplayLanguagePanel: !prevState.openDisplayLanguagePanel
        }));
    }

    handleOpenLanguageDropdown(e) {
        e.stopPropagation();
        this.setState((prevState) => ({
            openLanguageDropdown: !prevState.openLanguageDropdown
        }));
    }

    handleChangeDisplayLanguage(e, languageCode) {
        e.stopPropagation();
        this.setState({
            displayLanguage: languageCode,
            openLanguageDropdown: false
        });
    }

    handleOnSubmitChangeDisplayLanguage() {
        const { displayLanguage } = this.state;
        displayLanguage && changeDisplayLanguage(displayLanguage);
    }

    componentDidMount() {
        window.addEventListener("click", this.handleWindowClick);
    }

    componentWillUnmount() {
        window.removeEventListener('click', this.handleWindowClick);
    }

    handleWindowClick() {
        this.setState({
            openLanguageDropdown: false
        });
    }

    render() {
        const { openDisplayLanguagePanel, openLanguageDropdown, displayLanguage } = this.state;
        const { resources, languages, allowUpdate } = this.props;
        const renderDisplayLanguage = () => {
            if (displayLanguage) {
                const matchedLanguage = languages.find(lang => lang.Key === displayLanguage);
                return <label onClick={(e) => this.handleOpenLanguageDropdown(e)}><span className={'flag flag-' + displayLanguage}></span>{matchedLanguage.Value}</label>
            }

            return <label onClick={(e) => this.handleOpenLanguageDropdown(e)}>{resources.PleaseSelectDisplayLanguage}</label>
        };
        const renderLanguageDropdown = () => languages.map((lang) =>
            <li key={lang.Key} onClick={(e) => this.handleChangeDisplayLanguage(e, lang.Key)}><span className={'flag flag-' + lang.Key}></span>{lang.Value}</li>);

        return (<div className='panel'>
            <div className={openDisplayLanguagePanel ? "panel-heading open" : "panel-heading"} onClick={this.handleOpenDisplayLanguagePanel}>
                <h4 className='panel-title'>
                    {resources.DisplayLanguage}
                </h4>
            </div>
            <div className='panel-content'>
                <p>{resources.DisplayLanguageIntro}</p>
                <div className='language'>
                    <div className={'block-language' + (openLanguageDropdown ? ' open' : '')}>
                        {renderDisplayLanguage(displayLanguage, languages)}
                        <ul className='dropdown-menu'>{renderLanguageDropdown(languages)}</ul>
                    </div>
                    <button className='btn-submit' onClick={this.handleOnSubmitChangeDisplayLanguage} disabled={!allowUpdate}>{resources.Submit}</button>
                </div>
            </div>
        </div>)
    }
}