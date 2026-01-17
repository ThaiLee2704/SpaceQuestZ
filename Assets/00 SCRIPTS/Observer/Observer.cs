using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverTest
{
    public class Observer : MonoBehaviour
    {
        List<ISubscriber> subscribers = new List<ISubscriber>();

        public void AddListener(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void RemoveListener(ISubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        public void Notify()
        {
            foreach (ISubscriber subscriber in subscribers)
                subscriber.OnNotify();
        }
    }
}
