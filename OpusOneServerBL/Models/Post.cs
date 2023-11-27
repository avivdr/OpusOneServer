using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Post
{
    public int Id { get; set; }

    public int CreatorId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string? FilePath { get; set; }

    public DateTime UploadDateTime { get; set; }

    public int? Work { get; set; }

    public int? Composer { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
