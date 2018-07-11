export default class CustomerInfoViewModel {
    custId: number;
    roleId: number;
    actorRoleId: number;
    userName: string;
    userName2: string;

    phone: string;
    fax: string;
    memberName: string;
    agentName: string;
    masterName: string;
    superName: string;

    customerListLink: string;
    betListLink: string;
    statemenLink: string;
    viewLogLink: string;

    public constructor(init?: Partial<CustomerInfoViewModel>) {
        Object.assign(this, init);
    }
};