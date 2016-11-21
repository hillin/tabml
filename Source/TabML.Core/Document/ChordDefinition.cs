using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class ChordDefinition : Element
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ChordFingering Fingering { get; set; }
        public override IEnumerable<Element> Children { get { yield return this.Fingering; } }
        public string GetDisplayName()
        {
            return !string.IsNullOrEmpty(this.DisplayName) ? this.DisplayName : this.Name;
        }
    }
}
