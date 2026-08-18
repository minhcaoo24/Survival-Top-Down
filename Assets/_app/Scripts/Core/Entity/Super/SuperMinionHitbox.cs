using STD.Core.Interface;
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

            other.GetComponent<IDamageable>().TakeDamage(30);
        }
    }
}