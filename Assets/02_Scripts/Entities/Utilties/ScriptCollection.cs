using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptCollection
{
    static List<Object> objects = new List<Object>();
    public static void NewList()
    {
        objects = new List<Object>();
    }

    /// <summary>
    /// Registers a script in a list
    /// </summary>
    /// <param name="obj">Pass in 'this'</param>
    public static void RegisterScript(Object obj)
    {
        if (!objects.Exists(x => x.GetType().Equals(obj.GetType())))
            objects.Add(obj);
    }

    /// <summary>
    /// Returns a script of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of script you want</typeparam>
    /// <returns></returns>
    public static T GetScript<T>()
    {
        // if (objects.Exists(x => x.GetType().Equals(typeof(T))))
        // {
        return (T)System.Convert.ChangeType(objects.Find(x => x.GetType().Equals(typeof(T))), typeof(T));
        // }
        // return default(T);
    }

    /// <summary>
    /// Removes a script of the specified type from the collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void RemoveScript<T>()
    {
        // if (obj == null)
            objects.RemoveAt(objects.FindIndex(0, x => x.GetType().Equals(typeof(T))));
        // else
        //     objects.Remove(obj);
    }
}


