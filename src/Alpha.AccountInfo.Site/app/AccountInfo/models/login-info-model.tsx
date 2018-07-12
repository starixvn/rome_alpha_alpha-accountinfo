export default class LoginInfoViewModel {
    CreatedDate: string;
    LastLogin: string;
    LastTxn: string;
    LoginIP: string;

    public constructor(init?: Partial<LoginInfoViewModel>) {
        Object.assign(this, init);
    }
};