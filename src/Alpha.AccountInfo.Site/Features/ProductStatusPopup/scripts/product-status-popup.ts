"use strict";
interface Window {
    realoadLeftPanel: boolean,
    ageWnd: {
        close: Function
    }
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

    getProductStatusData = (): any => {
        let checkbox: JQuery,
            isDisabled: boolean,
            oldValue: boolean;
        const self = this;
        const betTypeStatus = [];

        this.$("input[type='checkbox']").each(function (): void {
            checkbox = self.$(this);
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

        if (betTypeStatus.length !== 0) {
            return {
                ProductId: this.productId,
                CustId: this.custId,
                CustRoleId: this.custRoleId,
                BetTypeStatus: betTypeStatus
            };
        }
    };

    afterUpdateStatusSuccessfully = data => {
        const code = this.$.type(data.ErrCode) === "undefined" ? data.Code : data.ErrCode,
            messsage = data.Message || data.ErrMsg, timeoutDefault = 1500;

        if (code !== 0) {
            this.showError(messsage);
        } else {
            this.showSuccess(messsage);
            setTimeout(() => {
                this.topWindow.realoadLeftPanel = true;
                this.topWindow.ageWnd.close();
            }, timeoutDefault);
        }
    };

    showNoDataChange = () => {
        this.showMessage("msg msg-warning", this.$("#hiddenNoDataChange").val());
    };

    showSuccess = (message: string) => {
        this.showMessage("msg msg-success", message);
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