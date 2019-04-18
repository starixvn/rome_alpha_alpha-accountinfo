import * as React from 'react';
import ReactDOM from 'react-dom';

window.renderUnsuspendList = function (unsuspendDownlinesModel) {
	ReactDOM.render(
		<UnsuspendList 
			unsuspendDownlinesModel={unsuspendDownlinesModel}
		/>,
		document.getElementById('unsuspend-list')
	);
};

export default class UnsuspendList extends React.Component {
	constructor(props) {
		super(props);
	}

	buildNote() {
		return (
			<div className="note">
				List of downlines whose status are changed/unchanged after going to unsuspend 'selected customer'
            </div>
		);
	}

	render() {
		const { unsuspendDownlinesModel } = this.props;

		return (
			< div className="online-list" >

				{this.buildNote()}

				<table className="tblRpt tblRpt-bordered width-100per">
					<thead>
						<tr>
							<th>Level</th>
							<th>Username</th>
						</tr>
					</thead>
					<tbody>
						{
							unsuspendDownlinesModel.UnsuspendDownlinesByLevel.map((level, index) => {
								const length = level.UnsuspendDownlines.length;
								return (
									level.UnsuspendDownlines.map((username, index1, arr) => {
										if (index1 === 0) {
											return (
												<tr key={index1}>
													<td className="text-center" rowSpan={length}>
														{level.RoleId}
													</td>
													<td className="text-center">
														<a href='#'>
															{username.Username}
														</a>
													</td>
												</tr>
											);
										}
										else {
											return (
												<tr key={index1}>
													<td className="text-center">
														<a href='#'>
															{username.Username}
														</a>
													</td>
												</tr>
											);
										}
									})
								);
								
							})
						}
					</tbody>
				</table>
			</div >
		);
	}
}

export const UnsuspendRow = ({ level}) => {
	return (
		<tr>
			<td className="text-center" rowSpan={level.UnsuspendDownlines.length}>
				{level.RoleId}
			</td>
			<td className="text-center">
				<a href='#'>
					{level.AffectedDownlines}
				</a>
			</td>
		</tr>
	);
}