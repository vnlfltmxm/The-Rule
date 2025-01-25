using System;

namespace Script.Manager.Framework
{
    public class Utility
    {
        public static class Random
        {
            private static System.Random s_Random = new System.Random((int) DateTime.UtcNow.Ticks);
            
            public static void SetSeed(int seed) => s_Random = new System.Random(seed);
            public static int GetRandom() => s_Random.Next();
            public static int GetRandom(int maxValue) => s_Random.Next(maxValue);
            public static int GetRandom(int minValue, int maxValue) => s_Random.Next(minValue, maxValue);
            
            public static float GetRandomFloat() => (float)s_Random.NextDouble();
            public static float GetRandomFloat(float maxValue) => (float)(s_Random.NextDouble() * maxValue);
            public static float GetRandomFloat(float minValue, float maxValue) => (float)(s_Random.NextDouble() * (maxValue - minValue) + minValue);
            
            public static double GetRandomDouble() => s_Random.NextDouble();
            public static void GetRandomBytes(byte[] buffer) => s_Random.NextBytes(buffer);
        }
    }
}