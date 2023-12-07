using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpusOneServerBL.Models;

public partial class Post
{
    [NotMapped]
    [JsonIgnore]
    public FileStream File { get; set; }
}

