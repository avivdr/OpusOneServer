using OpusOneServerBL.MusicModels;
using OpusOneServerBL.Models;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace OpusOneServer.DTO;

public class OmniSearchDTO
{
    public List<Work>? Works { get; set; }
    public List<Composer>? Composers { get; set; }

    [JsonIgnore]
    public int Next { get; set; }
}
