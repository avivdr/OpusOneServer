using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpusOneServerBL.Models
{
    public partial class Composer
    {
        [NotMapped]
        public DateTime Birth { get; set; }
        [NotMapped]
        public DateTime? Death { get; set; }
        [NotMapped]
        public string? Epoch { get; set; }
        [NotMapped]
        public string? Portrait { get; set; }
    }
    public partial class Work
    {
        [NotMapped]
        public string? Subtitle { get; set; }
        //[NotMapped]
        //public List<string> SearchTerms { get; set; }
        [NotMapped]
        public string? Popular { get; set; }
        [NotMapped]
        public string? Recommended { get; set; }
        [NotMapped]
        public string? SearchMode { get; set; }
        [NotMapped]
        public string? Catalogue { get; set; }
        [NotMapped]
        [JsonPropertyName("Catalogue_Number")]
        public string? CatalogueNumber { get; set; }
    }
}
