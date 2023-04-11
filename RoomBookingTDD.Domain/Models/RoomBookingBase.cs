using System.ComponentModel.DataAnnotations;

namespace RoomBokingTDD.Domain.Models
{
    public abstract record RoomBookingBase:IValidatableObject
    {
        [MinLength(80)]
        public string? Name { get; set; }
        [MinLength(80)]
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date<DateTime.Now)
            {
                yield return new ValidationResult ("Date must be in future", new[] { nameof(Date)});
            }
        }
    }
}