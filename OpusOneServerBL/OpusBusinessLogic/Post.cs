using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpusOneServerBL.Models;

public partial class Post
{
    [JsonIgnore]
    public FileStream? File { get; set; }
}

