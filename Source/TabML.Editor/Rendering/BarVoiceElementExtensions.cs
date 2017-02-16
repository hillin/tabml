using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BarVoiceElementExtensions
    {
        public static VoicePart GetStemRenderVoicePart(this IBarVoiceElement element)
        {
            return element.OwnerBar.HasSingularVoice() ? VoicePart.Bass : element.VoicePart;
        }
    }
}
