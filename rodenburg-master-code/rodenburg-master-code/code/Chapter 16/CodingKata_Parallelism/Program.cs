using System;
using System.Threading.Tasks;

namespace CodingKata_Parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList list = new DoublyLinkedList();
            list.AddNode("Scrooge", "1A");
            list.AddNode("Colin", "1B");
            list.AddNode("Harry", "1C");
            list.AddNode("Olivia", "1D");
            list.AddNode("Ziya", "1E");
            list.AddNode("Blair", "1F");
            list.AddNode("Bubba", "2A");
            list.AddNode("Erica", "2B");
            list.AddNode("Daisy", "2C");
            list.AddNode("Mike", "2D");
            list.AddNode("Keith", "2E");
            list.AddNode("Mark", "2F");

            int[][] adjacencyMatrix = CalculateAdjacencyMatrix(list);
            PrintAdjacencyMatrix(adjacencyMatrix);
        }

        private static int[][] CalculateAdjacencyMatrix(DoublyLinkedList list)
        { 
            int[][] matrix = new int[list.Nodes.Count][];

            Parallel.For(0, list.Nodes.Count, i =>
            {
               {
                   Node current = list.Nodes[i];
                   matrix[i] = new int[list.Nodes.Count];

                   for (int y = 0; y < list.Nodes.Count; y++)
                   {
                       Node target = list.Nodes[y];
                       if (current == target) continue;

                       int count = 0;
                       Node temp = current;

                       while (temp != target)
                       {
                           count++;
                           temp = y > i ? temp.Next : temp.Previous;
                       }

                       int countTwin = 1;
                       temp = current.Twin;
                       int index = y;
                       while (temp != target)
                       {
                           countTwin++;
                           if (temp.Next == null)
                           {
                               index = 0;
                           }

                           if (temp.Previous == null)
                           {
                               index = list.Nodes.Count;
                           }


                           temp = index > i ? temp.Next : temp.Previous;
                       }

                       matrix[i][y] = count < countTwin ? count : countTwin;
                   }
               }
            });

            return matrix;
        }

        private static void PrintAdjacencyMatrix(int[][] matrix)
        {
            foreach (int[] row in matrix)
            {
                foreach (int column in row)
                {
                    Console.Write($"{column}\t");
                }

                Console.WriteLine();
            }
        }
    }
}
