namespace TabML.Core.Document
{
    public class RhythmTemplateSegment : RhythmSegmentBase
    {
        public RhythmTemplate OwnerRhythmTemplate { get; set; }

        private static RhythmSegmentVoice InstantializeVoice(RhythmSegmentVoice voice)
        {
            var instance = voice.Clone();
            instance.ClearRange();
            return instance;
        }

        public RhythmSegment Instantialize()
        {
            var segment = new RhythmSegment();  // do not set Range

            if (this.TrebleVoice != null)
                segment.TrebleVoice = RhythmTemplateSegment.InstantializeVoice(this.TrebleVoice);

            if (this.BassVoice != null)
                segment.BassVoice = RhythmTemplateSegment.InstantializeVoice(this.BassVoice);

            return segment;
        }
    }
}
