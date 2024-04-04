using System;
using System.Collections.Generic;

namespace OpusOneServerBL.Models;

public partial class WorksUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int WorkId { get; set; }

    public virtual User User { get; set; } = new();
    
    public virtual Work Work { get; set; } = new();
}
