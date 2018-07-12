export default class TotalStatusDownline {
    TotalActive: number;
    TotalClosed: number;
    TotalSuspend: number;

    public constructor(init?: Partial<TotalStatusDownline>) {
        Object.assign(this, init);
    }
};