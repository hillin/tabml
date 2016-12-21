namespace TabML.Editor.Rendering
{
    class BeamSlope
    {
        public double X0 { get; }
        public double Y0 { get; }
        public double Slope { get; }

        public BeamSlope(double x0, double y0, double slope)
        {
            this.X0 = x0;
            this.Y0 = y0;
            this.Slope = slope;
        }

        public double GetY(double x1)
        {
            return this.Slope * (x1 - this.X0) + this.Y0;
        }
    }
}
