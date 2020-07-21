namespace Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers
{
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "NumberHelpers")]
    public class NumberHelpersTest
    {
        [Theory]
        [InlineData(1232.142, 1232.15)]
        [InlineData(1232.146, 1232.15)]
        public void RoundUp_Always_ReturnCorrectValue(decimal inputValue, decimal expectedValue)
        {
            var helper = new NumberHelpersImpl();
            var outputValue = helper.RoundUp(inputValue);
            Assert.Equal(outputValue, expectedValue);
        }

        [Theory]
        [InlineData(1232.142, 1232.14)]
        [InlineData(1232.146, 1232.14)]
        public void RoundDown_Always_ReturnCorrectValue(decimal inputValue, decimal expectedValue)
        {
            var helper = new NumberHelpersImpl();
            var outputValue = helper.RoundDown(inputValue);
            Assert.Equal(outputValue, expectedValue);
        }
    }
}