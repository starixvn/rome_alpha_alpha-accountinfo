namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateGivenCreditCriteriaTest
    {
        [Theory]
        [InlineData(2, "Age_CustomerUpdCredit_Ag")]
        [InlineData(3, "Age_CustomerUpdCredit_MAg")]
        [InlineData(4, "Age_CustomerUpdCredit_SAg")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(byte actorRoleId, string expectedSettingKey)
        {
            var criteria = new UpdateGivenCreditCriteria(
                custId: 1, 
                roleId: 1,
                creditAmount: 999,
                actorId:2,
                actorRoleId: actorRoleId,
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
            var criteria = new UpdateGivenCreditCriteria(
               custId: custid,
               roleId: 2,
               creditAmount: 999,
               actorId: 2,
               actorRoleId: 4,
               subAccountId: 0,
               language: "E",
               ip: "192.196.10.1");

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
