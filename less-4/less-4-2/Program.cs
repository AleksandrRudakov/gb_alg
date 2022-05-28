using System;
using System.Collections.Generic;

namespace less_4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree NewTree = new Tree();
            NewTree.AddItem(5);
            NewTree.AddItem(7);
            NewTree.AddItem(3);
            NewTree.AddItem(8);
            NewTree.AddItem(9);
            NewTree.AddItem(1);
            NewTree.AddItem(2);
            NewTree.AddItem(4);
            NewTree.AddItem(6);
            NewTree.AddItem(12);
            NewTree.AddItem(15);
            NewTree.AddItem(11);
            NewTree.AddItem(18);
            NewTree.AddItem(0);
            NewTree.AddItem(-1);
            NewTree.AddItem(-5);
            NewTree.AddItem(-7);

            Console.WriteLine("Исходное дерево:");
            NewTree.PrintTree();

            Console.WriteLine("\nСбалансированное дерево:");
            NewTree.GetBalancedTree();
            NewTree.PrintTree();

            Console.WriteLine("\nУдаляем элемент 6:");
            NewTree.RemoveItem(6);
            NewTree.PrintTree();

            Console.WriteLine("\nУдаляем элемент -5:");
            NewTree.RemoveItem(-5);
            NewTree.PrintTree();

            Console.WriteLine("\nУдаляем элемент -7:");
            NewTree.RemoveItem(-7);
            NewTree.PrintTree();

            Console.WriteLine("\nУдаляем элемент 0:");
            NewTree.RemoveItem(0);
            NewTree.PrintTree();

            Console.WriteLine("\nУдаляем элемент 11:");
            NewTree.RemoveItem(11);
            NewTree.PrintTree();

            Console.WriteLine("\nПовторная балансировка дерева:");
            NewTree.GetBalancedTree();
            NewTree.PrintTree();

            Console.ReadKey();
        }
    }

    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Parent { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }

        public TreeNode(int Value, TreeNode Parent)
        {
            this.Value = Value;
            this.Parent = Parent;
        }

        public override bool Equals(object obj)
        {
            var node = obj as TreeNode;
            if (node == null)
                return false;
            return node.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public interface ITree
    {
        TreeNode GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); // получить узел дерева по значению
        void PrintTree(); // вывести дерево в консоль
    }

    public class Tree : ITree
    {
        public TreeNode Head { get; set; }

        public TreeNode GetFreeNode(int value, TreeNode parent)
        {
            return new TreeNode(value, parent);
        }

        public TreeNode GetRoot()
        {
            return Head;
        }

        public void AddItem(int value)
        {
            TreeNode head = GetRoot();

            TreeNode tmp;

            if (head == null)
            {
                Head = GetFreeNode(value, null);
                return;
            }

            tmp = head;
            while (tmp != null)
            {
                if (value > tmp.Value)
                {
                    if (tmp.RightChild != null)
                    {
                        tmp = tmp.RightChild;
                        continue;
                    }
                    else
                    {
                        tmp.RightChild = GetFreeNode(value, tmp);
                        return;
                    }
                }
                else if (value < tmp.Value)
                {
                    if (tmp.LeftChild != null)
                    {
                        tmp = tmp.LeftChild;
                        continue;
                    }
                    else
                    {
                        tmp.LeftChild = GetFreeNode(value, tmp);
                        return;
                    }
                }
                else
                {
                    return;
                    throw new Exception("Wrong tree state");
                }
            }
        }

        public TreeNode GetNodeByValue(int value)
        {
            TreeNode tmp = GetRoot();

            while (tmp != null)
            {
                if (value > tmp.Value)
                {
                    tmp = tmp.RightChild;
                    continue;
                }
                else if (value < tmp.Value)
                {
                    tmp = tmp.LeftChild;
                    continue;
                }
                else
                {
                    return tmp;
                }
            }

            return null;
        }

        public void PrintTree()
        {
            BTreePrinter.Print(Head);
        }

        public void RemoveItem(int value)
        {
            TreeNode tmp = GetNodeByValue(value);
            if (tmp != null)
            {
                if (tmp.Parent == null)
                {
                    Head = null;
                }
                if (tmp.LeftChild == null && tmp.RightChild == null)
                {
                    if (tmp.Parent.LeftChild == tmp)
                    {
                        tmp.Parent.LeftChild = null;
                    }
                    else
                    {
                        tmp.Parent.RightChild = null;
                    }
                }
                else if (tmp.LeftChild != null && tmp.RightChild == null)
                {
                    tmp.Parent.LeftChild = tmp.LeftChild;
                    tmp.LeftChild.Parent = tmp.Parent;
                }
                else if (tmp.LeftChild == null && tmp.RightChild != null)
                {
                    tmp.Parent.RightChild = tmp.RightChild;
                    tmp.RightChild.Parent = tmp.Parent;
                }
                else
                {
                    TreeNode MinNode = GetMinRightChild(tmp);

                    MinNode.Parent.LeftChild = null;

                    MinNode.Parent = tmp.Parent;
                    MinNode.LeftChild = tmp.LeftChild;
                    MinNode.RightChild = tmp.RightChild;

                    tmp.LeftChild.Parent = MinNode;
                    tmp.RightChild.Parent = MinNode;

                    if (tmp.Parent.LeftChild == tmp)
                    {
                        tmp.Parent.LeftChild = MinNode;
                    }
                    else
                    {
                        tmp.Parent.RightChild = MinNode;
                    }
                }
            }
        }

        public TreeNode GetMinRightChild(TreeNode tmp)
        {
            TreeNode StartNode = tmp.RightChild;

            while (StartNode.LeftChild != null)
            {
                StartNode = StartNode.LeftChild;
            }

            return StartNode;
        }

        public void GetBalancedTree()
        {
            var Array = new List<int>();

            GetArrayFromTree(Array, GetRoot());

            Array.Sort();

            Head = null;

            BuildBalancedTree(Array);
        }

        public void GetArrayFromTree(List<int> Array, TreeNode tmp)
        {
            if (tmp != null)
            {
                GetArrayFromTree(Array, tmp.LeftChild);
                GetArrayFromTree(Array, tmp.RightChild);
                Array.Add(tmp.Value);
            }
        }

        public void BuildBalancedTree(List<int> Array)
        {
            if (Array.Count == 1)
            {
                AddItem(Array[0]);
            }
            else if (Array.Count > 0)
            {
                int middle = Array.Count / 2;

                AddItem(Array[middle]);

                List<int> NewArray = new List<int>();
                
                for (int i = 0; i < middle; i++)
                {
                    NewArray.Add(Array[i]);
                }
                BuildBalancedTree(NewArray);

                NewArray.Clear();

                for (int i = (middle + 1); i < Array.Count; i++)
                {
                    NewArray.Add(Array[i]);
                }
                BuildBalancedTree(NewArray);
            }
        }
    }

    public static class BTreePrinter
    {
        class NodeInfo
        {
            public TreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this TreeNode root, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo item, int top)
        {
            SwapColors();
            Print(item.Text, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    }
}