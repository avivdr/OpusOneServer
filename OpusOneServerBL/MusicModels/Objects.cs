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
        public DateOnly Birth { get; set; }
        [NotMapped]
        public DateOnly Death { get; set; }
        [NotMapped]
        public string Epoch { get; set; }
        [NotMapped]
        public string Portrait { get; set; }
    }
    public partial class Work
    {
        [NotMapped]
        public string Subtitle { get; set; }
        [NotMapped]
        [JsonIgnore]
        public List<string> SearchTerms { get; set; }
        [NotMapped]
        public string Popular { get; set; }
        [NotMapped]
        public string Recommended { get; set; }
        [NotMapped]
        public string SearchMode { get; set; }
        [NotMapped]
        public string Catalogue { get; set; }
        [NotMapped]
        public string Catalogue_Number { get; set; }
    }
}
