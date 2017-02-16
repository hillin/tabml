﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    internal interface IBeatElementContainer : IBarVoiceElement
    {
        List<IBeatElement> Elements { get; }
    }
}
