namespace Alpha.AccountInfo.Services.ProductStatus
{
    using Alpha.AccountInfo.Params.ProductStatus;
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Fanex.Logging.Extensions.Web;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Alpha.AccountInfo.Constants.AccountInfoDataKey;

    public interface RnGCasinoService
    {
        void Update3rdRnGCasinoStatus(
            int IBCCustID, 
            bool EnableCasino, 
            string FromIP, 
            int ModifyBy, 
            string ModifyUserName);

        void SyncedTo3rdRnGCasinoByStatus(UpdateCustomerStatusParam param);
    }

    public class RnGCasinoServiceImpl : RnGCasinoService
    {
        private readonly byte AlphaSystemId = 2;
        private readonly IDictionary<string, Action<UpdateCustomerStatusParam>> SyncedCasinoByStatus;

        public RnGCasinoServiceImpl(): this(null)
        {
        }

        internal RnGCasinoServiceImpl(IDictionary<string, Action<UpdateCustomerStatusParam>> SyncedCasinoByStatus)
        {
            this.SyncedCasinoByStatus = SyncedCasinoByStatus ?? 
            new Dictionary<string, Action<UpdateCustomerStatusParam>>
            {
                {StatusMapping.Closed,  Update3rdRnGCasinoClosed},
                {StatusMapping.Suspended,  Update3rdRnGCasinoSuspended}
            };
        }
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

        public void SyncedTo3rdRnGCasinoByStatus(UpdateCustomerStatusParam param)
        {
            try
            {
                SyncedCasinoByStatus[param.StatusName](param);

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

        public void Update3rdRnGCasinoClosed(UpdateCustomerStatusParam param)
        {
            var customer = new Customer();

            customer.UpdateCustClosed(
                        param.SearchAccountId,
                        param.StatusValue,
                        param.IP,
                        param.ActorId,
                        param.ActorName,
                        AlphaSystemId);
        }

        public void Update3rdRnGCasinoSuspended(UpdateCustomerStatusParam param)
        {
            var customer = new Customer();

            customer.UpdateCustSuspend(
                     param.SearchAccountId,
                     param.StatusValue,
                     param.IP,
                     param.ActorId,
                     param.ActorName,
                     AlphaSystemId);
        }
    }
}
