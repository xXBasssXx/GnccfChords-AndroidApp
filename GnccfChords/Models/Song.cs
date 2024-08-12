﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnccfChords.Models
{
    public class Song
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; }
        public string? Artist { get; set; }
        public string? SongKey { get; set; }
    }
}
