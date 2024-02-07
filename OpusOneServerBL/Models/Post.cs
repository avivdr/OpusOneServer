using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Post
{
    public int Id { get; set; }

    public int CreatorId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime UploadDateTime { get; set; }

    public string? FileExtention { get; set; }

    public int? WorkId { get; set; }

    public int? ComposerId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Composer? Composer { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual Work? Work { get; set; }
}
