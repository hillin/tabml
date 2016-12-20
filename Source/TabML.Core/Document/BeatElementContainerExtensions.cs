using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    internal static class BeatElementContainerExtensions
    {
        public static Beat GetFirstBeat(this IBeatElementContainer container)
        {
            while (container.Elements != null && container.Elements.Count > 0)
            {
                var firstElement = container.Elements[0];
                var firstBeat = firstElement as Beat;
                if (firstBeat != null)
                    return firstBeat;

                container = (IBeatElementContainer)firstElement;
            }

            return null;
        }

        public static Beat GetLastBeat(this IBeatElementContainer container)
        {
            while (container.Elements != null && container.Elements.Count > 0)
            {
                var lastElement = container.Elements[container.Elements.Count - 1];
                var firstBeat = lastElement as Beat;
                if (firstBeat != null)
                    return firstBeat;

                container = (IBeatElementContainer)lastElement;
            }

            return null;
        }
    }
}
