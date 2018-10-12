namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateGivenCreditCriteriaTest
    {
        [Theory]
        [InlineData(1, "Age_CustomerUpdCredit_Ag")]
        [InlineData(2, "Age_CustomerUpdCredit_MAg")]
        [InlineData(3, "Age_CustomerUpdCredit_SAg")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(byte searchAccountRoleId, string expectedSettingKey)
        {
            var criteria = new UpdateGivenCreditCommand(
                searchAccountId: 1,
                searchAccountRoleId: searchAccountRoleId,
                creditAmount: 999,
                actorId: 2,
                subAccountId: 0,
                language: "E",
                ip: "192.196.10.1");

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void IsValid_CustId_Should_ReturnsValidSettingKey(int custid, bool expectedIsValid)
        {
            var criteria = new UpdateGivenCreditCommand(
               searchAccountId: custid,
               searchAccountRoleId: 2,
               creditAmount: 999,
               actorId: 2,
               subAccountId: 0,
               language: "E",
               ip: "192.196.10.1");

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_ActorId_Should_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            var criteria = new UpdateGivenCreditCommand(
                  searchAccountId: 1,
                  searchAccountRoleId: 2,
                  creditAmount: 999,
                  actorId: actorId,
                  subAccountId: 0,
                  language: "E",
                  ip: "192.196.10.1");

            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}