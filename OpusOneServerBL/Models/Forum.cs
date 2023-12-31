﻿using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Forum
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ForumDescription { get; set; } = null!;

    public int CreatorId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int? WorkId { get; set; }

    public int? ComposerId { get; set; }

    public virtual Composer? Composer { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();

    public virtual Work? Work { get; set; }
}
