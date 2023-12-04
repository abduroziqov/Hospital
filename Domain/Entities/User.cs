using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    [JsonIgnore]
    public virtual ICollection<Role> Roles { get; set;} 
}
