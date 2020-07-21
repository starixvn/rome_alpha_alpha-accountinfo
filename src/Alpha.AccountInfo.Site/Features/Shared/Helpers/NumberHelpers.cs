namespace Alpha.AccountInfo.Site.Features.Shared.Helpers
{
    using System;

    public interface NumberHelpers
    {
        decimal RoundUp(decimal value);
        decimal RoundDown(decimal value);
    }

    public class NumberHelpersImpl : NumberHelpers
    {
        private readonly int DevidedNumber = 100;
        public decimal RoundUp(decimal value) => Math.Ceiling(value * DevidedNumber) / DevidedNumber;
        public decimal RoundDown(decimal value) => Math.Floor(value * DevidedNumber) / DevidedNumber;
    }
}