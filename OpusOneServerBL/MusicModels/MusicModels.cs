using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using OpusOneServerBL.Models;

namespace OpusOneServerBL.MusicModels
{
    public class Request
    {
        public string Type { get; set; }
        public string Item { get; set; }
        public string Search { get; set; }
        public string Offset { get; set; }
    }
    public class Status
    {
        public string Version { get; set; }
        public string Success { get; set; }
        public string Error { get; set; }
        public string Source { get; set; }
        public int Rows { get; set; }
        public float ProcessingTime { get; set; }
        public string Api { get; set; }
    }

   
    #region Requests
    public class ComposerResult
    {
        public Status Status { get; set; }
        public Request Request { get; set; }
        public List<Composer> Composers { get; set; }
    }

    public class GenreResult
    {
        public Status Status { get; set; }
        public Composer Composer { get; set; }
        public List<string> Genres { get; set; }
    }

    public class WorkResult
    {
        public Status Status { get; set; }
        public Request Request { get; set; }
        public Composer Composer { get; set; }
        public List<Work> Works { get; set; }
    }

    public class OmniSearchResult
    {
        public Status Status { get; set; }
        public Request Request { get; set; }
        public List<OmniSearchItem> Results { get; set; }
        public int Next { get; set; }
    }

    public class OmniSearchItem
    {
        public Composer Composer { get; set; }
        public Work Work { get; set; }
    }
    #endregion
}