import React from 'react';
import ReactDOM from 'react-dom';
import { cloneDeep, forEach } from 'lodash';
import ProtectedMultiple from './protected-multiple';
import { decodeResources } from '../../../SearchAccount/Scripts/app/helpers/accountinfo-helper';

class ProtectedMultipleContainer extends React.Component {
    constructor(props) {
        super(props);
        const { viewModel, resources } = this.props;

        this.state = {
            viewModel: viewModel,
            originalViewModel: cloneDeep(viewModel),
            resources: resources
        };

        this.toggleProtectedAll = this.toggleProtectedAll.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleReset = this.handleReset.bind(this);
    }

    toggleProtectedAll(isProtected) {
        const viewModelCopy = cloneDeep(this.state.viewModel);
        viewModelCopy.forEach(item => { item.IsProtectedMode = isProtected; });

        this.setState({ viewModel: viewModelCopy });
    }

    handleChange(index, value) {
        const { viewModel } = this.state;
        const viewModelCopy = cloneDeep(viewModel);

        viewModelCopy[index].IsProtectedMode = value;

        this.setState({ viewModel: viewModelCopy });
    }

    handleReset() {
        this.setState(prevState => ({
            viewModel: prevState.originalViewModel
        }));
    }

    render() {
        const {  viewModel, originalViewModel, resources } = this.state;

        return (<ProtectedMultiple
                {...{
                    viewModel,
                    originalViewModel,
                    resources,
                    toggleProtectedAll: this.toggleProtectedAll,
                    handleChange: this.handleChange,
                    handleReset: this.handleReset
                }} />);
    }
}

export default ProtectedMultipleContainer;

export const renderProtectedMultiple = (viewModel, resources) => {
    ReactDOM.render(
        <ProtectedMultipleContainer {...{
            viewModel: viewModel,
            resources: decodeResources(resources)
        }} />,
        document.getElementById('container')
    );
};

window.renderProtectedMultiple = renderProtectedMultiple;