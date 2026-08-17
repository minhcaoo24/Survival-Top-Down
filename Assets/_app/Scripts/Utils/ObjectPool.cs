using System.Collections.Generic;
using UnityEngine;

namespace STD.Utils
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected Queue<T> pool = new();

        [SerializeField] protected T prefab;
        [SerializeField] private int amount = 9;
        [SerializeField] private Transform parent;

        public int Count => pool.Count;

        protected virtual void Awake() { }
        private void Start()
        {
            if (!prefab)
            {
                Debug.LogWarning("<color=red>Prefab is null!!</color>, cannot instantiate object in queue.");
                return;
            }

            T item;
            for (int i = 0; i <= amount; i++)
            {
                item = !parent ? Instantiate(prefab, Vector3.zero, Quaternion.identity) :
                    Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                pool.Enqueue(item);
            }
        }

        public T Get()
        {
            T item;

            if (Count > 0)
                item = pool.Dequeue();
            else
                item = !parent ? Instantiate(prefab, Vector3.zero, Quaternion.identity) :
                    Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);

            Rigidbody body = item.GetComponentInChildren<Rigidbody>();
            if (!body)
            {
                Debug.LogWarning($"<color=red>GameObject {item.name}'s rigidbody is null</color>");
                return null;
            }

            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            item.gameObject.SetActive(true);
            return item;
        }

        public void GoBackToPool(T item)
        {
            if (!item)
            {
                Debug.Log($"<color=red>That item isnot exist to go back to pool</color>");
                return;
            }
            Rigidbody body = item.GetComponentInChildren<Rigidbody>();
            if (!body)
            {
                Debug.LogWarning($"<color=red>GameObject {item.name}'s rigidbody is null</color>");
                return;
            }
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            item.gameObject.SetActive(false);
            pool.Enqueue(item);
        }

        public void ClearPool()
        {
            if (Count <= 0)
            {
                Debug.LogWarning($"<color=red>Cannot clear the pool, because it's nothing inside</color>");
                return;
            }
            pool.Clear();
        }
    }
}