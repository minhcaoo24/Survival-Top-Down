using UnityEngine;

namespace STD.Core.Entity.SuperMinion
{
    public class SuperMinionAttack : MonoBehaviour
    {
        [SerializeField] private Collider hitbox;
        public void EnableHitbox()
        {
            hitbox.enabled = true;
        }

        public void DisableHitbox()
        {
            hitbox.enabled = false;
        }
    }
}