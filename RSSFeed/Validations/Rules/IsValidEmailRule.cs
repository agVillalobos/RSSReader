using System;
using System.Text.RegularExpressions;
using RSSFeed.Interfaces;

namespace RSSFeed.Validations.Rules
{
    public class IsValidEmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexEmail { get; set; } = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public bool Check(T value)
        {
            try
            {
                var email = value as string;
                Match match = RegexEmail.Match(email);

                return match.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
