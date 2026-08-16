using System;
using System.Collections.Generic;
using UnityEngine;

namespace STD.Utils
{
    public class Observer : MonoBehaviour
    {
        static Dictionary<string, List<Delegate>> listeners = new();

        public static void Subscribe(string name, Delegate callback)
        {
            if (!listeners.ContainsKey(name))
                listeners.Add(name, new List<Delegate>());
            if (callback == null)
                return;
            listeners[name].Add(callback);
        }

        public static void Unsubscribe(string name, Delegate callback)
        {
            if (!listeners.ContainsKey(name))
            {
                Debug.LogWarning("Event not exist");
                return;
            }
            listeners[name].Remove(callback);
        }

        public static void Publish(string name)
        {
            if (!listeners.ContainsKey(name))
                return;

            foreach (var e in listeners[name])
            {
                if (!listeners.TryGetValue(name, out var callbacks))
                {
                    Debug.LogWarning($"Event '{name}' does not exist.");
                    return;
                }

                foreach (var callback in callbacks.ToArray())
                {
                    try
                    {
                        callback?.DynamicInvoke();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Observer error: {ex}");
                    }
                }
            }
        }

        public static void Publish<T>(string name, T arg)
        {
            if (!listeners.TryGetValue(name, out var callbacks))
                return;

            foreach (var callback in callbacks.ToArray())
            {
                try
                {
                    callback?.DynamicInvoke(arg);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Observer error: {ex}");
                }
            }
        }
    }
}