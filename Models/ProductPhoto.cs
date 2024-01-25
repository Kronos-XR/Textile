using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Textile.Models;

public partial class ProductPhoto
{
    public int ProductId { get; set; }

    public byte[] Photo { get; set; } = null!;
    [JsonIgnore]

    public virtual Product? Product { get; set; } 
}
