using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Role
{
    public int Id { get; set; } 
    public string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; }
    [JsonIgnore]
    public virtual ICollection<Permission> Permissions { get; set; }
}
