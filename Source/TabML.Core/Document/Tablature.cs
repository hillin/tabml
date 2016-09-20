using System.Collections.Generic;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Document
{
    public class Tablature
    {
        public int Strings { get; set; }
        public MusicTheory.Tuning Tuning { get; set; }
        public List<ChordDefinition> ChordDefinitions { get; }
        public List<Staff> Staffs { get; }

        public Tablature()
        {
            this.ChordDefinitions = new List<ChordDefinition>();
            this.Staffs = new List<Staff>();
        }
    }
}
