export default class CellModel {
    classnames?: string;
    hrefLink?: string;
    text: string

    public constructor(init?: Partial<CellModel>) {
        Object.assign(this, init);
    }
};