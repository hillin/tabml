using System.Collections.Generic;
using System.Reflection;
using static TabML.Core.MusicTheory.Pitches;
namespace TabML.Core.MusicTheory
{
    public static class Tunings
    {
        private static readonly Dictionary<string, Tuning> NamedTunings
            = new Dictionary<string, Tuning>();

        private static string NormalizeTuningName(string tuningName)
        {
            return tuningName.Replace(' ', '-').ToLowerInvariant();
        }

        static Tunings()
        {
            foreach (var field in typeof(Tunings).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<NamedTuningAttribute>();
                if (attribute != null)
                    NamedTunings[Tunings.NormalizeTuningName(attribute.Name)] = (Tuning)field.GetValue(null);
            }
        }

        public static Tuning GetNamedTuning(string name)
        {
            Tuning tuning;
            if (NamedTunings.TryGetValue(Tunings.NormalizeTuningName(name), out tuning))
                return tuning;

            return null;
        }

        [NamedTuning("Standard")]
        public static Tuning Standard = new Tuning(E(2), A(2), D(3), G(3), B(3), E(4));

        [NamedTuning("Drop D")]
        public static Tuning DropD = new Tuning(D(2), A(2), D(3), G(3), B(3), E(4));
    }
}
