using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    class GapCollection : IEnumerable<GapCollection.IGap>
    {
        public interface IGap
        {
            double Left { get; }
            double Right { get; }
        }

        private class Node : IGap
        {
            public Node Previous { get; private set; }
            public Node Next { get; private set; }

            public double Left { get; }
            public double Right { get; }
            public Node(double left, double right)
            {
                this.Left = left;
                this.Right = right;
            }

            public void InsertBefore(Node node)
            {
                this.Previous?.SetNext(node);

                this.Previous = node;
                node.Next = this;
            }

            public void InsertAfter(Node node)
            {
                this.Next?.SetPrevious(node);

                this.Next = node;
                node.Previous = this;
            }

            public void SetNext(Node next)
            {
                if (this.Next != null)
                    this.Next.Previous = null;

                this.Next = next;

                if (next != null)
                    next.Previous = this;
            }

            private void SetPrevious(Node previous)
            {
                if (this.Previous != null)
                    this.Previous.Next = null;

                this.Previous = previous;

                if (previous != null)
                    previous.Next = this;
            }
        }

        private Node _head;


        public void Add(double left, double right)
        {
            if (_head == null)
            {
                _head = new Node(left, right);
                return;
            }

            Node before = null;
            Node after = null;

            for (var node = _head; node != null; node = node.Next)
            {
                if (node.Left <= left && node.Right >= right)   // the new gap is contained by this node
                    return;

                if (node.Right < left)
                {
                    before = node;
                    continue;
                }

                if (node.Left <= left && node.Right >= left)
                {
                    left = node.Left;
                    before = node.Previous;
                    continue;
                }

                if (node.Left <= right && node.Right >= right)
                {
                    right = node.Right;
                    after = node.Next;
                    break;
                }

                if (node.Left > right)
                {
                    after = node;
                    break;
                }
            }

            var newNode = new Node(left, right);

            if (before == null)
            {
                _head.InsertBefore(newNode);
                _head = newNode;
            }
            else
            {
                before.InsertAfter(newNode);
            }

            newNode.SetNext(after);
        }

        public void Clear()
        {
            _head = null;
        }

        public IEnumerator<IGap> GetEnumerator()
        {
            if (_head == null)
                yield break;

            for (var node = _head; node != null; node = node.Next)
                yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
