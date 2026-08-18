using STD.Core.Entity;
using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Player.Bullet
{
    using static Utils.Constants.Bullet;
    public class BulletScript : MonoBehaviour
    {
        private PlayerScript stats;
        void Start()
        {
            stats = FindFirstObjectByType<PlayerScript>();
        }
        private async void OnEnable()
        {
            await DisableColliderAsync(this.gameObject.GetComponentInChildren<Collider>());
            await DestroyAsync(BULLET_DESTROY_TIME);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy"))
            {
                other.GetComponent<IDamageable>().TakeDamage((int)(10f * (1 +stats.DamageMultiplier)));
            }
        }

        private async Awaitable DestroyAsync(float duration)
        {
            await Awaitable.WaitForSecondsAsync(duration);
            BulletPool.Singleton.GoBackToPool(this);
        }

        private async Awaitable DisableColliderAsync(Collider c)
        {
            c.enabled = false;
            await Awaitable.WaitForSecondsAsync(BULLET_COLLIDER_DISABLE_TIME);
            c.enabled = true;
        }
    }
}
