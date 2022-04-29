using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace less_3
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadKey();
        }
    }
    public class PointClass
    {
        public float X;
        public float Y;

        public PointClass(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
    public struct PointStruct
    {
        public float X;
        public float Y;

        public PointStruct(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
    public struct PointStructD
    {
        public double X;
        public double Y;

        public PointStructD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class BenchmarkClass
    {
        [ParamsSource(nameof(ValuesPoint))]
        public int[] pointOne { get; set; }
        [ParamsSource(nameof(ValuesPoint))]
        public int[] pointTwo { get; set; }
        public IEnumerable<int[]> ValuesPoint => new[] { new int[] { 10, 30 }, new int[] { 20, 50 }, new int[] { 0, 35 } };

        public PointClass pointOneClass { get { return new PointClass(pointOne[0], pointOne[1]); } set { } }
        public PointClass pointTwoClass { get { return new PointClass(pointTwo[0], pointTwo[1]); } set { } }

        public PointStruct pointOneStruct { get { return new PointStruct(pointOne[0], pointOne[1]); } set { } }
        public PointStruct pointTwoStruct { get { return new PointStruct(pointOne[0], pointOne[1]); } set { } }

        public PointStructD pointOneStructD { get { return new PointStructD(pointOne[0], pointOne[1]); } set { } }
        public PointStructD pointTwoStructD { get { return new PointStructD(pointOne[0], pointOne[1]); } set { } }

        public float PointDistanceClass(PointClass pointOne, PointClass pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public float PointDistanceStruct(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public double PointDistanceStructD(PointStructD pointOne, PointStructD pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }

        public float PointDistanceStructShort(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }

        [Benchmark]
        public void TestPointDistanceClass()
        {
            PointDistanceClass(pointOneClass, pointTwoClass);
        }
        
        [Benchmark]
        public void TestPointDistanceStruct()
        {
            PointDistanceStruct(pointOneStruct, pointTwoStruct);
        }

        [Benchmark]
        public void TestPointDistanceStructD()
        {
            PointDistanceStructD(pointOneStructD, pointTwoStructD);
        }

        [Benchmark]
        public void TestPointDistanceStructShort()
        {
            PointDistanceStructShort(pointOneStruct, pointTwoStruct);
        }
    }
}
