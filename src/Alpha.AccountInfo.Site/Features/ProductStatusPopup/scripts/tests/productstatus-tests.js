var _this = this;
describe("ProductStatus", function () {
    var jQueryFake = $;
    var topWindow;
    var productStatus;
    var template = {
        normal: "<body>\n                    <div id=\"msg-serverResult\" class=\"hide\"></div>\n                    <div class=\"msg msg-warning\">\n                        You are not allowed to disable downlines as a company policy.\n                    </div>\n                    <form action=\"/site-accountinfo/ProductStatus/Update\" id=\"formProductStatus\" method=\"post\">\n                        <input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"W88YvCanMUJ0AptMkY0VmYpsLc3NWRW5PQFVyBiUTOJTs4GUM1cmRI-zKId4ocyu28wTe7jx4MB2c-44KXGMBK5iCeA1\" />\n                        <table class=\"tblRpt tblRpt-bordered width-100per\">\n                            <thead>\n                                <tr>\n                                    <th colspan=\"2\">\n                                        <div class=\"text-center\">Master: master1</div>\n                                    </th>\n                                </tr>\n                            </thead>\n                            <tbody>\n                                <tr>\n                                    <td>Baccarat, Roulette, Sic Bo, Dragon Tiger</td>\n                                    <td>\n                                        <input type=\"checkbox\"\n                                                id=\"checkbox-2401\"\n                                                data-sporttypeid=\"211\"\n                                                data-bettypeid=\"2401\"\n                                                data-oldvalue=\"false\"\n                                                checked=checked\n                                                disabled=disabled>\n                                        <label for=\"checkbox-2401\">Master</label>\n                                    </td>\n                                </tr>\n                                <tr>\n                                    <td>VIP Baccarat</td>\n                                    <td>\n                                        <input type=\"checkbox\"\n                                                id=\"checkbox-2402\"\n                                                data-sporttypeid=\"211\"\n                                                data-bettypeid=\"2402\"\n                                                data-oldvalue=\"false\"\n                                                checked=checked\n                                                disabled=disabled>\n                                        <label for=\"checkbox-2402\">Master</label>\n                                    </td>\n                                </tr>\n                            </tbody>\n                        </table>\n                        <div class=\"margin-top10\"><button type=\"submit\" class=\"btn-submit\">Submit</button></div>\n                        <input type=\"hidden\" id=\"hiddenProductId\" value=\"21\" />\n                        <input type=\"hidden\" id=\"hiddenCustId\" value=\"1176083\" />\n                        <input type=\"hidden\" id=\"hiddenCustRoleId\" value=\"3\" />\n                        <input type=\"hidden\" id=\"hiddenNoDataChange\" value=\"There is no change to update.\" />\n                    </form>\n                </body>"
    };
    beforeEach(function () {
        topWindow = {
            ageWnd: jasmine.createSpyObj('ageWnd', ['close'])
        };
        productStatus = new ProductStatusPopup(jQueryFake, topWindow);
    });
    describe("_init", function () {
        beforeEach(function () {
            spyOn(productStatus, "cacheDOMElements");
            spyOn(productStatus, "registerEvents");
            productStatus.init();
        });
        it("_Always_Calls cacheDOMElements", function () {
            expect(productStatus.cacheDOMElements).toHaveBeenCalled();
        });
        it("_Always_Calls registerEvents", function () {
            expect(productStatus.registerEvents).toHaveBeenCalled();
        });
    });
    describe("_cacheDOMElements", function () {
        it("_Always_All neccessary elements are cached", function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
            expect(productStatus.$msgServerResult).toBeDefined();
            expect(productStatus.$formProductStatus).toBeDefined();
            expect(productStatus.productId).toBeDefined();
            expect(productStatus.custId).toBeDefined();
            expect(productStatus.custRoleId).toBeDefined();
            expect(productStatus.token).toBeDefined();
        });
    });
    describe("_registerEvents", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            spyOn(productStatus, "updateProductStatus");
            productStatus.cacheDOMElements();
            productStatus.registerEvents();
        });
        it("_Always_Bind Form_Submit event", function () {
            productStatus.$formProductStatus.submit();
            expect(productStatus.updateProductStatus).toHaveBeenCalled();
        });
    });
    describe("_updateProductStatus", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Form Data Change -> Make a AJAX POST", function () {
            var e = { preventDefault: function () { } };
            var self = _this;
            _this.$("input[type='checkbox']").each(function () {
                var checkbox = self.$(this);
                checkbox.prop("checked", !checkbox.is(":checked"));
            });
            spyOn(jQueryFake, "ajax").and.callThrough();
            productStatus.updateProductStatus(e);
            expect(productStatus.$msgServerResult.hasClass("hide")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("");
            expect(jQueryFake.ajax).toHaveBeenCalled();
        });
        it("_No Form Data Change -> Show Warning Message", function () {
            var e = { preventDefault: function () { } };
            spyOn(jQueryFake, "ajax").and.callThrough();
            productStatus.updateProductStatus(e);
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
            expect(jQueryFake.ajax).not.toHaveBeenCalled();
        });
    });
    describe("_getProductStatusData", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_No Data Change -> Returns Undefined", function () {
            var formData = productStatus.getProductStatusData();
            expect(formData).toBe(undefined);
        });
        it("_Data Change -> Returns Object", function () {
            var self = _this;
            _this.$("input[type='checkbox']").each(function () {
                var checkbox = self.$(this);
                checkbox.prop("checked", !checkbox.is(":checked"));
            });
            var formData = productStatus.getProductStatusData();
            expect(formData.ProductId).toBe(productStatus.productId);
            expect(formData.CustId).toBe(productStatus.custId);
            expect(formData.CustRoleId).toBe(productStatus.custRoleId);
            expect(formData.ProductStatus).not.toBe("");
        });
    });
    describe("_afterUpdateStatusSuccessfully", function () {
        it("_Code = 0 -> Show Successful Message & Close Popup After 3 seconds", function () {
            jasmine.clock().install();
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
            var data = { Code: 0, Message: "Status updated successfully." };
            productStatus.afterUpdateStatusSuccessfully(data);
            jasmine.clock().tick(3001);
            expect(productStatus.topWindow.realoadLeftPanel).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("Status updated successfully.");
            expect(productStatus.topWindow.ageWnd.close).toHaveBeenCalled();
            jasmine.clock().uninstall();
        });
        it("_Code != 0 -> Show error Message & Don't Reload Page", function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
            var data = { Code: 403, Message: "You are not authorized on this function." };
            productStatus.afterUpdateStatusSuccessfully(data);
            expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("You are not authorized on this function.");
            expect(productStatus.topWindow.ageWnd.close).not.toHaveBeenCalled();
        });
        it("_ErrCode != 0 -> Show error Message 'Request Verification Token does not exist.'", function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
            var data = { ErrCode: 1, ErrMsg: "Request Verification Token does not exist. Please refresh your page. Thanks." };
            productStatus.afterUpdateStatusSuccessfully(data);
            expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("Request Verification Token does not exist. Please refresh your page. Thanks.");
            expect(productStatus.topWindow.ageWnd.close).not.toHaveBeenCalled();
        });
    });
    describe("_showNoDataChange", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Always_Show_Warning_Message", function () {
            productStatus.showNoDataChange();
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
        });
    });
    describe("_showSuccess", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Always_Show_Success_Message", function () {
            productStatus.showSuccess("Status updated successfully.");
            expect(productStatus.$msgServerResult.hasClass("msg msg-success")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("Status updated successfully.");
        });
    });
    describe("_showError", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Always_Show_Error_Message", function () {
            productStatus.showError("You are not allowed to disable for downlines.");
            expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("You are not allowed to disable for downlines.");
        });
    });
    describe("_showMessage", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Always_Show_Message", function () {
            productStatus.showMessage("msg msg-warning", "There is no change to update.");
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
        });
    });
    describe("_hideMessage", function () {
        beforeEach(function () {
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });
        it("_Always_Hide_Message", function () {
            productStatus.hideMessage();
            expect(productStatus.$msgServerResult.hasClass("hide")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("");
        });
    });
});
//# sourceMappingURL=productstatus-tests.js.map