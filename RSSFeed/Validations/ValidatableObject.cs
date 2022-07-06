using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RSSFeed.Interfaces;

namespace RSSFeed.Validations
{
    public class ValidatableObject<T> : IValidationService<T>
    {
        T value;
        private bool isValid;
        private IList<string> errors;
        private bool cleanOnChange;

        public ValidatableObject()
        {
            Errors = new List<string>();
            CleanOnChange = true;
            IsValid = true;
            Validations = new List<IValidationRule<T>>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<IValidationRule<T>> Validations { get; }

        public IList<string> Errors
        {
            get => errors;
            set => SetProperty(ref errors, value);
        }

        public bool CleanOnChange
        {
            get => cleanOnChange;
            set => SetProperty(ref cleanOnChange, value);
        }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;

                if (CleanOnChange)
                    IsValid = true;

                SetProperty(ref this.value, value);
            }
        }

        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        public virtual bool Validate()
        {
            Errors.Clear();
            var errors = Validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public override string ToString() => $"{Value}";

        private bool SetProperty<TProp>(ref TProp oldValue, TProp newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TProp>.Default.Equals(oldValue, newValue)) return false;

            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }
    }
}
