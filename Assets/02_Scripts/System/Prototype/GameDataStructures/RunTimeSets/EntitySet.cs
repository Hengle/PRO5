using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySet<T> : ScriptableObject
{
    public List<T> entityList = new List<T>();

    public void Add(T t)
    {
        entityList.Add(t);
    }

    public void Remove(T t)
    {
        if (entityList.Contains(t))
            entityList.Remove(t);
    }

    private void OnEnable()
    {
        entityList = new List<T>();
    }

    private void OnDisable()
    {
        entityList.Clear();
    }
}
