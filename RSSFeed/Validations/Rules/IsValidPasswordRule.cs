using System;
using System.Text.RegularExpressions;
using RSSFeed.Interfaces;

namespace RSSFeed.Validations.Rules
{
    public class IsValidPasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Regex RegexPassword { get; set; } = new Regex("(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*¿?\\-\\(\\)&]{5,7}");

        public bool Check(T value) =>(RegexPassword.IsMatch($"{value}"));
    }
}
