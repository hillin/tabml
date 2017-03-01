using System.Collections.Generic;
using System.Reflection;

// ReSharper disable ArrangeStaticMemberQualifier
namespace TabML.Core.MusicTheory.String
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
        public static Tuning Standard = new Tuning("Standard", Pitches.E(2), Pitches.A(2), Pitches.D(3), Pitches.G(3), Pitches.B(3), Pitches.E(4));

        [KnownTuning]
        public static Tuning DropD = new Tuning("Drop D", Pitches.D(2), Pitches.A(2), Pitches.D(3), Pitches.G(3), Pitches.B(3), Pitches.E(4));
    }
}
