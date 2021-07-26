namespace Alpha.AccountInfo.Site.Tests.Features.OpenDownlines.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class OpenMultipleResultViewModelTest
    {
        [Theory]
        [InlineData(false, "test1, test2 and his/her affected downlines have been successfully opened.")]
        [InlineData(true, "test1, test2 and his/her downlines have been successfully closed.")]
        public void Results_SuccessfullyOpenMultiple_ReturnSuccessfullyResult(bool isClosed, string sucessfullyMessage)
        {
            var result = Result.CreateSuccessResult();
            var openSuccessfullyResults = new List<OpenResult>
            {
                new OpenResult("test1", isClosed, result),
                new OpenResult("test2", isClosed, result)
            };

            var expectedViewModel = Result.CreateSuccessResult(sucessfullyMessage);
            var actualViewModel = new OpenMultipleResultViewModel(UserLevel.Super.Value, openSuccessfullyResults);
            AssertExtension.Equal(actualViewModel.Results.FirstOrDefault(), expectedViewModel);
        }

        [Fact]
        public void Results_ErrorCloseMultiple_ReturnErrorResults()
        {
            var openedErrorResult = Result.CreateErrorResult(100, "Opened Error Result");
            var closedErrorResult = Result.CreateErrorResult(100, "Closed Error Result");
            var openSuccessfullyResults = new List<OpenResult>
            {
                new OpenResult("test1", true, openedErrorResult),
                new OpenResult("test2", false, closedErrorResult)
            };

            var expectedViewModel = new List<Result>
            {
                Result.CreateErrorResult(100, "test1: Opened Error Result"),
                Result.CreateErrorResult(100, "test2: Closed Error Result"),
            };

            var actualViewModel = new OpenMultipleResultViewModel(UserLevel.Super.Value, openSuccessfullyResults);
            AssertExtension.Equal(actualViewModel.Results.ToList(), expectedViewModel);
        }
    }
}