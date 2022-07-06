using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RSSFeed.Interfaces
{
    public interface IValidationService<T> : INotifyPropertyChanged
    {
        IList<IValidationRule<T>> Validations { get; }
        IList<string> Errors { get; set; }
        bool Validate();
        bool IsValid { get; set; }
    }
}
