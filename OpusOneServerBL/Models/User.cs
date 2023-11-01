using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Pwsd { get; set; } = null!;

    public string Email { get; set; } = null!;
}
