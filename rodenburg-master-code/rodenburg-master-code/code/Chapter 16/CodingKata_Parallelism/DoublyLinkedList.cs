using System.Collections.Generic;

namespace CodingKata_Parallelism
{
    public class DoublyLinkedList
    {
        public List<Node> Nodes = new List<Node>();
        private const int seatCapacity = 12;

        public IEnumerable<Node> AddNode(string name, string seat)
        {
            if (Nodes.Count == 0)
            {
                Nodes.Add(new Node(null, null, name, seat));
            }
            else
            {
                Node previous = Nodes[^1];
                Node current;

                if (TwinMode())
                {
                    Node twin = Nodes[^(seatCapacity / 2)];
                    current = new Node(previous, twin, name, seat);
                    twin.Twin = current;

                    Nodes.Add(current);
                }
                else
                {
                    current = new Node(previous, null, name, seat);
                    Nodes.Add(current);
                }

                previous.Next = current;
            }

            return Nodes;
        }

        private bool TwinMode() => Nodes.Count > seatCapacity / 2 - 1;
    }

    public class Node
    {
        public Node Next;
        public Node Previous;
        public Node Twin;
        public string Name;
        public string Seat;

        public Node(Node previous, Node twin, string name, string seat)
        {
            Previous = previous;
            Name = name;
            Seat = seat;
            Twin = twin;
        }
    }
}
