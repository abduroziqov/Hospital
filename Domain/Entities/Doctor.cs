namespace Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? MedicalLicenseNumber { get; set; }
    }
}