using System;
using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class ChordDefinition : Element, IChordDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        string IChordDefinition.DisplayName => this.GetDisplayName();
        public ChordFingering Fingering { get; set; }
        IChordFingering IChordDefinition.Fingering => this.Fingering;
        public override IEnumerable<Element> Children { get { yield return this.Fingering; } }

        public string GetDisplayName()
        {
            return !string.IsNullOrEmpty(this.DisplayName) ? this.DisplayName : this.Name;
        }
    }
}
