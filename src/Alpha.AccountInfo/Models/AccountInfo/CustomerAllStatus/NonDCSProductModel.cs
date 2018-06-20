namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class NonDCSProductModel
    {
        public NonDCSProductModel(
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

        public bool SyncCasino { get; }

        public bool DisableCasino { get; }

        public bool UplineDisableCS { get; }

        public bool DisableNumber { get; }

        public bool UplineDisableNumber { get; }

        public bool DisableEsports { get; }

        public bool UplineDisableEsports { get; }

        public bool DisableColossusBet { get; }

        public bool HideColossusBet { get; }

        public bool UplineDisableColossusBet { get; }

        public bool HideKeno { get; }
    }
}
