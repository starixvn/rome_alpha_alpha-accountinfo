namespace Alpha.AccountInfo.Services.ProductStatus
{
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;

    public interface RnGCasinoService
    {
        void Update3rdRnGCasino(int IBCCustID, bool EnableCasino, string FromIP, int ModifyBy, string ModifyUserName, int FromSystem);
    }

    public class RnGCasinoServiceImpl : RnGCasinoService
    {
        public void Update3rdRnGCasino(
            int IBCCustID, 
            bool EnableCasino,
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName, 
            int FromSystem)
        {
            var customer = new Customer();

            var success = customer.UpdateCustEnableCasino(
                IBCCustID,
                EnableCasino,
                false,
                FromIP,
                ModifyBy,
                ModifyUserName,
                FromSystem);

            if (!success)
            {
                throw (new ErrorException($"UpdateCustEnableCasino({IBCCustID}) on CasinoLibrary is failure."));
            }
        }
    }
}
