using Domain.States;

namespace Domain.Models.DoctorDTO
{
    public class DoctorBaseDTO
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string? MedicalHistory { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? MedicalLicenseNumber { get; set; }
    }
}
