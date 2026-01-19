using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverTest
{
    public class Observer : MonoBehaviour
    {
        //BẢN KHÔNG TRUYỀN DATA

        //static Dictionary<string, List<Action>> Listeners = new Dictionary<string, List<Action>>();

        //public static void AddListener(string name, Action action)
        //{
        //    if (!Listeners.ContainsKey(name))
        //        Listeners.Add(name, new List<Action>());

        //    Listeners[name].Add(action);
        //}

        //public static void RemoveListener(string name, Action action)
        //{
        //    if (!Listeners.ContainsKey(name))
        //        return;

        //    Listeners[name].Remove(action);
        //}

        //public static void Notify(string name)
        //{
        //    if (!Listeners.ContainsKey(name))
        //        return;

        //    foreach (var item in Listeners[name])
        //    {
        //        try
        //        {
        //            item?.Invoke();
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.LogError($"Error in Invoke {ex}");
        //        }
        //    }

        //}

        //====> Nhận thấy bản Observer này chưa thể truyền data
        //Kiểu như muốn thay đổi UI HealthSlider thì phải truyền data(currentHealth, maxHealth) vào
        //Ta có bản nâng cấp có thể truyền data

        //BẢN CHO TRUYỀN DATA

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
}
