namespace Alpha.AccountInfo.Tests.Features.Shared
{
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.Shared.Params;
    using Alpha.AccountInfo.Features.Shared.Params.SPs;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class KickoutCustomerServiceTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly KickoutCustomerService kickoutCustomerService;

        public KickoutCustomerServiceTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            kickoutCustomerService = new KickoutCustomerServiceImpl(dynamicRepository);
        }

        [Fact]
        public async Task Kickout_ValidateCriteriaSuccessful_CallsExecute()
        {
            // Arrange
            var param = new KickoutCustomerParam(1, 3, 90, "::1", 0, "ChangeProtectionMode");

            // Act
            await kickoutCustomerService.KickoutAsync(param);

            // Assert
            await dynamicRepository.Received().ExecuteAsync(Arg.Is<KickoutCustomerCriteria>(
                c => c.CustId == param.CustId
                && c.RoleId == param.CustLevelId
                && c.ServerId == param.ServerId
                && c.IP == param.IP
                && c.SubAccId == param.SubAccId
                && c.Mode == param.Mode));
        }
    }
}
