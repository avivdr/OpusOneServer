﻿using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int CreatorId { get; set; }

    public string Content { get; set; } = "";

    public DateTime UploadDateTime { get; set; }

    public virtual User Creator { get; set; } = new();

    public virtual Post Post { get; set; } = new();
}
