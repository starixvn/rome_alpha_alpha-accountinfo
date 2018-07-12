export default class CustomerInfoViewModel {
    CustId: number;
    RoleId: number;
    ActorRoleId: number;
    UserName: string;
    UserName2: string;

    Phone: string;
    Fax: string;
    MemberName: string;
    AgentName: string;
    MasterName: string;
    SuperName: string;

    CustomerListLink: string;
    BetListLink: string;
    StatemenLink: string;
    ViewLogLink: string;

    public constructor(init?: Partial<CustomerInfoViewModel>) {
        Object.assign(this, init);
    }
};