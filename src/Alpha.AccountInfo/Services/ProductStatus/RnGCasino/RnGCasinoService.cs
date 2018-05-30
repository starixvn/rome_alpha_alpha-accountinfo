namespace Alpha.AccountInfo.Services.ProductStatus
{
    using CasinoLibrary.SBAgent;

    public interface RnGCasinoService
    {
        bool UpdateCasino3rd(int IBCCustID, bool EnableCasino, string FromIP, int ModifyBy, string ModifyUserName, int FromSystem);
    }

    public class RnGCasinoServiceImpl : RnGCasinoService
    {
        public bool UpdateCasino3rd(
            int IBCCustID, 
            bool EnableCasino,
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName, 
            int FromSystem)
        {
            var customer = new Customer();

            return customer.UpdateCustEnableCasino(
                IBCCustID,
                EnableCasino,
                false,
                FromIP,
                ModifyBy,
                ModifyUserName,
                FromSystem);
        }
    }
}
