namespace Alpha.AccountInfo.Features.Shared.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.Products.Core.Shared;

    [Serializable]
    public class UserLevel : Enumeration
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Reviewed!")]
        public static readonly UserLevel Super = new UserLevel(4, "Super");

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Reviewed!")]
        public static readonly UserLevel Master = new UserLevel(3, "Master");

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Reviewed!")]
        public static readonly UserLevel Agent = new UserLevel(2, "Agent");

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Reviewed!")]
        public static readonly UserLevel Member = new UserLevel(1, "Member");

        public UserLevel()
        {
        }

        private UserLevel(byte value, string displayName)
            : base(value, displayName)
        {
        }

        public string GetDisplayName()
        {
            if (IsMemberLevel())
            {
                return Resources.Member;
            }

            if (IsAgentLevel())
            {
                return Resources.Agent;
            }

            if (IsMasterLevel())
            {
                return Resources.Master;
            }

            if (IsSuperLevel())
            {
                return Resources.Super;
            }

            return string.Empty;
        }

        public bool IsMemberLevel() => this == Member;

        public bool IsNotMemberLevel() => !IsMemberLevel();

        public bool IsAgentLevel() => this == Agent;

        public bool IsNotAgentLevel() => !IsAgentLevel();

        public bool IsMasterLevel() => this == Master;

        public bool IsNotMasterLevel() => !IsMasterLevel();

        public bool IsSuperLevel() => this == Super;

        public bool IsNotSuperLevel() => !IsSuperLevel();

        public bool IsSuperOrMasterLevel() => IsSuperLevel() || IsMasterLevel();

        public bool IsDownlineOfSuperLevel() => IsMasterLevel() || IsAgentLevel() || IsMemberLevel();

        public bool IsDownlineOfMasterLevel() => IsAgentLevel() || IsMemberLevel();

        public bool IsDirectDownlineOf(Enumeration uplineLevel)
            => (uplineLevel != null) && GapBetweenTwoLevels(uplineLevel.Value, Value) == 1;

        private static int GapBetweenTwoLevels(byte uplineLevelValue, byte downlineLevelValue)
            => uplineLevelValue - downlineLevelValue;
    }
}