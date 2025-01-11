using System.Collections.Generic;
using UnityEngine;

public class TreeNode<T>
{
    public TreeNode<T> Parent { get; set; }
    public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
    public T Data { get; set; }

    public TreeNode(T data)
    {
        Data = data;
    }

    // 자식 노드 추가
    public void AddChild(TreeNode<T> child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    // 자식 노드 제거
    public bool RemoveChild(TreeNode<T> child)
    {
        return Children.Remove(child);
    }
}