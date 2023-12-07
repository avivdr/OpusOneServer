using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Work
{
    public int Id { get; set; }

    public int ComposerId { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public virtual Composer Composer { get; set; } = null!;

    public virtual ICollection<WorksUser> WorksUsers { get; set; } = new List<WorksUser>();
}
