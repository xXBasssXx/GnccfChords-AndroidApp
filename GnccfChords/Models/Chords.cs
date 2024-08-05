using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnccfChords.Models
{
    public class Chords
    {
        public Guid SongId { get; set; }
        public string IntroChords { get; set; }
        public string? PreChorusChords { get; set; }
        public string ChorusChords { get; set; }
        public string? BridgeChords { get; set; }
        public char ChordKey { get; set; }

    }
}
