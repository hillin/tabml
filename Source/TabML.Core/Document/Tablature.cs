using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Tablature
    {
        public int Strings { get; set; }
        public Tuning Tuning { get; set; }
        public List<ChordDefinition> ChordDefinitions { get; }
        public List<Staff> Staffs { get; }

        public Tablature()
        {
            this.ChordDefinitions = new List<ChordDefinition>();
            this.Staffs = new List<Staff>();
        }
    }
}
