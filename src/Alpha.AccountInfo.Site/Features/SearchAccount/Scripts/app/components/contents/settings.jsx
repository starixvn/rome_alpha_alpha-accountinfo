import React from 'react';
import CustomAnchorTag from '../common/custom-anchor-tag';

const Settings = ({ resources, settingData, mobileView, searchCounter }) => {
	if ((mobileView || searchCounter > 0) && settingData === null) {
		return <div className="no-information">{resources.NoInfo}</div>
	}

	return (
		<div className="setting">
			{
				settingData.map((value, index) => {
					return (
						<div className="col-xs-6" key={index}>
							<CustomAnchorTag href={value.Url} mobileView={mobileView}>
								{value.Text}
							</CustomAnchorTag>
						</div>
					);
				})}
		</div>
	);
}

export default Settings;