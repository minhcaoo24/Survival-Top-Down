using STD.Utils;
using UnityEngine;

namespace STD.Core.Entity.SuperMinion
{
    public class SuperMinionHitbox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            Debug.Log("HITTTT");
        }
    }
}