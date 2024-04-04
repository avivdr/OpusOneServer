﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpusOneServerBL.Models;

public partial class Work
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int Id { get; set; }

    public int ComposerId { get; set; }

    public string Title { get; set; } = "";

    public virtual Composer Composer { get; set; } = new();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<WorksUser> WorksUsers { get; set; } = new List<WorksUser>();
}
