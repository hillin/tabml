using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    public interface IChordDefinition
    {
        string DisplayName { get; }
        IChordFingering Fingering { get; }
    }
}
