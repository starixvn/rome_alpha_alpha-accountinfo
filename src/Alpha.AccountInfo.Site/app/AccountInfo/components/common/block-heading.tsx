import * as React from 'react';

interface IHeadingTabProps {
    text: string,
    iconClassname: string,
    id: string,
    tabHref: string
}

interface IHeadingTabState {
    text: string
}

export default class BlockHeading extends React.Component<IHeadingTabProps, IHeadingTabState> {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className="panel-heading" role="tab" id={this.props.id}>
                <h4 className="panel-title">
                    <a className="collapsed" role="button" data-toggle="collapse" data-parent="#infoTab" href={"#" + this.props.tabHref} aria-expanded="false" aria-controls="collapseDownlineList">
                        <span className={this.props.iconClassname}></span> {this.props.text}
                    </a>
                </h4>
            </div>
        );
    }
}