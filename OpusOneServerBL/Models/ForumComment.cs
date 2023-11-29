using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class ForumComment
{
    public int Id { get; set; }

    public int ForumId { get; set; }

    public int CreatorId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime UploadDateTime { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual Forum Forum { get; set; } = null!;
}
