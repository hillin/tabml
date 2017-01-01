using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private IEnumerable<int> GetIndicesGuarded(double position, double size)
        {
            var fromIndex = Math.Max(0, this.GetIndex(position));
            var toIndex = Math.Min(_heights.Length - 1, this.GetIndex(position + size));
            return Enumerable.Range(fromIndex, toIndex - fromIndex + 1);
        }

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

        public void EnsureHeight(double from, double size, double fromHeight, double toHeight, double hMargin)
        {
            var slope = (toHeight - fromHeight) / size;

            foreach (var index in this.GetIndicesGuarded(from - hMargin, hMargin))
            {
                _heights[index] = Math.Max(_heights[index], fromHeight);
            }

            foreach (var index in this.GetIndices(from, size))
            {
                _heights[index] = Math.Max(_heights[index], fromHeight + slope * (index / _sampleRateInversed - from));
            }

            foreach (var index in this.GetIndicesGuarded(from + size , hMargin))
            {
                _heights[index] = Math.Max(_heights[index], toHeight);
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

        public List<Point> DebugGetVertices()
        {
            var vertices = new List<Point>();

            if (_heights.Length == 0)
                return vertices;

            var previousHeight = _heights[0];
            vertices.Add(new Point(0, previousHeight));

            var lastIndex = _heights.Length - 1;
            for (var i = 1; i < lastIndex; i++)
            {
                var height = _heights[i];
                if (Math.Abs(height - previousHeight) > 1e-3)
                {
                    var x = i / _sampleRateInversed;
                    vertices.Add(new Point(x, previousHeight));
                    vertices.Add(new Point(x, height));
                    previousHeight = height;
                }
            }

            vertices.Add(new Point(lastIndex /_sampleRateInversed, _heights[lastIndex]));

            return vertices;
        }
    }
}
