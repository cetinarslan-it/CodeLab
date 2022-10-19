using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Models;

public class Insurance
{
    public int InsuranceId { get; set; }
    public string? Name { get; set; }
    public int Value { get; set; }
    public int? ParentId { get; set; }
    public int Depth { get; set; } = 0;
    [JsonIgnore]
    public virtual Insurance? Parent { get; set; }
    [JsonIgnore]
    public virtual ICollection<Insurance> Children { get; } = new List<Insurance>();
}
