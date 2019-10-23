describe("ProductStatus", () => {
    const jQueryFake = $;
    let topWindow: any;
    let productStatus: ProductStatusPopup;

    const template: any = {
        normal: `<body>
                    <div id="msg-serverResult" class="hide"></div>
                    <div class="msg msg-warning">
                        You are not allowed to disable downlines as a company policy.
                    </div>
                    <form action="/site-accountinfo/ProductStatus/Update" id="formProductStatus" method="post">
                        <input name="__RequestVerificationToken" type="hidden" value="W88YvCanMUJ0AptMkY0VmYpsLc3NWRW5PQFVyBiUTOJTs4GUM1cmRI-zKId4ocyu28wTe7jx4MB2c-44KXGMBK5iCeA1" />
                        <table class="tblRpt tblRpt-bordered width-100per">
                            <thead>
                                <tr>
                                    <th colspan="2">
                                        <div class="text-center">Master: master1</div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Baccarat, Roulette, Sic Bo, Dragon Tiger</td>
                                    <td>
                                        <input type="checkbox"
                                                id="checkbox-2401"
                                                data-sporttypeid="211"
                                                data-bettypeid="2401"
                                                data-oldvalue="false"
                                                checked=checked
                                                disabled=disabled>
                                        <label for="checkbox-2401">Master</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>VIP Baccarat</td>
                                    <td>
                                        <input type="checkbox"
                                                id="checkbox-2402"
                                                data-sporttypeid="211"
                                                data-bettypeid="2402"
                                                data-oldvalue="false"
                                                checked=checked
                                                disabled=disabled>
                                        <label for="checkbox-2402">Master</label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="margin-top10"><button type="submit" class="btn-submit">Submit</button></div>
                        <input type="hidden" id="hiddenProductId" value="21" />
                        <input type="hidden" id="hiddenCustId" value="1176083" />
                        <input type="hidden" id="hiddenCustRoleId" value="3" />
                        <input type="hidden" id="hiddenNoDataChange" value="There is no change to update." />
                    </form>
                </body>`
    };

    beforeEach(() => {
        topWindow = {
            ageWnd: jasmine.createSpyObj('ageWnd', ['close'])
        };

        productStatus = new ProductStatusPopup(jQueryFake, topWindow);
    });

    describe("_init", () => {
        beforeEach(() => {
            // arrange
            spyOn(productStatus, "cacheDOMElements");
            spyOn(productStatus, "registerEvents");

            // act
            productStatus.init();
        });

        it("_Always_Calls cacheDOMElements", () => {
            expect(productStatus.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Always_Calls registerEvents", () => {
            expect(productStatus.registerEvents).toHaveBeenCalled();
        });
    });

    describe("_cacheDOMElements", () => {
        it("_Always_All neccessary elements are cached", () => {
            // arrange
            setFixtures(template.normal);

            // act
            productStatus.cacheDOMElements();

            // assert
            expect(productStatus.$msgServerResult).toBeDefined();
            expect(productStatus.$formProductStatus).toBeDefined();
            expect(productStatus.productId).toBeDefined();
            expect(productStatus.custId).toBeDefined();
            expect(productStatus.custRoleId).toBeDefined();
            expect(productStatus.token).toBeDefined();
        });
    });

    describe("_registerEvents", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            spyOn(productStatus, "updateProductStatus");
            productStatus.cacheDOMElements();

            // act
            productStatus.registerEvents();
        });

        it("_Always_Bind Form_Submit event", () => {
            // act
            productStatus.$formProductStatus.submit();

            // assert
            expect(productStatus.updateProductStatus).toHaveBeenCalled();
        });
    });

    describe("_updateProductStatus", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Form Data Change -> Make a AJAX POST", () => {
            // arrange
            const e = { preventDefault: function () { } };
            const self = this;
            this.$("input[type='checkbox']").each(function (): void {
                const checkbox = self.$(this);
                checkbox.prop("checked", !checkbox.is(":checked"));
            });
            spyOn(jQueryFake, "ajax").and.callThrough();

            // act
            productStatus.updateProductStatus(e);

            // assert
            expect(productStatus.$msgServerResult.hasClass("hide")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("");
            expect(jQueryFake.ajax).toHaveBeenCalled();
        });

        it("_No Form Data Change -> Show Warning Message", () => {
            // arrange
            const e = { preventDefault: function () { } };
            spyOn(jQueryFake, "ajax").and.callThrough();

            // act
            productStatus.updateProductStatus(e);

            // assert
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
            expect(jQueryFake.ajax).not.toHaveBeenCalled();
        });
    });

    describe("_getProductStatusData", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_No Data Change -> Returns null", () => {
            // act
            const formData = productStatus.getProductStatusData();

            // assert
            expect(formData).toBe(null);
        });

        it("_Data Change -> Returns Object", () => {
            // arrange
            let self: any = this;
            this.$("input[type='checkbox']").each(function (): void {
                const checkbox = self.$(this);
                checkbox.prop("checked", !checkbox.is(":checked"));
            });

            // act
            const formData = productStatus.getProductStatusData();

            // assert
            expect(formData.ProductId).toBe(productStatus.productId);
            expect(formData.CustId).toBe(productStatus.custId);
            expect(formData.CustRoleId).toBe(productStatus.custRoleId);
            expect(formData.BetTypeStatus).not.toBe("");
        });
    });

    //describe("_afterUpdateStatusSuccessfully", () => {
    //    it("_Code = 0 -> Show Successful Message & Close Popup After 3 seconds", () => {
    //        // arrange
    //        jasmine.clock().install();
    //        setFixtures(template.normal);
    //        productStatus.cacheDOMElements();

    //        const data = { Code: 0, Message: "Status updated successfully." };

    //        // act
    //        productStatus.afterUpdateStatusSuccessfully(data);
    //        jasmine.clock().tick(3001);

    //        // assert
    //        expect(productStatus.topWindow.reloadLeftPanel).toBeTruthy();
    //        expect(productStatus.$msgServerResult.html()).toBe("Status updated successfully.");
    //        expect(productStatus.topWindow.ageWnd.close).toHaveBeenCalled();
    //        jasmine.clock().uninstall();
    //    });

    //    it("_Code != 0 -> Show error Message & Don't Reload Page", () => {
    //        // arrange
    //        setFixtures(template.normal);
    //        productStatus.cacheDOMElements();

    //        const data = { Code: 403, Message: "You are not authorized on this function." };

    //        // act
    //        productStatus.afterUpdateStatusSuccessfully(data);

    //        // assert
    //        expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
    //        expect(productStatus.$msgServerResult.html()).toBe("You are not authorized on this function.");
    //        expect(productStatus.topWindow.ageWnd.close).not.toHaveBeenCalled();
    //    });

    //    it("_ErrCode != 0 -> Show error Message 'Request Verification Token does not exist.'", () => {
    //        // arrange
    //        setFixtures(template.normal);
    //        productStatus.cacheDOMElements();

    //        const data = { ErrCode: 1, ErrMsg: "Request Verification Token does not exist. Please refresh your page. Thanks." };

    //        // act
    //        productStatus.afterUpdateStatusSuccessfully(data);

    //        // assert
    //        expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
    //        expect(productStatus.$msgServerResult.html()).toBe("Request Verification Token does not exist. Please refresh your page. Thanks.");
    //        expect(productStatus.topWindow.ageWnd.close).not.toHaveBeenCalled();
    //    });
    //});

    describe("_showNoDataChange", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Always_Show_Warning_Message", () => {
            // act
            productStatus.showNoDataChange();

            // assert
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
        });
    });

    describe("_showSuccess", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Always_Show_Success_Message", () => {
            // act
            productStatus.showSuccess("Status updated successfully.");

            // assert
            expect(productStatus.$msgServerResult.hasClass("msg msg-success")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("Status updated successfully.");
        });
    });

    describe("_showError", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Always_Show_Error_Message", () => {
            // act
            productStatus.showError("You are not allowed to disable for downlines.");

            // assert
            expect(productStatus.$msgServerResult.hasClass("msg msg-error")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("You are not allowed to disable for downlines.");
        });
    });

    describe("_showMessage", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Always_Show_Message", () => {
            // act
            productStatus.showMessage("msg msg-warning", "There is no change to update.");

            // assert
            expect(productStatus.$msgServerResult.hasClass("msg msg-warning")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("There is no change to update.");
        });
    });

    describe("_hideMessage", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            productStatus.cacheDOMElements();
        });

        it("_Always_Hide_Message", () => {
            // act
            productStatus.hideMessage();

            // assert
            expect(productStatus.$msgServerResult.hasClass("hide")).toBeTruthy();
            expect(productStatus.$msgServerResult.html()).toBe("");
        });
    });
});