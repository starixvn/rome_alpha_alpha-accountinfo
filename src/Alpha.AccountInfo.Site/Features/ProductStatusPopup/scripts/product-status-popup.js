"use strict";
var ProductStatusPopup = (function () {
    function ProductStatusPopup($, topWindow) {
        var _this = this;
        this.init = function () {
            _this.cacheDOMElements();
            _this.registerEvents();
        };
        this.cacheDOMElements = function () {
            _this.$msgServerResult = _this.$("#msg-serverResult");
            _this.$formProductStatus = _this.$("#formProductStatus");
            _this.productId = _this.$("#hiddenProductId").val();
            _this.custId = _this.$("#hiddenCustId").val();
            _this.custRoleId = _this.$("#hiddenCustRoleId").val();
            _this.token = _this.$("input[name=\"__RequestVerificationToken\"]", _this.$formProductStatus).val();
        };
        this.registerEvents = function () {
            _this.$formProductStatus.submit(_this.updateProductStatus);
        };
        this.updateProductStatus = function (e) {
            e.preventDefault();
            _this.$.blockUI();
            var formData = _this.getProductStatusData();
            if (!formData) {
                _this.showNoDataChange();
                _this.$.unblockUI();
                return;
            }
            _this.hideMessage();
            var requestVerificationToken = _this.token;
            _this.$.ajax({
                type: "POST",
                url: _this.$formProductStatus.attr("action"),
                data: { message: formData },
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("__RequestVerificationToken", requestVerificationToken);
                }
            })
                .done(_this.afterUpdateStatusSuccessfully)
                .always(_this.$.unblockUI);
        };
        this.getProductStatusData = function () {
            var checkbox, isDisabled, oldValue;
            var self = _this;
            var betTypeStatus = [];
            _this.$("input[type='checkbox']").each(function () {
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
                    ProductId: _this.productId,
                    CustId: _this.custId,
                    CustRoleId: _this.custRoleId,
                    BetTypeStatus: betTypeStatus
                };
            }
        };
        this.afterUpdateStatusSuccessfully = function (data) {
            var code = _this.$.type(data.ErrCode) === "undefined" ? data.Code : data.ErrCode;
            var messsage = data.Message || data.ErrMsg;
            if (code !== 0) {
                _this.showError(messsage);
            }
            else {
                _this.showSuccess(messsage);
                setTimeout(function () {
                    _this.topWindow.realoadLeftPanel = true;
                    _this.topWindow.ageWnd.close();
                }, 3000);
            }
        };
        this.showNoDataChange = function () {
            _this.showMessage("msg msg-warning", _this.$("#hiddenNoDataChange").val());
        };
        this.showSuccess = function (message) {
            _this.showMessage("msg msg-success", message);
        };
        this.showError = function (message) {
            _this.showMessage("msg msg-error", message);
        };
        this.showMessage = function (className, message) {
            _this.$msgServerResult
                .removeClass()
                .addClass(className)
                .html(message);
        };
        this.hideMessage = function () {
            _this.$msgServerResult
                .removeClass()
                .addClass("hide")
                .html("");
        };
        this.$ = $;
        this.topWindow = topWindow;
    }
    return ProductStatusPopup;
}());
$(function () {
    var productStatusPopup = new ProductStatusPopup($, window.top);
    productStatusPopup.init();
});
//# sourceMappingURL=product-status-popup.js.map