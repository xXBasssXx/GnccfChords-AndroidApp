using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnccfChords.Models
{
    public class SongChords
    {
        public Guid? ChordPartId { get; set; }
        public string SongName { get; set; }
        public string ChordKey { get; set; }
        public string Chords { get; set; }
    }
}
