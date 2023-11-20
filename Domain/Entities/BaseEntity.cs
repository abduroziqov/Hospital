using Domain.States;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string? MedicalHistory { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
