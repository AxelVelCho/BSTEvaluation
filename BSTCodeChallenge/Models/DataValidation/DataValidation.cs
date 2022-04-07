using System.ComponentModel.DataAnnotations;

namespace BSTCodeChallenge.Models.DataValidation
{
    public class DataValidation
    {
        [AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
        public class NotEmptyAttribute : ValidationAttribute
        {
            public const string DefaultErrorMessage = "The {0} field must not be empty";
            public NotEmptyAttribute() : base(DefaultErrorMessage) { }

            public override bool IsValid(object value)
            {
                switch (value)
                {
                    case Guid guid:
                        return guid != Guid.Empty;
                    default:
                        return true;
                }
            }
        }
    }
}
