"use strict";
class ProductStatus {
    $: JQueryStatic;
    topWindow: any;

    $msgServerResult: JQuery;
    $formProductStatus: JQuery;
    productId: number;
    custId: number;
    custRoleId: number;
    token: string;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = (): void => {
        this.cacheDOMElements();
        this.registerEvents();
    };

    cacheDOMElements = (): void => {
        this.$msgServerResult = this.$("#msg-serverResult");
        this.$formProductStatus = this.$("#formProductStatus");
        this.productId = this.$("#hiddenProductId").val();
        this.custId = this.$("#hiddenCustId").val();
        this.custRoleId = this.$("#hiddenCustRoleId").val();
        this.token = this.$("input[name=\"__RequestVerificationToken\"]", this.$formProductStatus).val();
    };

    registerEvents = (): void => {
        this.$formProductStatus.submit(this.updateProductStatus);
    };

    updateProductStatus = (e: any): void => {
        e.preventDefault();

        this.$.blockUI();
        let formData: any = this.getProductStatusData();

        if (!formData) {
            this.showNoDataChange();
            this.$.unblockUI();
            return;
        }

        this.hideMessage();
        //let self: any = this;

        this.$.post(this.$formProductStatus.attr("action"), formData, this.afterUpdateStatusSuccessfully, "json")
            .always(this.$.unblockUI);
    };

    getProductStatusData = (): any => {
        let checkbox: JQuery,
            isDisabled: boolean,
            oldValue: boolean;
        let self: any = this;
        let formData: any = [];

        this.$("input[type='checkbox']").each(function (): void {
            checkbox = self.$(this);
            isDisabled = !checkbox.prop("checked");
            oldValue = checkbox.data("oldvalue");

            if (isDisabled !== oldValue) {
                formData.push({
                    SportType: checkbox.data("sporttypeid"),
                    BetType: checkbox.data("bettypeid"),
                    IsDisabled: isDisabled
                });
            }
        });

        if (formData.length !== 0) {
            return {
                __RequestVerificationToken: this.token,
                ProductId: this.productId,
                CustId: this.custId,
                CustRoleId: this.custRoleId,
                ProductStatus: JSON.stringify(formData)
            };
        }
    };

    afterUpdateStatusSuccessfully = (data: any): void => {
        if (data.Code !== 0) {
            this.showError(data.Message);
        } else {
            this.showSuccess(data.Message);
            setTimeout(this.topWindow.closeEditSportStatusPopup, 1000 * 3);
        }
    };

    showNoDataChange = (): void => {
        this.showMessage("msg msg-warning", this.$("#hiddenNoDataChange").val());
    };

    showSuccess = (message: string): void => {
        this.showMessage("msg msg-success", message);
    };

    showError = (message: string): void => {
        this.showMessage("msg msg-error", message);
    };

    showMessage = (className: string, message: string): void => {
        this.$msgServerResult
            .removeClass()
            .addClass(className)
            .html(message);
    };

    hideMessage = (): void => {
        this.$msgServerResult
            .removeClass()
            .addClass("hide")
            .html("");
    };
}

$(() => {
    let productStatus: any = new ProductStatus($, window.top);
    productStatus.init();
});