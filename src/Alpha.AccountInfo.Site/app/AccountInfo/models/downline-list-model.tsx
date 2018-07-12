import TotalStatusDownline from './total-status-downline';

export default class DownlineListViewModel {
    DownlineList: string[];
    TotalStatusAgent: TotalStatusDownline;
    TotalStatusMember: TotalStatusDownline;

    public constructor(init?: Partial<DownlineListViewModel>) {
        Object.assign(this, init);
    }
};