﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpusOneServerBL.Models
{
    public partial class Composer
    {
        public string Birth { get; set; }
        public string Death { get; set; }
        public string Epoch { get; set; }
        public string Portrait { get; set; }
    }
    public partial class Work
    {
        public string Subtitle { get; set; }
        public string Searchterms { get; set; }
        public string Popular { get; set; }
        public string Recommended { get; set; }
        public string SearchMode { get; set; }
        public string Catalogue { get; set; }
        public string Catalogue_Number { get; set; }
    }
}