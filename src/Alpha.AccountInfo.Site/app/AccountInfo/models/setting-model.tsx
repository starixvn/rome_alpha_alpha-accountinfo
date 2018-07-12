export default class SettingViewModel {
    public constructor(init?: Partial<SettingViewModel>) {
        Object.assign(this, init);
    }
};