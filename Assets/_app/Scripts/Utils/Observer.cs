using System;
using System.Collections.Generic;
using UnityEngine;

namespace STD.Utils
{
    public class Observer : MonoBehaviour
    {
        static Dictionary<string, List<Action>> listener = new();

        public static void Subscribe(string name, Action callback)
        {
            if (!listener.ContainsKey(name))
                listener.Add(name, new List<Action>());
            listener[name].Add(callback);
        }

        public static void Unsubscribe(string name, Action callback)
        {
            if (!listener.ContainsKey(name))
            {
                Debug.LogWarning("Event not exist");
                return;
            }
            listener[name].Remove(callback);
        }

        public static void Publish(string name)
        {
            if (!listener.ContainsKey(name))
            {
                Debug.LogWarning("Event not exist");
                return;
            }

            foreach (var e in listener[name])
            {
                try
                {
                    e?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"<color=red>You have a error: {ex}</color>");
                }
            }
        }
    }
}