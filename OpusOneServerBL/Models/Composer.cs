using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Composer
{
    public string Id { get; set; }

    public string Name { get; set; } = null!;

    public string CompleteName { get; set; } = null!;

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
