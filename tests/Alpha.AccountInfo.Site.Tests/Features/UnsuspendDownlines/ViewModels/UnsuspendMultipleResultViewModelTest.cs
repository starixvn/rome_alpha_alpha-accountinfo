namespace Alpha.AccountInfo.Site.Tests.Features.UnsuspendDownlines.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class UnsuspendMultipleResultViewModelTest
    {
        [Theory]
        [InlineData(false, "test1, test2 and their affected downlines have been unsuspended successfully.")]
        [InlineData(true, "test1, test2 and their downlines have been suspended successfully.")]
        public void Results_SuccessfullySusependMultiple_ReturnSuccessfullyResult(bool suspended, string sucessfullyMessage)
        {
            var result = Result.CreateSuccessResult();
            var unsuspendSuccessfullyResults = new List<UnsuspendResult>
            {
                new UnsuspendResult("test1", suspended, result),
                new UnsuspendResult("test2", suspended, result)
            };

            var expectedViewModel = Result.CreateSuccessResult(sucessfullyMessage);
            var actualViewModel = new UnsuspendMultipleResultViewModel(UserLevel.Super.Value, unsuspendSuccessfullyResults);
            AssertExtension.Equal(actualViewModel.Results.FirstOrDefault(), expectedViewModel);
        }

        [Fact]
        public void Results_ErrorSusependMultiple_ReturnErrorResults()
        {
            var unsuspendedErrorResult = Result.CreateErrorResult(100, "Unsuspended Error Result");
            var suspendedErrorResult = Result.CreateErrorResult(100, "Suspended Error Result");
            var unsuspendSuccessfullyResults = new List<UnsuspendResult>
            {
                new UnsuspendResult("test1", true, unsuspendedErrorResult),
                new UnsuspendResult("test2", false, suspendedErrorResult)
            };

            var expectedViewModel = new List<Result>
            {
                Result.CreateErrorResult(100, "test1: Unsuspended Error Result"),
                Result.CreateErrorResult(100, "test2: Suspended Error Result"),
            };
                
            var actualViewModel = new UnsuspendMultipleResultViewModel(UserLevel.Super.Value, unsuspendSuccessfullyResults);
            AssertExtension.Equal(actualViewModel.Results.ToList(), expectedViewModel);
        }
    }
}
