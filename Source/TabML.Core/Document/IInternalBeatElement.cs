using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    interface IInternalBeatElement : IBeatElement
    {
        void SetOwnerBeam(Beam owner);
        IInternalBeatElement Clone();
    }
}
