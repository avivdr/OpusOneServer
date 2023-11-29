using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Forum
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ForumDescription { get; set; } = null!;

    public int CreatorId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int? Work { get; set; }

    public int? Composer { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();
}
