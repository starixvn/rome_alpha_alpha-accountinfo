namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class UpdateActorProfilePictureRequestTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Theory]
        [InlineData(-100, "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAIAAACRXR/mAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAkVJREFUeNrsmFlrAkEMgN1xvRFBRK34ouj//0Pqk3ghgme97VdDl+KZ7rZbHyYPwzqTzXxOMknUWSwWkdcTE3lJsVgWy2JZLIv1K+IGN+E4jjGGkefT6XQ8Hhn/E0uA1uv1fD5nZCaZTGazWcaAcG4QJjbudrv9fn+328HxGRPGxGKxt7OIgk/jvjsIdu10OoPBIBqNigdFQDkcDuVyuV6v+8byGfKgjEaj4XB4wSS4TLKEAg/hYbHxfr9nY+csNxUQFFC7qfBXWO9nIZLu2jVGdELFInokxh8ICqiFh0Ug33PftSv9Rb1PrEQi4brugy1ZQgG1sLFyuRw+uqfDEgqhYglZtVpl15sRxiRLKISdt9gvnU43m022JwtIqZGCyEcmWULhH7K8JFVKIflpOp1ut1tm4vE4viuVSpTFBy7+WyyvWsuBSboi0oM3EUEbG6mA5ixesNs28FaqlGyJy743NjhRusLTl4SBJVsSTKvViu5vuVxS+LxaxCr3IJVKZTIZ+kEuo2Tdp5XKf8hLdEMzHo8nkwl3UMLoohB5JwQf9zGfzxcKBfh+dA+0WADhqV6vRxfFgyA+fUtQ6FeLxWKlUuFBeWwqLOlSWq0WXvN+TfzotkKDTxuNBv7VkBmN74ikdrsN03UvqvQ+L/I6RpSNodEcFZE0m818d8BeScAIpjTeV53WZrMJ/tNPvIkpzWm5SnMcfnAsjCi/nqu5TVTfWq2mOXyNKU3Ia29icCaPTIPl/qIt+0eSxbJYFstiWSyVfAgwANaLooANmykXAAAAAElFTkSuQmCC", true)]
        [InlineData(2, "", true)]
        [InlineData(-100, "", false)]
        public void IsValid_Always_ReturnsCorrectValue(int imageId, string image, bool expectedIsValid)
        {
            var messages = new UpdateActorProfilePictureRequest(viewer)
            {
                ImageId = imageId,
                Image = image
            };


            Assert.Equal(messages.IsValid(), expectedIsValid);
        }
    }
}