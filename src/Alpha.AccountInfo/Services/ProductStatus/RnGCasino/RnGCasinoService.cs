namespace Alpha.AccountInfo.Services.ProductStatus
{
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Fanex.Logging.Extensions.Web;
    using System;
    using System.Diagnostics;

    public interface RnGCasinoService
    {
        void Update3rdRnGCasinoStatus(
            int IBCCustID, 
            bool EnableCasino, 
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName);

        void Update3rdRnGCasinoClosed(
            int IBCCustID, 
            bool Closed, 
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName);

        void Update3rdRnGCasinoSuspended(
           int IBCCustID,
           bool Suspended,
           string FromIP,
           int ModifyBy,
           string ModifyUserName);
    }

    public class RnGCasinoServiceImpl : RnGCasinoService
    {
        private readonly byte AlphaSystemId = 2;

        public void Update3rdRnGCasinoStatus(
            int IBCCustID, 
            bool EnableCasino,
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName)
        {
            var customer = new Customer();
            try
            {
                customer.UpdateCustEnableCasino(
                        IBCCustID,
                        EnableCasino,
                        false,
                        FromIP,
                        ModifyBy,
                        ModifyUserName,
                        AlphaSystemId);
            }
            catch (ErrorException ex)
            {
                ExceptionHandler.Handle(new Exception(ex.Message, ex), TraceEventType.Information);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e, TraceEventType.Information);
            }
        }

        public void Update3rdRnGCasinoClosed(
            int IBCCustID, 
            bool Closed, 
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName)
        {
            var customer = new Customer();

            try
            {
                customer.UpdateCustClosed(
                       IBCCustID,
                       Closed,
                       FromIP,
                       ModifyBy,
                       ModifyUserName,
                       AlphaSystemId);
            }
            catch (ErrorException ex)
            {
                ExceptionHandler.Handle(new Exception(ex.Message, ex), TraceEventType.Information);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e, TraceEventType.Information);
            }
        }

        public void Update3rdRnGCasinoSuspended(
           int IBCCustID,
           bool Suspended,
           string FromIP,
           int ModifyBy,
           string ModifyUserName)
        {
            var customer = new Customer();

            try
            {
                customer.UpdateCustSuspend(
                       IBCCustID,
                       Suspended,
                       FromIP,
                       ModifyBy,
                       ModifyUserName,
                       AlphaSystemId);
            }
            catch (ErrorException ex)
            {
                ExceptionHandler.Handle(new Exception(ex.Message, ex), TraceEventType.Information);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e, TraceEventType.Information);
            }
        }
    }
}
