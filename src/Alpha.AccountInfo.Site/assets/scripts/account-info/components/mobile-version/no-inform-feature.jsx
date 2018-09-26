import * as React from 'react';
import { Link } from 'react-router-dom';
import { HeaderFeature } from './header-feature';

export const NoInformFeature = ({ url, iconClass, featureText, noInform }) => {
    return (
        <div className="feature-detail translate-from-left">
            <HeaderFeature url={url} iconClass={iconClass} featureText={featureText} />
            <div className="no-information">{noInform}</div>
        </div>
    );
};
