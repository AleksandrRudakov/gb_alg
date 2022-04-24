using System;

namespace less_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Инициализация двусвязного списка:");

            Node test = new Node(51);
            test.AddNode(35);
            test.AddNode(41);
            test.AddNode(5);
            test.AddNode(7);
            test.AddNode(12);
            test.AddNode(23);
            test.AddNode(79);
            test.AddNode(90);
            test.AddNode(53);

            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция AddNode(45)");
            test.AddNode(45);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция GetCount()");
            Console.WriteLine(test.GetCount());

            Console.WriteLine("\nФукнция FindNode(12)");
            Node nodeResultSearch = test.FindNode(12);
            Console.WriteLine($"nodeResultSearch.Value = {nodeResultSearch.Value}");

            Console.WriteLine("\nФукнция AddNodeAfter(nodeResultSearch, 47)");
            test.AddNodeAfter(nodeResultSearch, 47);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция RemoveNode(nodeResultSearch)");
            test.RemoveNode(nodeResultSearch);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция RemoveNode(0)");
            test.RemoveNode(0);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция GetCount()");
            Console.WriteLine(test.GetCount());

            Console.WriteLine($"\nФукнция RemoveNode({test.GetCount()-1})");
            test.RemoveNode(test.GetCount()-1);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция RemoveNode(3)");
            test.RemoveNode(3);
            Console.WriteLine(test.PrintNode());

            Console.WriteLine("\nФукнция AddNode(15)");
            test.AddNode(15);
            Console.WriteLine(test.PrintNode());

            Console.ReadKey();
        }
        public class Node : ILinkedList
        {
            public int Value { get; set; }
            public Node NextNode { get; set; }
            public Node PrevNode { get; set; }
            public Node StartNode { get; set; }
            public Node EndNode { get; set; }
            public Node(int value)
            {
                Value = value;
                StartNode = this;
                EndNode = this;
            }
            public void AddNode(int value)
            {
                Node newNode = new Node(value);
                newNode.PrevNode = EndNode;

                EndNode.NextNode = newNode;

                EndNode = newNode;
            }
            public void AddNodeAfter(Node node, int value)
            {
                Node newNode = new Node(value);

                newNode.PrevNode = node;
                newNode.NextNode = node.NextNode;

                node.NextNode.PrevNode = newNode;

                node.NextNode = newNode;

                if (newNode.NextNode == null)
                {
                    EndNode = newNode;
                }
            }
            public Node FindNode(int searchValue)
            {
                Node tempNode = StartNode;

                while ((tempNode != null) && (tempNode.Value != searchValue))
                {
                    tempNode = tempNode.NextNode;
                }

                return tempNode;
            }
            public int GetCount()
            {
                if (StartNode == null)
                {
                    return 0;
                }

                int count = 1;

                Node tempNode = StartNode;

                while (tempNode.NextNode != null)
                {
                    tempNode = tempNode.NextNode;
                    count++;
                }

                return count;
            }
            public void RemoveNode(int index)
            {
                int i = 0;

                Node tempNode = StartNode;

                while (i != index)
                {
                    tempNode = tempNode.NextNode;
                    i++;
                }

                RemoveNode(tempNode);
            }
            public void RemoveNode(Node node)
            {
                if (node.PrevNode != null)
                {
                    node.PrevNode.NextNode = node.NextNode;
                }
                else if (node.NextNode != null)
                {
                    StartNode = node.NextNode;
                }
                if (node.NextNode != null)
                {
                    node.NextNode.PrevNode = node.PrevNode;
                }
                else if (node.PrevNode != null)
                {
                    EndNode = node.PrevNode;
                }

                node.PrevNode = null;
                node.NextNode = null;
            }
            public string PrintNode()
            {
                string result = "";

                Node testNode = StartNode;

                while (testNode != null)
                {
                    result = result + ((result == "") ? "" : ", ") + testNode.Value;
                    testNode = testNode.NextNode;
                }

                return result;
            }
        }
        //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
        public interface ILinkedList
        {
            int GetCount(); // возвращает количество элементов в списке
            void AddNode(int value); // добавляет новый элемент списка
            void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
            void RemoveNode(int index); // удаляет элемент по порядковому номеру
            void RemoveNode(Node node); // удаляет указанный элемент
            Node FindNode(int searchValue); // ищет элемент по его значению
        }
    }
}
