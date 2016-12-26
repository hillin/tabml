using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    class HeightMap
    {
        private readonly double[] _heights;
        private readonly double _sampleRateInversed;

        public HeightMap(int width, int sampleRate, double filledHeight = 0)
        {
            _sampleRateInversed = 1.0 / sampleRate;
            _heights = new double[(int)Math.Ceiling((double)width / sampleRate)];
            this.Fill(filledHeight);
        }

        private int GetIndex(double position) => (int)(position * _sampleRateInversed);
        private IEnumerable<int> GetIndices(double position, double size)
            => Enumerable.Range(this.GetIndex(position), (int)Math.Ceiling(size * _sampleRateInversed));
        public double GetHeight(double position) => _heights[this.GetIndex(position)];
        public double GetHeight(double from, double size) => this.GetIndices(from, size).Select(i => _heights[i]).Max();
        public void AddHeight(double position, double height) => _heights[this.GetIndex(position)] += height;
        public void AddHeight(double from, double size, double height)
            => this.SetHeight(from, size, this.GetHeight(from, size) + height);
        public void SetHeight(double position, double height) => _heights[this.GetIndex(position)] = height;
        public void SetHeight(double from, double size, double height)
        {
            foreach (var index in this.GetIndices(from, size))
                _heights[index] = height;
        }

        public void SetHeight(double from, double size, double fromHeight, double toHeight)
        {
            var slope = (toHeight - fromHeight) / size;
            foreach (var index in this.GetIndices(from, size))
            {
                _heights[index] = fromHeight + slope * index / _sampleRateInversed;
            }
        }

        public void EnsureHeight(double position, double height)
        {
            var index = this.GetIndex(position);
            var oldHeight = _heights[index];
            _heights[index] = Math.Max(oldHeight, height);
        }

        public void EnsureHeight(double from, double size, double height)
        {
            foreach (var index in this.GetIndices(from, size))
                _heights[index] = Math.Max(_heights[index], height);
        }

        public void EnsureHeight(double from, double size, double fromHeight, double toHeight)
        {
            var slope = (toHeight - fromHeight) / size;
            foreach (var index in this.GetIndices(from, size))
            {
                _heights[index] = Math.Max(_heights[index], fromHeight + slope * index / _sampleRateInversed);
            }
        }

        public double this[double position]
        {
            get { return this.GetHeight(position); }
            set { this.SetHeight(position, value); }
        }

        public double this[double from, double size]
        {
            get { return this.GetHeight(from, size); }
            set { this.SetHeight(from, size, value); }
        }

        public void Fill(double height)
        {
            for (var i = 0; i < _heights.Length; ++i)
                _heights[i] = height;
        }
    }
}
