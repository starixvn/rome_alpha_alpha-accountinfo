export default class BalanceInfoViewModel {
    TotalCash: number;
    YesterdayCash: number;
    TotalBalance: number;
    YesterdayBalance: number;
    Outstanding: number;
    UsedCredit: number;
    GivenCredit: number;
    CurrentGivenCredit: number;
    Difference: number;
    NewGivenCredit: number;

    public constructor(init?: Partial<BalanceInfoViewModel>) {
        Object.assign(this, init);
    }
};