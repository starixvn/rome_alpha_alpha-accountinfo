namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectMemberBalanceCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectMemberBalanceCriteria(1);
            var expectedSettingKey = "Acc_BalanceMemberSel";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-2, false)]
        public void IsValid_Always_ReturnsCorrectValue(int searchAccountId, bool expectedResult)
        {
            var criteria = new SelectMemberBalanceCriteria(searchAccountId);

            var actualResult = criteria.IsValid();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}