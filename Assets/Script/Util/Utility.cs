using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;

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
    }

    public static class RandomUtil
    {
        private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);
        public static void SetSeed(int seed) => RandomUtil.s_Random = new System.Random(seed);
        public static int GetRandom() => RandomUtil.s_Random.Next();
        public static int GetRandom(int maxValue) => RandomUtil.s_Random.Next(maxValue);
        public static int GetRandom(int minValue, int maxValue) => RandomUtil.s_Random.Next(minValue, maxValue);
        public static double GetRandomDouble() => RandomUtil.s_Random.NextDouble();
        public static void GetRandomBytes(byte[] buffer) => RandomUtil.s_Random.NextBytes(buffer);
    }

    public static class SearchUtil
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

    public static class DetectionUtils
    {
        public static bool CalculateAngle(Transform originTransform, Transform targetTransform, float viewAngle)
        {
            Vector3 angleDirection = (targetTransform.position - originTransform.position).normalized;

            Vector3 forward = originTransform.forward;

            float angle = Vector3.Angle(forward, angleDirection);

            return angle < viewAngle / 2;
        }

        public static bool CalculateDistance(Transform originTransform, Transform targetTransform, float viewDistance)
        {
            return Vector3.Distance(targetTransform.position, originTransform.position) <= viewDistance;
        }

        public static bool RayCast(Transform originTransform, Transform targetTransform, float viewDistance, Vector3 offSet, LayerMask targetLayer, Color? color = null)
        {
            Color finalColor = color ?? Color.red;
            Vector3 rayDirection = (targetTransform.position - originTransform.position).normalized;
            Vector3 rayOrigin = originTransform.position + originTransform.TransformDirection(offSet);
            
            Debug.DrawRay(rayOrigin, rayDirection * (viewDistance - offSet.z), finalColor);

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, viewDistance - offSet.z, targetLayer,
                    QueryTriggerInteraction.Ignore))
            {
                return true;
            }
            return false;
        }
    }
    public static class AIUtil
    {
        public static void CalculatePath(List<Vector3> pathList, Vector3 originPosition, Vector3 targetPosition, NavMeshPath path)
        {
            pathList.Clear();
            NavMesh.CalculatePath(originPosition, targetPosition, NavMesh.AllAreas, path);
            for (int i = 1; i < path.corners.Length; i++)
            {
                pathList.Add(path.corners[i]);
            }
        }
        public static void RotateToTarget(this Transform originTransform, Vector3 moveDirection, float rotationSpeed)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

            originTransform.rotation = Quaternion.Slerp(
                originTransform.rotation, 
                rotation, 
                rotationSpeed * Time.fixedDeltaTime
            );
        }
        public static void MoveToTarget(this Rigidbody originRigidbody, Vector3 moveDirection, float moveSpeed)
        {
            Vector3 moveVelocity = moveDirection * moveSpeed;
            moveVelocity.y = originRigidbody.linearVelocity.y;
            originRigidbody.linearVelocity = moveVelocity;
        }
    }
}