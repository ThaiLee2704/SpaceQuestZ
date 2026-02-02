using System;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    static Dictionary<string, List<Action<object[]>>> Listeners = new Dictionary<string, List<Action<object[]>>>();

    public static void AddListener(string name, Action<object[]> action)
    {
        if (!Listeners.ContainsKey(name))
            Listeners.Add(name, new List<Action<object[]>>());

        Listeners[name].Add(action);
    }

    public static void RemoveListener(string name, Action<object[]> action)
    {
        if (!Listeners.ContainsKey(name))
            return;

        Listeners[name].Remove(action);
    }

    public static void Notify(string name, params object[] datas)
    {
        if (!Listeners.ContainsKey(name))
            return;

        foreach (var item in Listeners[name])
        {
            try
            {
                item?.Invoke(datas);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error in Invoke {ex}");
            }
        }

    }
}
