using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptCollection
{
    static List<Object> collection = new List<Object>();

    public static void NewList()
    {
        collection = new List<Object>();
    }

    /// <summary>
    /// Registers a script in a list.
    /// </summary>
    /// <param name="obj">Pass in 'this'</param>
    public static void RegisterScript(Object obj)
    {
        if (!collection.Exists(x => x.GetType().Equals(obj.GetType())))
            collection.Add(obj);
    }

    /// <summary>
    /// Returns a script of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of script you want</typeparam>
    /// <returns></returns>
    public static T GetScript<T>()
    {
        return (T)System.Convert.ChangeType(collection.Find(x => x.GetType().Equals(typeof(T))), typeof(T));
    }

    /// <summary>
    /// Removes a script from the collection. Can be called at any point but mostly when the script is disabled.
    /// </summary>
    /// <typeparam name="obj"></typeparam>
    public static void RemoveScript(Object obj)
    {
        collection.Remove(obj);
    }
}


