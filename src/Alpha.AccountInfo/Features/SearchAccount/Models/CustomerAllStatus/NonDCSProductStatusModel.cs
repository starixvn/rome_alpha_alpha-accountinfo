namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class NonDCSProductStatusModel
    {
        public NonDCSProductStatusModel() { }

        public NonDCSProductStatusModel(
            bool syncCasino,
            bool disableCasino,
            bool uplineDisableCS,
            bool disableNumber,
            bool uplineDisableNumber,
            bool disableEsports,
            bool uplineDisableEsports,
            bool disableColossusBet,
            bool hideColossusBet,
            bool uplineDisableColossusBet,
            bool hideKeno)
        {
            SyncCasino = syncCasino;
            DisableCasino = disableCasino;
            UplineDisableCS = uplineDisableCS;

            DisableNumber = disableNumber;
            UplineDisableNumber = uplineDisableNumber;

            DisableEsports = disableEsports;
            UplineDisableEsports = uplineDisableEsports;

            DisableColossusBet = disableColossusBet;
            HideColossusBet = hideColossusBet;
            UplineDisableColossusBet = uplineDisableColossusBet;

            HideKeno = hideKeno;
        }

        public bool SyncCasino { get; private set; }

        public bool DisableCasino { get; private set; }

        public bool UplineDisableCS { get; private set; }

        public bool DisableNumber { get; private set; }

        public bool UplineDisableNumber { get; private set; }

        public bool DisableEsports { get; private set; }

        public bool UplineDisableEsports { get; private set; }

        public bool DisableColossusBet { get; private set; }

        public bool HideColossusBet { get; private set; }

        public bool UplineDisableColossusBet { get; private set; }

        public bool HideKeno { get; private set; }
    }
}
