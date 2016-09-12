using System.Collections.Generic;
using System.Reflection;
using static TabML.Core.MusicTheory.Pitches;
// ReSharper disable ArrangeStaticMemberQualifier
namespace TabML.Core.MusicTheory
{
    public static class Tunings
    {
        private static readonly Dictionary<string, Tuning> KnownTunings
            = new Dictionary<string, Tuning>();

        private static string NormalizeTuningName(string tuningName)
        {
            return tuningName.Replace(' ', '-').ToLowerInvariant();
        }

        static Tunings()
        {
            foreach (var field in typeof(Tunings).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<KnownTuningAttribute>();
                if (attribute != null)
                {
                    var tuning = (Tuning)field.GetValue(null);
                    KnownTunings[Tunings.NormalizeTuningName(tuning.Name)] = tuning;
                }
            }
        }

        public static Tuning GetKnownTuning(string name)
        {
            Tuning tuning;
            if (KnownTunings.TryGetValue(Tunings.NormalizeTuningName(name), out tuning))
                return tuning;

            return null;
        }

        [KnownTuning]
        public static Tuning Standard = new Tuning("Standard", E(2), A(2), D(3), G(3), B(3), E(4));

        [KnownTuning]
        public static Tuning DropD = new Tuning("Drop D", D(2), A(2), D(3), G(3), B(3), E(4));
    }
}
