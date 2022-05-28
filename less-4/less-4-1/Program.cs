using System;
using System.Text;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace less_4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadKey();
        }
    }
    public class BenchmarkClass
    {
        public Random _random = new Random(Environment.TickCount);
        int idSearch = new Random().Next(10000);

        public string stringSearchMassiv { get; set; }
        public string stringSearchHashSet { get; set; }
        public string[] massString { get { return GenerateMassiv(); } set { } }
        public HashSet<StringHashSet> stringHashSet { get { return GenerateHashSet(); } set { } }

        public string[] GenerateMassiv()
        {
            string[] massString = new string[10000];
            for (int i = 0; i < 10000; i++)
            {
                massString[i] = RandomString(10);
                if (i == idSearch)
                {
                    stringSearchMassiv = massString[i];
                }
            }
            return massString;
        }

        public HashSet<StringHashSet> GenerateHashSet()
        {
            var hashSet = new HashSet<StringHashSet>();
            for (int i = 0; i < 10000; i++)
            {
                var stringHashSet = new StringHashSet() { StringField = RandomString(10) };
                hashSet.Add(stringHashSet);
                if (i == idSearch)
                {
                    stringSearchHashSet = stringHashSet.ToString();
                }
            }
            return hashSet;
        }

        public string RandomString(int length)
        {
            string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[_random.Next(chars.Length)]);

            return builder.ToString();
        }

        public class StringHashSet
        {
            public string StringField { get; set; }
            public override bool Equals(object obj)
            {
                var currentString = obj as StringHashSet;
                if (currentString == null)
                    return false;
                return StringField == currentString.StringField;
            }
            public override int GetHashCode()
            {
                int stringFieldHashCode = StringField?.GetHashCode() ?? 0;
                return stringFieldHashCode;
            }
        }

        [Benchmark]
        public bool SearchMassiv()
        {
            for (int i = 0; i < massString.Length; i++)
            {
                if (massString[i] == stringSearchMassiv)
                {
                    return true;
                }
            }
            return false;
        }

        [Benchmark]
        public bool SearchHashSet()
        {
            return stringHashSet.Contains(new StringHashSet() { StringField = stringSearchHashSet });
        }
    }
}