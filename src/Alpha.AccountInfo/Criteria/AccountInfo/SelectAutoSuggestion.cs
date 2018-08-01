namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectAutoSuggestion : CriteriaBase
    {
        public SelectAutoSuggestion(int actorId, string suggestionQuery, bool isInternal)
        {
            ActorId = actorId;
            SuggestionQuery = suggestionQuery;
            IsInternal = isInternal;
        }

        [SpParam(SqlParamName = "CustId")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "username")]
        public string SuggestionQuery { get; }

        [SpParam(SqlParamName = "isInternalAgent")]
        public bool IsInternal { get; }

        public override string GetSettingKey() => "Age_CustomerSelUserSearch";

        public override bool IsValid() => ActorId > 0 && !string.IsNullOrEmpty(SuggestionQuery);
    }
}
