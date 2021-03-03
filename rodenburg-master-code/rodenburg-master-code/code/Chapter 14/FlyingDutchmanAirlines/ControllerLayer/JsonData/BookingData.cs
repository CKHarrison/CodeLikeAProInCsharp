using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlyingDutchmanAirlines.ControllerLayer.JsonData
{
    public class BookingData : IValidatableObject
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (IsValidName(value))
                    _firstName = value;
            }
        }

        private string _lastName;
        
        public string LastName
        {
            get => _lastName;
            set
            {
                if (IsValidName(value))
                    _lastName = value;
            }
        }

        private bool IsValidName(string name) => !string.IsNullOrEmpty(name);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (FirstName == null && LastName == null)
            {
                results.Add(new ValidationResult("All given data points are null"));
            }
            else if (FirstName == null || LastName == null)
            {
                results.Add(new ValidationResult("One of the given data points is null"));
            }

            return results;
        }
    }
}
