using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class KeyCommandletNode : CommandletNode
    {
        public NoteName Key { get; }

        public KeyCommandletNode(NoteName key)
        {
            this.Key = key;
        }
    }
}
