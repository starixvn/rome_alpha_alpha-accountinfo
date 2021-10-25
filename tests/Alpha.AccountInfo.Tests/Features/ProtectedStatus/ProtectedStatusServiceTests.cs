namespace Alpha.AccountInfo.Tests.Features.ProtectedStatus
{
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.ProtectedStatus;
    using Alpha.AccountInfo.Features.ProtectedStatus.Models;
    using Alpha.AccountInfo.Features.ProtectedStatus.Params;
    using Alpha.AccountInfo.Features.ProtectedStatus.Params.SPs;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "ProtectedStatus")]
    public class ProtectedStatusServiceTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SearchAccountService searchAccountService;
        private readonly KickoutCustomerService kickoutCustomerService;

        private readonly ProtectedStatusService protectedStatusService;

        public ProtectedStatusServiceTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            searchAccountService = Substitute.For<SearchAccountService>();
            kickoutCustomerService = Substitute.For<KickoutCustomerService>();

            protectedStatusService = new ProtectedStatusServiceImpl(dynamicRepository, searchAccountService, kickoutCustomerService);
        }

        [Fact]
        public async Task GetProtectedStatusAsync_Always_CallsFetchAsync()
        {
            // Arrange
            var param = new GetProtectedStatusParam(1, 4, "2,3,4,5,6,7");

            // Act
            await protectedStatusService.GetProtectedStatusAsync(param);

            // Assert
            await dynamicRepository.Received().FetchAsync<CustomerProtectedStatus>(Arg.Is<GetProtectedStatusCriteria>(
                c => c.CustId == param.ViewerId
                && c.RoleId == param.ViewerLevelId
                && c.CustList == param.CustIds));
        }

        [Fact]
        public async Task UpdateMultipleAsync_Always_CallsCheckMultipleCustomesBelonging()
        {
            // Arrange
            var param = new UpdateMultipleProtectedStatusParam(
                new[]
                {
                    new CustomerProtectedStatus(1, "master1", 3, false),
                    new CustomerProtectedStatus(2, "master2", 3, true)
                },
                8,
                "super1",
                0,
                "E",
                "::1",
                false,
                3200);

            // Act
            await protectedStatusService.UpdateMultipleAsync(param);

            // Assert
            await searchAccountService.Received().CheckMultipleCustomesBelonging(
                param.ActorId,
                param.SubAccId,
                string.Join("^", param.ProtectedStatus.Select(p => p.CustId)),
                param.IsInternal);
        }

        [Fact]
        public async Task UpdateMultipleAsync_InvalidBelonging_ReturnsNotAuthorized()
        {
            // Arrange
            var param = new UpdateMultipleProtectedStatusParam(
                new[]
                {
                    new CustomerProtectedStatus(1, "master1", 3, false),
                    new CustomerProtectedStatus(2, "master2", 3, true)
                },
                8,
                "super1",
                0,
                "E",
                "::1",
                false,
                3200);
            searchAccountService
                .CheckMultipleCustomesBelonging(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(false);

            // Act
            var result = (await protectedStatusService.UpdateMultipleAsync(param)).FirstOrDefault();

            // Assert
            Assert.True(403 == result.Code
                && "You are not authorized on this function." == result.Message);
        }

        [Fact]
        public async Task UpdateMultipleAsync_AllSuccess_ReturnsSuccessResult()
        {
            // Arrange
            var param = new UpdateMultipleProtectedStatusParam(
                new[]
                {
                    new CustomerProtectedStatus(1, "master1", 3, false),
                    new CustomerProtectedStatus(2, "master2", 3, true)
                },
                8,
                "super1",
                0,
                "E",
                "::1",
                false,
                3200);
            searchAccountService
                .CheckMultipleCustomesBelonging(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(true);
            searchAccountService.UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
                .Returns(Result.CreateSuccessResult());

            // Act
            var result = (await protectedStatusService.UpdateMultipleAsync(param)).FirstOrDefault();

            // Assert
            Assert.Equal("The selected accounts' protection status has been updated successfully.", result.Message);
        }

        [Fact]
        public async Task UpdateMultipleAsync_AllFail_ReturnsFailResults()
        {
            // Arrange
            var param = new UpdateMultipleProtectedStatusParam(
                new[]
                {
                    new CustomerProtectedStatus(1, "master1", 3, false),
                    new CustomerProtectedStatus(2, "master2", 3, true)
                },
                8,
                "super1",
                0,
                "E",
                "::1",
                false,
                3200);
            searchAccountService
                .CheckMultipleCustomesBelonging(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(true);
            searchAccountService.UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
                .Returns(Result.CreateErrorResult(101, "abc"));

            // Act
            var results = (await protectedStatusService.UpdateMultipleAsync(param)).ToList();

            // Assert
            Assert.Equal("master1: abc", results[0].Message);
            Assert.Equal("master2: abc", results[1].Message);
        }
    }
}
