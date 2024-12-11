using System;

namespace Script.Manager.Framework
{
    public class Utility
    {
        public static class Random
        {
            private static System.Random s_Random = new System.Random((int) DateTime.UtcNow.Ticks);
            
            public static void SetSeed(int seed) => Utility.Random.s_Random = new System.Random(seed);
            public static int GetRandom() => Utility.Random.s_Random.Next();
            public static int GetRandom(int maxValue) => Utility.Random.s_Random.Next(maxValue);
            public static int GetRandom(int minValue, int maxValue) => Utility.Random.s_Random.Next(minValue, maxValue);
            public static double GetRandomDouble() => Utility.Random.s_Random.NextDouble();
            public static void GetRandomBytes(byte[] buffer) => Utility.Random.s_Random.NextBytes(buffer);
        }
    }
}