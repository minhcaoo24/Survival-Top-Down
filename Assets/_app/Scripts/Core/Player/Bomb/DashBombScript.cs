using UnityEngine;
using STD.Core.Interface;

namespace STD.Core.Player.Bomb
{
    using static STD.Utils.Constants.Bomb;

    public class DashBombScript : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private ParticleSystem explosionEffect;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private async void OnEnable()
        {
            // Explode();
            Collider[] hits = Physics.OverlapSphere(
                transform.position,
                DASHBOMB_EXPLOSION_RADIUS,
                enemyLayer
            );

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(DASHBOMB_EXPLOSION_DAMAGE);
                }
            }

            if (explosionEffect != null)
            {
                explosionEffect.Play();
            }

            await Awaitable.WaitForSecondsAsync(1f);
            explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            gameObject.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(
                transform.position,
                DASHBOMB_EXPLOSION_RADIUS
            );
        }
    }
}