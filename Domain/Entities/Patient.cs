namespace Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string? PassportId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
    }
}
