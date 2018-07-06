import CellModel from './cell-model';

export default class RowInfoModel {
    key: CellModel;
    value: CellModel;

    public constructor(init?: Partial<RowInfoModel>) {
        Object.assign(this, init);
    }

    public toJs(): any {
        return {
            key: this.key,
            value: this.value
        }
    }
};