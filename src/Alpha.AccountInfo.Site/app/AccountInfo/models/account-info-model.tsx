import CustomerInfoViewModel from './customer-info-model';
import BalanceInfoViewModel from './balance-info-model';
import DownlineListViewModel from './downline-list-model';
import StatusViewModel from './status-model';
import SettingViewModel from './setting-model';
import LoginInfoViewModel from './login-info-model';
import OnlineListViewModel from './online-list-model';

export default class AccountInfoViewModel {
    CustomerInfo: CustomerInfoViewModel;
    BalanceInfo: BalanceInfoViewModel;
    DownlineList: DownlineListViewModel;
    Status: StatusViewModel;
    Setting: SettingViewModel;
    LoginInfo: LoginInfoViewModel;
    OnlineList: OnlineListViewModel;

    public constructor(init?: Partial<AccountInfoViewModel>) {
        Object.assign(this, init);
    }
};