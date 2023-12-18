using OpusOneServerBL.MusicModels;
using OpusOneServerBL.Models;
using System.Runtime.CompilerServices;

namespace OpusOneServer.DTO;

public class OmniSearchDTO
{
    public List<Work>? Works { get; set; }
    public List<Composer>? Composers { get; set; }
}
