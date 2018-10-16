namespace Alpha.AccountInfo.Tests.Features.SearchAccount.Models
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class OnlineUserModelTests
    {
        [Fact]
        public void FormattedLoginTime_Always_BeFormatted()
        {
            var loginTime = new DateTime(1989, 12, 12);
            var model = new OnlineUserModel("10.18.20.20", loginTime);

            Assert.Equal("12 Dec 12:00 AM", model.FormattedLoginTime);
        }
    }
}