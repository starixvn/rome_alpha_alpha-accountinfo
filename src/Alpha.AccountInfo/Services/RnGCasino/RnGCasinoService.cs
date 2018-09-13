namespace Alpha.AccountInfo.Services
{
    using Alpha.AccountInfo.Params.ProductStatus;
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Fanex.Logging.Extensions.Web;
    using System;
    using System.Diagnostics;
    using static Alpha.AccountInfo.Constants.AccountInfoDataKey;

    public interface RnGCasinoService
    {
        void SyncedToRnGCasino3rdByStatus(UpdateRnGCasino3rdParam param);
    }

    public class RnGCasinoServiceImpl : RnGCasinoService
    {
        public void SyncedToRnGCasino3rdByStatus(UpdateRnGCasino3rdParam param)
        {
            try
            {
                var customer = new Customer();
                var AlphaSystemId = 2;

                switch (param.StatusName)
                {
                    case StatusMapping.Closed:
                        customer.UpdateCustClosed(
                        param.SearchAccountId,
                        param.StatusValue,
                        param.IP,
                        param.ActorId,
                        param.ActorName,
                        AlphaSystemId);
                        break;

                    case StatusMapping.Suspended:
                        customer.UpdateCustSuspend(
                         param.SearchAccountId,
                         param.StatusValue,
                         param.IP,
                         param.ActorId,
                         param.ActorName,
                         AlphaSystemId);
                        break;

                    case StatusMapping.EnableRnGCasino3rd:
                        customer.UpdateCustEnableCasino(
                        param.SearchAccountId,
                        param.StatusValue,
                        false,
                        param.IP,
                        param.ActorId,
                        param.ActorName,
                        AlphaSystemId);
                        break;

                    default:
                        break;
                }

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
