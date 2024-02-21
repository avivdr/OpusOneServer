using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpusOneServerBL.Models;

public partial class Composer
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonPropertyName("Complete_Name")]
    public string CompleteName { get; set; } = null!;

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
