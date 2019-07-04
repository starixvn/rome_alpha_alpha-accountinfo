"use strict";
interface Window {
    isClearInterval: boolean,
    reloadStatusTab: boolean,
    ageWnd: {
        close: Function
    }
}

interface BettypeStatus {
    SportType: number,
    BetType: number,
    IsDisabled: boolean
}

interface ProductStatus {
    ProductId: number,
    CustId: number,
    CustRoleId: number,
    BetTypeStatus: Array<BettypeStatus>
}

class ProductStatusPopup {
    $: JQueryStatic;
    topWindow: Window;

    $msgServerResult: JQuery;
    $formProductStatus: JQuery;
    productId: number;
    custId: number;
    custRoleId: number;
    token: string;

    constructor($: JQueryStatic, topWindow: Window) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.cacheDOMElements();
        this.registerEvents();
    };

    cacheDOMElements = () => {
        this.$msgServerResult = this.$("#msg-serverResult");
        this.$formProductStatus = this.$("#formProductStatus");
        this.productId = this.$("#hiddenProductId").val();
        this.custId = this.$("#hiddenCustId").val();
        this.custRoleId = this.$("#hiddenCustRoleId").val();
        this.token = this.$("input[name=\"__RequestVerificationToken\"]", this.$formProductStatus).val();
    };

    registerEvents = () => {
        this.$formProductStatus.submit(this.updateProductStatus);
    };

    updateProductStatus = event => {
        event.preventDefault();

        this.$.blockUI();
        const formData = this.getProductStatusData();

        if (!formData) {
            this.showNoDataChange();
            this.$.unblockUI();
            return;
        }

        this.hideMessage();
        const requestVerificationToken = this.token;

        this.$.ajax({
            type: "POST",
            url: this.$formProductStatus.attr("action"),
            data: { message: formData },
            dataType: "json",
            beforeSend: xhr => xhr.setRequestHeader("__RequestVerificationToken", requestVerificationToken)
        })
            .done(this.afterUpdateStatusSuccessfully)
            .always(this.$.unblockUI);
    };

    getProductStatusData = (): ProductStatus => {
        let checkbox: JQuery,
            isDisabled: boolean,
            oldValue: boolean;
        const betTypeStatus = [];

        this.$("input[type='checkbox']").each((_index, element) => {
            checkbox = this.$(element);
            isDisabled = !checkbox.prop("checked");
            oldValue = checkbox.data("oldvalue");

            if (isDisabled !== oldValue) {
                betTypeStatus.push({
                    SportType: checkbox.data("sporttypeid"),
                    BetType: checkbox.data("bettypeid"),
                    IsDisabled: isDisabled
                });
            }
        });

        return betTypeStatus.length !== 0 ?
            {
                ProductId: this.productId,
                CustId: this.custId,
                CustRoleId: this.custRoleId,
                BetTypeStatus: betTypeStatus
            } : null;
    };

    afterUpdateStatusSuccessfully = data => {
        const code = this.$.type(data.ErrCode) === "undefined" ? data.Code : data.ErrCode,
            messsage = data.Message || data.ErrMsg;

        code === 0 ? this.showSuccess(messsage) : this.showError(messsage);
    };

    showNoDataChange = () => {
        this.showMessage("msg msg-warning", this.$("#hiddenNoDataChange").val());
    };

    showSuccess = (message: string) => {
        const timeoutDefault = 1500;
        this.showMessage("msg msg-success", message);

        setTimeout(() => {
            this.topWindow.isClearInterval = true;
            this.topWindow.reloadStatusTab = true;
            this.topWindow.ageWnd.close();
        }, timeoutDefault);
    };

    showError = (message: string) => {
        this.showMessage("msg msg-error", message);
    };

    showMessage = (className: string, message: string) => {
        this.$msgServerResult
            .removeClass()
            .addClass(className)
            .html(message);
    };

    hideMessage = () => {
        this.$msgServerResult
            .removeClass()
            .addClass("hide")
            .html("");
    };
}

$(() => {
    const productStatusPopup = new ProductStatusPopup($, window.top);
    productStatusPopup.init();
});