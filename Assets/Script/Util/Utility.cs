using System;
using System.Collections.Generic;
using Script.Util;

namespace Script.Manager.Framework
{
    public static class Utility
    {
        public static class Struct
        {
            public static short ShortNull => short.MinValue;
            public static int IntNull => int.MinValue;
            public static float FloatNull => float.MinValue;
            public static double DoubleNull => double.MinValue;
        }
        public static class Validation
        {
            public static bool IsIndexValid<T>(List<T> list, int index)
            {
                return index >= 0 && index < list.Count;
            }
            public static bool IsIndexValid<T>(IReadOnlyList<T> list, int index)
            {
                return index >= 0 && index < list.Count;
            }
        }
        public static class Path
        {
            public static string GetPrefabDataPath(PrefabDataType type) => $"Data/PrefabData/{type.ToString()}PrefabData";
            public static string GetSpriteDataPath => $"Data/SpriteData/SpriteData";
            public static string ItemDataBasePath => $"Data/ItemDataBase";
        }
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

        public static class Search
        {
            //깊이 우선 탐색
            public static TreeNode<T> DepthFirstSearch<T>(TreeNode<T> root, T value)
            {
                if (root.Data.Equals(value))
                    return root;

                foreach (var child in root.Children)
                {
                    var result = DepthFirstSearch(child, value);
                    if (result != null)
                        return result;
                }

                return null;
            }
            //너비 우선 탐색
            public static TreeNode<T> BreadthFirstSearch<T>(TreeNode<T> root, T value)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
                queue.Enqueue(root);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (current.Data.Equals(value))
                        return current;

                    foreach (var child in current.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

                return null;
            }
            public static TreeNode<T> BreadthFirstSearch<T>(TreeNode<T> root, Func<T, bool> predicate)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
                queue.Enqueue(root);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (predicate(current.Data))
                        return current;

                    foreach (var child in current.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

                return null;
            }
        }
        
    }
}