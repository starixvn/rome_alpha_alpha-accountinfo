namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class ProfilePictureLibraryQueryTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void IsValid_Always_ReturnsCorrectValue()
        {
            var messages = new ProfilePictureLibraryQuery(viewer)
            {
                CurrentPage = 1
            };

            Assert.True(messages.IsValid());
        }
    }
}