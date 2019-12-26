namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateGivenCreditReplicateServerCommandTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var expectedSettingKey = "Acc_CustomerUpdCreditOnReplicateServer";

            var criteria = new UpdateGivenCreditReplicateServerCommand(
                searchAccountId: 1,
                creditAmount: 999);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void IsValid_CustId_Should_ReturnsValidSettingKey(int custid, bool expectedIsValid)
        {
            var criteria = new UpdateGivenCreditReplicateServerCommand(
               searchAccountId: custid,
               creditAmount: 999);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_CreditAmount_Should_ReturnsCorrectValue(int creditAmount, bool expectedValue)
        {
            var criteria = new UpdateGivenCreditReplicateServerCommand(
                  searchAccountId: 1,
                  creditAmount: creditAmount);

            Assert.Equal(expectedValue, criteria.IsValid());
        }
    }
}